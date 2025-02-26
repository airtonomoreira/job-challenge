using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeEntity> CreateAsync(EmployeeEntity employee);
        Task<EmployeeEntity> UpdateAsync(EmployeeEntity employee);
        Task<bool> DeleteAsync(Guid id);
        Task<EmployeeEntity> GetByIdAsync(Guid id);
        Task<EmployeeEntity> GetByNameAsync(string name);
        Task<IEnumerable<EmployeeEntity>> GetAllAsync();
    }
}
