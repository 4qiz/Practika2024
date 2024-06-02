namespace api.Models
{
    public partial class WarehouseHasMedicine
    {
        public int MedicineId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public virtual Warehouse Warehouse { get; set; } = null!;

        public virtual Medicine Medicine { get; set; } = null!;
    }
}
