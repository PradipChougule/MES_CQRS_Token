namespace Manufacturing.Core.Domain
{
    public class UserRolesXref
    {
        public int Id { get; set; }
        public int UsertId { get; set; }
        public UserManagement UserManagement { get; set; }
        public int RoleId { get; set; }
        public Roles Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
