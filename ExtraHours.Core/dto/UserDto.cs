namespace ExtraHours.Core.Dto
{
    public class UserDto
    {
<<<<<<< HEAD
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
=======
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
       
        public required string Password { get; set; }
        public int RoleId { get; set; }
        public int AreaId { get; set; }
        public Boolean IsActive { get; set; }
>>>>>>> origin/diego
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
