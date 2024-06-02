using api.Dtos.Medicine;
using api.Models;

namespace api.Mappers
{
    public static class MedicineMappers
    {
        public static Medicine ToMedicineFromDto(this MedicineDto medicineDto)
        {
            return new Medicine
            {
                Image = medicineDto.Image,
                Manufacturer = medicineDto.Manufacturer,
                Name = medicineDto.Name,
                Price = medicineDto.Price,
                TradeName = medicineDto.TradeName
            };
        }

        public static MedicineDto ToDtoFromMedicine(this Medicine medicine)
        {
            return new MedicineDto
            {
                MedicineId = medicine.MedicineId,
                Image = medicine.Image,
                Manufacturer = medicine.Manufacturer,
                Name = medicine.Name,
                Price = medicine.Price,
                TradeName = medicine.TradeName,
                WarehouseHasMedicineDto = medicine.WarehouseHasMedicines.Select(w=> w.ToDtoFromWarehouseHasMedicine()).ToList()
            };
        }

        public static WarehouseHasMedicineDto ToDtoFromWarehouseHasMedicine(this WarehouseHasMedicine warehouseHasMedicine)
        {
            return new WarehouseHasMedicineDto
            {
                WarehouseId = warehouseHasMedicine.WarehouseId,
                StockQuantity = warehouseHasMedicine.Quantity
            };
        }
    }
}
