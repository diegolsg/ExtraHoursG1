namespace ExtraHours.Core.Models {
    public class ExtraHour
    {
        public int Id { get; set;}
        public int UserId { get; set;}
        public User Users { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set;}
        public string EndTime { get; set;}
        public string Status { get; set; } = "pendiente";
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        //nuevas
        public int ExtraHoursTypeId { get; set; }
        public ExtraHourType ExtraHoursType { get; set; }

      
    }
}