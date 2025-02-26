using System;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Api.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IUserRepository userRepository,
            IPasswordHasher<UserEntity> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<EmployeeEntity> CreateAsync(EmployeeEntity employee)
        {
            if (employee.User == null)
            {
                throw new ArgumentException("User information is required");
            }

            // Hash the provided password
            var password = employee.User.PasswordHash; // Temporarily store the plain password
            employee.User.PasswordHash = _passwordHasher.HashPassword(employee.User, password);

            // Create user first
            var user = await _userRepository.InsertAsync(employee.User);

            // Create a new employee entity with the user ID
            var newEmployee = new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DocumentNumber = employee.DocumentNumber,
                PhoneNumbers = employee.PhoneNumbers,
                ManagerId = employee.ManagerId,
                DateOfBirth = employee.DateOfBirth,
                Role = employee.Role,
                HierarchicalLevel = employee.HierarchicalLevel,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            // Create employee
            return await _employeeRepository.InsertAsync(newEmployee);
        }

        public async Task<EmployeeEntity> UpdateAsync(EmployeeEntity employee)
        {
            return await _employeeRepository.UpdateAsync(employee);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<EmployeeEntity> GetByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }
        public async Task<EmployeeEntity> GetByNameAsync(string name)
        {
            return await _employeeRepository.GetByNameAsync(name);
        }


        public async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}
