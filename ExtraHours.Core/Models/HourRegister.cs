using System.ComponentModel.DataAnnotations;

namespace ExtraHours.Core.Models
{
    public class RegistroHora
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal CantidadHoras { get; set; }

        [Required]
        public string TipoHora { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
