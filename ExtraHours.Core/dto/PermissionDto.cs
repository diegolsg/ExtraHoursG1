namespace ExtraHours.Core.dto
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

    }
}