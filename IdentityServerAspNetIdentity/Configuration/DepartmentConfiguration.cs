using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class DepartmentConfiguration:IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(o => o.DepartmentVal).HasMaxLength(20);

            builder.HasIndex(o => o.Id);

            builder.ToTable("support_departments");
        }
    }
}