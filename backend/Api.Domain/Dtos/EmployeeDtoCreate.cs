using System;
using System.Collections.Generic;

namespace Api.Domain.Dtos
{
    public class EmployeeDtoCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DocumentNumber { get; set; }
        public List<string> PhoneNumbers { get; set; }
        public Guid? ManagerId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
