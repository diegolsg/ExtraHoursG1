namespace ExtraHours.Core.Dto
{    
    public class ExtraHourDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Status { get; set; } 
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}