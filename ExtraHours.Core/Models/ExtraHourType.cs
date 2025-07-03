namespace ExtraHours.Core.Models {
    public class ExtraHourType
    {
        public int Id { get; set; }
        public required string TypeHourName { get; set; }
        public required string Porcentaje { get; set; }
        public TimeSpan StartExtraHour { get; set; }
        public TimeSpan EndExtraHour { get; set; }
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; } 
        
    }
}