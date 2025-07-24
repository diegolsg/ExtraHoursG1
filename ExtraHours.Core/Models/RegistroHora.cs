using System;

namespace ExtraHours.Core.Models
{
    public class RegistroHora
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Fecha { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int CantidadHoras { get; set; }
        public string TipoHora { get; set; } = string.Empty;
    }

}
