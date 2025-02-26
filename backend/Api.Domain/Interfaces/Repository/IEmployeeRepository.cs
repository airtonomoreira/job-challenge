using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository : IRepository<EmployeeEntity>
    {
        Task<bool> DocumentNumberExistsAsync(string documentNumber);
        Task<bool> IsValidField(string fieldValue);
        Task<EmployeeEntity> GetByEmailAsync(string email); // Added method
        Task<EmployeeEntity> GetByDocumentNumberAsync(string documentNumber);
        Task<IEnumerable<EmployeeEntity>> GetByManagerIdAsync(Guid managerId);

        Task<EmployeeEntity> GetByIdAsync(Guid id);
        Task<EmployeeEntity> GetByNameAsync(string name);
        Task<EmployeeEntity> CreateAsync(EmployeeEntity entity);
    }
}
