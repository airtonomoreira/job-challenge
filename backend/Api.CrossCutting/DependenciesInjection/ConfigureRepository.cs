using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependenciesInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IEmployeeRepository, EmployeeImplementation>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            var databaseType = Environment.GetEnvironmentVariable("DATABASE")?.ToLower();
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            if (databaseType == "sqlserver")
            {
                serviceCollection.AddDbContext<AppDbContext>(
                    options => options.UseSqlServer(connectionString)
                );
            }
            else if (databaseType == "sqlite")
            {
                serviceCollection.AddDbContext<AppDbContext>(
                    options => options.UseSqlite(connectionString)
                );
            }
            else
            {
                throw new InvalidOperationException("Unsupported database type. Please set DATABASE environment variable to 'sqlserver' or 'sqlite'");
            }
        }
    }
}