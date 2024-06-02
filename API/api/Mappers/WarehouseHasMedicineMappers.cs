using api.Dtos.Medicine;
using api.Dtos.WarehouseHasMedicine;
using api.Models;

namespace api.Mappers
{
    public static class WarehouseHasMedicineMappers
    {
        public static WarehouseHasMedicine ToWarehouseHasMedicineFromDto(this WarehouseHasMedicineCreateDto warehouseHasMedicine)
        {
            return new WarehouseHasMedicine
            {
                WarehouseId = warehouseHasMedicine.WarehouseId,
                MedicineId = warehouseHasMedicine.MedicineId,
                Quantity = warehouseHasMedicine.Quantity,
            };
        }
    }
}
