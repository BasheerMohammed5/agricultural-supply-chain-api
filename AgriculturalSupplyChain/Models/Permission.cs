namespace AgriculturalSupplyChain.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }

        // Navigation property
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
