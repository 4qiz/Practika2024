using api.Data;
using api.Dtos.WarehouseHasMedicine;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseHasMedicineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WarehouseHasMedicineController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMedicineToWarehouse(WarehouseHasMedicineCreateDto createDto)
        {
            if (createDto.Quantity <= 0)
            {
                return BadRequest("Не возможно добавить отрицательное/нулевое количество");
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
                    warehouseHasMedicine.Quantity += createDto.Quantity;
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
                if (warehouseHasMedicine.Quantity < 0)
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
