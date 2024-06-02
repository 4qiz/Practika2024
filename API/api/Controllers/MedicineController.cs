using api.Data;
using api.Dtos.Medicine;
using api.Helpers;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicine
        [HttpGet]
        public async Task<ActionResult<List<MedicineDto>>> GetMedicines([FromQuery] MedicineQueryObject query)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var medicines = _context.Medicines
                .Include(m => m.WarehouseHasMedicines)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.WarehouseTitle))
            {
                var warehouse = _context.Warehouses.FirstOrDefault(w => w.Name.ToLower().Contains(query.WarehouseTitle.ToLower()));
                if (warehouse == null)
                {
                    return NotFound("склад не найден");
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
                .FirstOrDefaultAsync(m => m.MedicineId == id);

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine.ToDtoFromMedicine();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, MedicineDto medicineDto)
        {
            if (id != medicineDto.MedicineId)
            {
                return BadRequest();
            }

            try
            {
                var warehouseHasMedicine = new WarehouseHasMedicine
                {

                };

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
                {
                    return NotFound("Препарат не найден");
                }
                else
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(MedicineDto medicineDto)
        {
            var medicine = medicineDto.ToMedicineFromDto();
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMedicine", new { id = medicineDto.MedicineId }, medicineDto);
        }

        [HttpPost("with_warehouse")]
        public async Task<ActionResult<Medicine>> PostMedicineWithWarehouse(MedicineDto medicineDto)
        {
            //todo
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMedicine", new { id = medicineDto.MedicineId }, medicineDto);
        }

        [HttpPost("import_list")]
        public async Task<ActionResult<Medicine>> PostMedicines(List<MedicineDto> medicineDto)
        {
            var medicines = new List<Medicine>();
            foreach (var item in medicineDto)
            {
                _context.Medicines.Add(item.ToMedicineFromDto());
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

        private bool MedicineExists(int id)
        {
            return (_context.Medicines?.Any(e => e.MedicineId == id)).GetValueOrDefault();
        }
    }
}
