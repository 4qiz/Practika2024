namespace api.Dtos.WarehouseHasMedicine
{
    public class WarehouseHasMedicineCreateDto
    {
        public int MedicineId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }
    }
}
