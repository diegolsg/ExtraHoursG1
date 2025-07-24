namespace ExtraHours.Core.dto
{
    public class ExtraHourSummaryDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Fecha { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int CantidadHoras { get; set; }
        public string Estado { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}



