using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Medicine
{
    public class TransferMedicineDto
    {
        [Required]
        [Range(1, 1000000000)]
        [DefaultValue(0)]
        public int MedicineId { get; set; } = 0;

        [Required]
        [Range(1, 1000000000)]
        [DefaultValue(0)]
        public int FromWarehouseId { get; set; } = 0;

        [Required]
        [Range(1, 1000000000)]
        [DefaultValue(0)]
        public int ToWarehouseId { get; set; } = 0;

        [Required]
        [Range(1, 100000)]
        [DefaultValue(0)]
        public int Quantity { get; set; } = 0;
    }
}
