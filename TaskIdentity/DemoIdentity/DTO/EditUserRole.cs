namespace DemoIdentity.DTO
{
    public class EditUserRole
    {
        public Guid UserId { get; set; }
        public Guid OldRoleId { get; set; }
        public Guid NewRoleId { get; set; }
    }
}
