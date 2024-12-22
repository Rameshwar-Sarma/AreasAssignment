namespace CustomerSupportManagementSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int? SupportAgentId { get; set; }
    }
}
