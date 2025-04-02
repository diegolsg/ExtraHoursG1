using System.ComponentModel.DataAnnotations;

namespace ExtraHours.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string Code { get; set; } = GenerateCode();
        public required string Password { get; set; }
        public required string Email { get; set; }
        public int AreaId { get; set; }
        public int RoleId { get; set; }

        public required Area Area { get; set; }
        public required Role Roles { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        private static string GenerateCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return "AMD" + code;
        }
    }
}
