using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Entities;
using Api.Domain.Dtos;
using Microsoft.AspNetCore.Identity;
using Api.Domain.Interfaces.Services;


namespace Api.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public AdminService(
            IEmployeeRepository employeeRepository,
            IUserRepository userRepository,
            IPasswordHasher<UserEntity> passwordHasher)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<EmployeeEntity> CreateEmployeeAsync(EmployeeDtoCreate employeeDto)
        {
            // First check if employee with same email already exists
            var existingEmployee = await _employeeRepository.GetByEmailAsync(employeeDto.Email);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException("Employee with this email already exists");
            }

            // First create and save the user
            var user = new UserEntity
            {
                Email = employeeDto.Email,
                PasswordHash = _passwordHasher.HashPassword(new UserEntity(), employeeDto.Password),
                HierarchyLevel = employeeDto.HierarchyLevel
            };

            // Save user and get the saved entity with Id
            var savedUser = await _userRepository.InsertAsync(user);

            // Then create the employee with the saved user's Id
            var employee = new EmployeeEntity
            {
                UserId = savedUser.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                DocumentNumber = employeeDto.DocumentNumber,
                PhoneNumbers = employeeDto.PhoneNumbers,
                ManagerId = employeeDto.ManagerId,
                DateOfBirth = employeeDto.DateOfBirth,
                Role = employeeDto.Role,
                HierarchicalLevel = employeeDto.HierarchyLevel
            };
            
            // Save and return the employee
            return await _employeeRepository.InsertAsync(employee);
        }

        public async Task<UserEntity> CreateUserAsync(UserDtoCreate userDto)
        {
            var user = new UserEntity
            {
                Email = userDto.Email,
                PasswordHash = _passwordHasher.HashPassword(new UserEntity(), userDto.Password),
                HierarchyLevel = userDto.HierarchyLevel
            };

            return await _userRepository.InsertAsync(user);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }
    }
}
