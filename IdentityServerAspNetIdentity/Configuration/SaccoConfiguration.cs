using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class SaccoConfiguration:IEntityTypeConfiguration<Sacco>
    {
        public void Configure(EntityTypeBuilder<Sacco> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.Id);

            builder.Property(o => o.Id).HasColumnType("char(36)");
            
            builder.ToTable("support_saccos");
        }
    }
}