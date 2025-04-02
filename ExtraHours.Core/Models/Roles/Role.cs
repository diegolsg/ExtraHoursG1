

namespace ExtraHours.Models.Roles
{
    public class Role
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }  
    }
}


public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Relación muchos-a-muchos con Permissions
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}


// Models/Roles/Permission.cs
public class Permission
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty; // Ej: "users.create"
    public string Description { get; set; } = string.Empty;
    
    // Relación muchos-a-muchos con Roles
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}