using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using FluentAssertions;
using Api.Service.Services;

namespace Api.Service.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher<UserEntity>> _passwordHasherMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher<UserEntity>>();
            _employeeService = new EmployeeService(
                _employeeRepositoryMock.Object,
                _userRepositoryMock.Object,
                _passwordHasherMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_WithValidEmployee_ReturnsCreatedEmployee()
        {
            // Arrange
            var userEntity = new UserEntity { Id = Guid.NewGuid(), PasswordHash = "password123" };
            var employeeEntity = new EmployeeEntity
            {
                FirstName = "John",
                LastName = "Doe",
                User = userEntity
            };

            _passwordHasherMock
                .Setup(x => x.HashPassword(It.IsAny<UserEntity>(), It.IsAny<string>()))
                .Returns("hashedPassword");

            _userRepositoryMock
                .Setup(x => x.InsertAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(userEntity);

            _employeeRepositoryMock
                .Setup(x => x.InsertAsync(It.IsAny<EmployeeEntity>()))
                .ReturnsAsync(employeeEntity);

            // Act
            var result = await _employeeService.CreateAsync(employeeEntity);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
        }

        [Fact]
        public async Task CreateAsync_WithNullUser_ThrowsArgumentException()
        {
            // Arrange
            var employeeEntity = new EmployeeEntity
            {
                FirstName = "John",
                LastName = "Doe",
                User = null
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _employeeService.CreateAsync(employeeEntity)
            );
        }

        [Fact]
        public async Task UpdateAsync_WithValidEmployee_ReturnsUpdatedEmployee()
        {
            // Arrange
            var employeeEntity = new EmployeeEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            };

            _employeeRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<EmployeeEntity>()))
                .ReturnsAsync(employeeEntity);

            // Act
            var result = await _employeeService.UpdateAsync(employeeEntity);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _employeeRepositoryMock
                .Setup(x => x.DeleteAsync(id))
                .ReturnsAsync(true);

            // Act
            var result = await _employeeService.DeleteAsync(id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsEmployee()
        {
            // Arrange
            var id = Guid.NewGuid();
            var employeeEntity = new EmployeeEntity
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe"
            };

            _employeeRepositoryMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(employeeEntity);

            // Act
            var result = await _employeeService.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task GetByNameAsync_WithValidName_ReturnsEmployee()
        {
            // Arrange
            var name = "John";
            var employeeEntity = new EmployeeEntity
            {
                FirstName = name,
                LastName = "Doe"
            };

            _employeeRepositoryMock
                .Setup(x => x.GetByNameAsync(name))
                .ReturnsAsync(employeeEntity);

            // Act
            var result = await _employeeService.GetByNameAsync(name);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<EmployeeEntity>
            {
                new EmployeeEntity { FirstName = "John", LastName = "Doe" },
                new EmployeeEntity { FirstName = "Jane", LastName = "Smith" }
            };

            _employeeRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}