namespace api.Models
{
    public partial class IssueRequest
    {
        public int IssueRequestId { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string Purpose { get; set; } = null!;

        public virtual ICollection<IssueRequestHasMedicine> IssueRequestHasMedicines { get; set; } = new List<IssueRequestHasMedicine>();
    }

}