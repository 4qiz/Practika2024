namespace api.Models
{
    public partial class Manufacturer
    {
        public int ManufacturerId { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
