namespace Api.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int HierarchyLevel { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
