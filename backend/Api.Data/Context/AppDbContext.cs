using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Mappings;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Data.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<UserEntity> Users { get; set; } 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlite("Data Source=employee.db",
                        b => b.MigrationsAssembly("Api.Application"))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information);
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employees");

            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
