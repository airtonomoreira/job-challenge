using System;
using System.Collections.Generic;

namespace Api.Domain.Dtos
{
    public class EmployeeDtoResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public Guid? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public int HierarchicalLevel { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
