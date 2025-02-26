using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Domain.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public Guid? ManagerId { get; set; }
        public EmployeeEntity Manager { get; set; }
        public ICollection<EmployeeEntity> Subordinates { get; set; } = new List<EmployeeEntity>();
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; } = "Employee";
        public int HierarchicalLevel { get; set; }

        public static ValueComparer<List<string>> PhoneNumbersComparer { get; } = new(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());
    }
}
