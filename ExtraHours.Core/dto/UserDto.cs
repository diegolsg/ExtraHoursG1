﻿namespace ExtraHours.Core.Dto
{
    public class UserDto
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        // public int AreaId { get; set; }
        public int RoleId { get; set; }
    }
}
