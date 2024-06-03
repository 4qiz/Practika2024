using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace api.Dtos.WarehouseHasMedicine
{
    public class WarehouseHasMedicineCreateDto
    {
        [Required]
        [Range(1, 1000000000)]
        [DefaultValue(0)]
        public int MedicineId { get; set; }

        [Required]
        [Range(1, 1000000000)]
        [DefaultValue(0)]
        public int WarehouseId { get; set; }

        [Required]
        [Range(1, 100000)]
        [DefaultValue(0)]
        public int Quantity { get; set; }
    }
}
