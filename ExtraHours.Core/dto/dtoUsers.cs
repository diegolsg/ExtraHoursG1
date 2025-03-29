public class dtoUsers
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Code { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
        public int AreaId { get; set; }
        public  Boolean IsActive { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }