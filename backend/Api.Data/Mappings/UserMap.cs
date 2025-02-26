using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.HierarchyLevel).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
