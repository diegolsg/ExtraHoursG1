namespace ExtraHours.Core.Models {
    public class ExtraHour
    {
        public int Id { get; set;}
        public int UserId { get; set;}
        public User users { get; set; } = null!;
        public required string Code { get; set; }
        public DateTime Date { get; set;}
        public int StartTime { get; set;}
        public int EndTime { get; set;}
        public string Status { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //nuevas
        // public int ExtraHoursTypeId { get; set; }
        // public required ExtraHourType ExtraHoursType { get; set; }
    }
}