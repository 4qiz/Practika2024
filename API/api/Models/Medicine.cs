using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public partial class Medicine
    {
        public int MedicineId { get; set; }

        public string Name { get; set; } = null!;

        public string TradeName { get; set; } = null!;

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; } = null!;

        public string? Image { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<IssueRequestHasMedicine> IssueRequestHasMedicines { get; set; } = new List<IssueRequestHasMedicine>();

        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        public List<WarehouseHasMedicine> WarehouseHasMedicines { get; } = [];
    }
}