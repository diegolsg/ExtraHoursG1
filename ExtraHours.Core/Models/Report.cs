namespace ExtraHours.Core.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User Users { get; set; }
    }
}
