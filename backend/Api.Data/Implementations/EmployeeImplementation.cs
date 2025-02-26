using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class EmployeeImplementation : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task<EmployeeEntity> GetAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<EmployeeEntity> InsertAsync(EmployeeEntity entity)
        {
            // Age validation
            if (entity.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException("Employee must be at least 18 years old.");
            }

            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // public async Task<EmployeeEntity> UpdateAsync(EmployeeEntity entity)
        // {
        //     // Age validation
        //     if (entity.DateOfBirth > DateTime.Now.AddYears(-18))
        //     {
        //         throw new ArgumentException("Employee must be at least 18 years old.");
        //     }

        //     // Hierarchical level check
        //     var currentEmployee = await _context.Employees.FindAsync(entity.Id);
        //     if (currentEmployee != null && currentEmployee.HierarchicalLevel < entity.HierarchicalLevel)
        //     {
        //         throw new ArgumentException("Cannot promote employee to a higher hierarchical level than their current manager.");
        //     }

        //     _context.Employees.Update(entity);
        //     await _context.SaveChangesAsync();
        //     return entity;
        // }

        public async Task<EmployeeEntity> UpdateAsync(EmployeeEntity entity)
        {
            // Validação de idade
            if (entity.DateOfBirth > DateTime.Now.AddYears(-18))
            {
                throw new ArgumentException("Employee must be at least 18 years old.");
            }

            // Carrega a entidade existente
            var existingEntity = await _context.Employees.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            // Validação de nível hierárquico
            if (existingEntity.HierarchicalLevel < entity.HierarchicalLevel)
            {
                throw new ArgumentException("Cannot promote employee to a higher hierarchical level than their current manager.");
            }

            // Aplica as alterações na entidade existente
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            return existingEntity;
        }
        public async Task<bool> DocumentNumberExistsAsync(string documentNumber)
        {
            return await _context.Employees.AnyAsync(e => e.DocumentNumber == documentNumber);
        }

        public async Task<EmployeeEntity> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.DocumentNumber == documentNumber);
        }

        public async Task<IEnumerable<EmployeeEntity>> GetByManagerIdAsync(Guid managerId)
        {
            return await _context.Employees
                .Where(e => e.ManagerId == managerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Manager)
                .ToListAsync();
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Employees.FindAsync(id);
            if (entity == null) return false;

            _context.Employees.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EmployeeEntity> GetByEmailAsync(string email)
        {
            return await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.User.Email == email);
        }

        public async Task<EmployeeEntity> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<EmployeeEntity> GetByNameAsync(string name)
        {

            return await _context.Employees.FirstOrDefaultAsync(e => (e.FirstName + " " + e.LastName).ToUpper() == name.ToUpper());
        }

        public async Task<EmployeeEntity> CreateAsync(EmployeeEntity employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public Task<bool> IsValidField(string fieldValue)
        {
            return Task.FromResult(string.IsNullOrEmpty(fieldValue));
        }
    }
}
