using api.Data;
using api.Dtos.Medicine;
using api.Dtos.WarehouseHasMedicine;
using api.Helpers;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicineController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Fetches a list of medicines based on the provided query parameters.
        /// </summary>
        /// <param name="query">The query object containing filters for fetching medicines.</param>
        /// <returns>A list of MedicineDto objects based on the query filters.</returns>
        /// <response code="200">A list of Medicines</response>
        /// <response code="404">Search result is empty</response>
        [HttpGet]
        public async Task<ActionResult<List<MedicineDto>>> GetMedicines([FromQuery] MedicineQueryObject query)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var medicines = _context.Medicines
                .Include(m => m.WarehouseHasMedicines)
                .Include(m => m.Manufacturer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.WarehouseTitle))
            {
                var warehouse = _context.Warehouses.FirstOrDefault(w => w.Name.ToLower().Contains(query.WarehouseTitle.ToLower()));
                if (warehouse == null)
                {
                    return NotFound("Склад не найден");
                }
                medicines = medicines.Where(m => m.WarehouseHasMedicines != null && m.WarehouseHasMedicines.Any(wm => wm.WarehouseId == warehouse.WarehouseId));
            }

            var medicinesFiltered = await medicines.ToListAsync();

            return Ok(medicinesFiltered.Select(m => m.ToDtoFromMedicine()));
        }

        // GET: api/Medicine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDto>> GetMedicine(int id)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var medicine = await _context.Medicines
                .Include(m => m.WarehouseHasMedicines)
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.MedicineId == id);

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine.ToDtoFromMedicine();
        }

        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(MedicineDto medicineDto)
        {
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Title.Equals(medicineDto.Manufacturer));
            if (manufacturer == null)
            {
                manufacturer = new Manufacturer { Title = medicineDto.Manufacturer };
            }
            var medicine = medicineDto.ToMedicineFromDto(manufacturer);
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMedicine", new { id = medicineDto.MedicineId }, medicineDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMedicineToWarehouse(WarehouseHasMedicineCreateDto createDto)
        {
            if (createDto.Quantity <= 0)
            {
                return BadRequest("Не возможно добавить отрицательное/нулевое количество");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!MedicineExists(createDto.MedicineId))
            {
                return NotFound($"Препарат не найден id = {createDto.MedicineId}");
            }
            if (!WarehouseExists(createDto.WarehouseId))
            {
                return NotFound($"Склад не найден id = {createDto.WarehouseId}");
            }

            try
            {
                var warehouseHasMedicine = await _context.WarehouseHasMedicines
                    .FirstOrDefaultAsync(wm => wm.WarehouseId == createDto.WarehouseId
                        && wm.MedicineId == createDto.MedicineId);

                if (warehouseHasMedicine == null)
                {
                    warehouseHasMedicine = new WarehouseHasMedicine();
                    warehouseHasMedicine.MedicineId = createDto.MedicineId;
                    warehouseHasMedicine.WarehouseId = createDto.WarehouseId;
                    warehouseHasMedicine.Quantity = createDto.Quantity;
                    await _context.WarehouseHasMedicines.AddAsync(warehouseHasMedicine);
                }
                else
                {
                    warehouseHasMedicine.Quantity += createDto.Quantity;
                    _context.Update(warehouseHasMedicine);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Created();
        }

        [HttpPost("writeoff")]
        public async Task<IActionResult> WriteoffMedicineFromWarehouse(WarehouseHasMedicineCreateDto createDto)
        {
            if (createDto.Quantity <= 0)
            {
                return BadRequest("Не возможно делать списание отрицательного/нулевого количества");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!MedicineExists(createDto.MedicineId))
            {
                return NotFound($"Препарат не найден id = {createDto.MedicineId}");
            }
            if (!WarehouseExists(createDto.WarehouseId))
            {
                return NotFound($"Склад не найден id = {createDto.WarehouseId}");
            }

            try
            {
                var warehouseHasMedicine = await _context.WarehouseHasMedicines
                    .FirstOrDefaultAsync(wm => wm.WarehouseId == createDto.WarehouseId
                        && wm.MedicineId == createDto.MedicineId);

                if (warehouseHasMedicine == null)
                {
                    return NotFound($"Препарат id = {createDto.MedicineId} не найден на складе id = {createDto.WarehouseId}");
                }
                if (warehouseHasMedicine.Quantity < createDto.Quantity)
                {
                    return BadRequest("Списание больше, чем есть на складе");
                }
                warehouseHasMedicine.Quantity -= createDto.Quantity;

                _context.Update(warehouseHasMedicine);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Created();
        }

        [HttpPost("Transfer")]
        public async Task<ActionResult<MedicineDto>> TransferMedicine([FromBody] TransferMedicineDto transfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (transfer.ToWarehouseId == transfer.FromWarehouseId)
            {
                return BadRequest("Склад назначения совпадает с текущим складом");
            }
            if (!WarehouseExists(transfer.FromWarehouseId))
            {
                return NotFound($"Склад отправитель с указанным Id = {transfer.FromWarehouseId} не найден");
            }
            if (!WarehouseExists(transfer.ToWarehouseId))
            {
                return NotFound($"Склад получатель с указанным Id = {transfer.ToWarehouseId} не найден");
            }
            if (!MedicineExists(transfer.MedicineId))
            {
                return NotFound($"Препарат с Id = {transfer.MedicineId} не найден");
            }

            try
            {
                var fromWarehouseHasMedicine = await _context.WarehouseHasMedicines
                    .FirstOrDefaultAsync(wm => wm.WarehouseId == transfer.FromWarehouseId
                        && wm.MedicineId == transfer.MedicineId);

                if (fromWarehouseHasMedicine == null)
                {
                    return NotFound($"Препарат id = {transfer.MedicineId} не найден на складе id = {transfer.FromWarehouseId}");
                }
                if (fromWarehouseHasMedicine.Quantity < transfer.Quantity)
                {
                    return BadRequest("Списание больше, чем есть на складе");
                }
                fromWarehouseHasMedicine.Quantity -= transfer.Quantity;
                _context.Update(fromWarehouseHasMedicine);

                var toWarehouseHasMedicine = await _context.WarehouseHasMedicines
                    .FirstOrDefaultAsync(wm => wm.WarehouseId == transfer.ToWarehouseId
                        && wm.MedicineId == transfer.MedicineId);

                if (toWarehouseHasMedicine == null)
                {
                    toWarehouseHasMedicine = new WarehouseHasMedicine
                    {
                        MedicineId = transfer.MedicineId,
                        WarehouseId = transfer.ToWarehouseId,
                        Quantity = transfer.Quantity,
                    };
                   await _context.AddAsync(toWarehouseHasMedicine);
                }
                else
                {
                    toWarehouseHasMedicine.Quantity += transfer.Quantity;
                    _context.Update(toWarehouseHasMedicine);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Created();
        }

        [HttpPost("import_list")]
        public async Task<ActionResult<Medicine>> PostMedicines(List<MedicineDto> medicineDto)
        {
            var medicines = new List<Medicine>();
            foreach (var item in medicineDto)
            {
                var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(m => m.Title.Equals(item.Manufacturer));
                if (manufacturer == null)
                {
                    manufacturer = new Manufacturer { Title = item.Manufacturer };
                }
                _context.Medicines.Add(item.ToMedicineFromDto(manufacturer));
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Medicine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var s5Medicine = await _context.Medicines.FindAsync(id);
            if (s5Medicine == null)
            {
                return NotFound();
            }

            _context.Medicines.Remove(s5Medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(int id)
        {
            return (_context.Warehouses?.Any(e => e.WarehouseId == id)).GetValueOrDefault();
        }

        private bool MedicineExists(int id)
        {
            return (_context.Medicines?.Any(e => e.MedicineId == id)).GetValueOrDefault();
        }
    }
}
