namespace Api.Domain.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
