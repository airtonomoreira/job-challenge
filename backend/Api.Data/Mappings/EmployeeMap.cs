using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.DocumentNumber).IsRequired();
            builder.Property(x => x.PhoneNumbers).IsRequired();
            builder.Property(x => x.ManagerId);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.HierarchicalLevel).IsRequired();
            
            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();
        }
    }
}
