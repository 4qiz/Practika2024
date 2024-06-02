namespace api.Dtos.Medicine
{
    public class MedicineDto
    {
        public int MedicineId { get; set; }

        public string Name { get; set; } = null!;

        public string TradeName { get; set; } = null!;

        public string Manufacturer { get; set; } = null!;

        public string Image { get; set; } = null!;

        public decimal Price { get; set; }

        public List<WarehouseHasMedicineDto> WarehouseHasMedicineDto { get; set; } = [];
    }
}
