using System;

namespace ExtraHours.Core.Models
{
    public class RegistroHora
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal CantidadHoras { get; set; }

        public string TipoHora { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        // Si tienes relación con User:
        public User? User { get; set; }
    }
}
