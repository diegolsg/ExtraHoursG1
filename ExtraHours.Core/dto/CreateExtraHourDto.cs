namespace ExtraHours.Core.Dto
{
    public class CreateExtraHourDto
    {
        public int UserId { get; set; }
        public int CantidadHoras { get; set; }
        public string TipoHora { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
