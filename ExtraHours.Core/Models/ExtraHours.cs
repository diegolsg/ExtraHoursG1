namespace ExtraHours.Core.Models {
    public class ExtraHours
    {
        public int Id { get; set;}
        public int userId { get; set;}
        public Users users { get; set; } = null!;
        public DateTime date { get; set;}
        public int StartTime { get; set;}
        public int EndTime { get; set;}
        public string Status { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}