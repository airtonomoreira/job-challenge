using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services
{
    public interface IAdminService
    {
        Task<EmployeeEntity> CreateEmployeeAsync(EmployeeDtoCreate employeeDto);
        Task DeleteEmployeeAsync(Guid id);
    }
}
