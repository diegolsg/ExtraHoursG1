namespace ExtraHours.Core.Models {
    public class ExtraHour
    {
        public int Id { get; set;}
        public int userId { get; set;}
        public User users { get; set; } = null!;
        public DateOnly date { get; set;}
        public TimeSpan StartTime { get; set;}
        public TimeSpan EndTime { get; set;}
        public string Status { get; set; } = "pendiente";
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        //nuevas
        public int ExtraHoursTypeId { get; set; }
        public required ExtraHourType ExtraHoursType { get; set; }

      
    }
}