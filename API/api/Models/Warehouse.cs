namespace api.Models
{
    public partial class Warehouse
    {
        public int WarehouseId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

        public List<WarehouseHasMedicine> WarehouseHasMedicines { get; } = [];
    }
}
