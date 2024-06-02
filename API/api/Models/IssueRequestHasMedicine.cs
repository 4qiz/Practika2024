namespace api.Models
{
    public partial class IssueRequestHasMedicine
    {
        public int MedicineId { get; set; }

        public int IssueRequestId { get; set; }

        public int Quantity { get; set; }

        public int Id { get; set; }

        public virtual IssueRequest IssueRequest { get; set; } = null!;

        public virtual Medicine Medicine { get; set; } = null!;
    }
}