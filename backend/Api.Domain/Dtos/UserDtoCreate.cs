using System;

namespace Api.Domain.Dtos
{
    public class UserDtoCreate
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
