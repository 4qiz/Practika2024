namespace api.Helpers
{
    public class MedicineQueryObject
    {
        public int WarehouseId { get; set; }
        public string WarehouseTitle { get; set; } = string.Empty;
        public string MedicineTitle { get; set; } = string.Empty;
        public string MedicineManufacturer{ get; set; } = string.Empty;
    }
}
