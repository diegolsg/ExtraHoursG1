namespace ExtraHours.Core.dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Code { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
        public int AreaId { get; set; }
        public Boolean IsActive { get; set; }
        public required string Email { get; set; }
    }
}
