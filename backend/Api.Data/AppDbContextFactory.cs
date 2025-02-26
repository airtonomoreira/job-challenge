using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=employee.db",
                b => b.MigrationsAssembly("Api.Application"))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
