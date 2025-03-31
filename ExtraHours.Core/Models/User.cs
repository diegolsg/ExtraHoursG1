using System.ComponentModel.DataAnnotations;

namespace ExtraHours.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Code { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public  int AreaId { get; set; }
        public int RoleId { get; set; }

        public Area Area { get; set; }
        public Role Roles { get; set; }
        public  Boolean IsActive { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
