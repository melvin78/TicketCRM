using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServerAspNetIdentity.Configuration;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasIndex(o => o.Id);

        builder.Property(o => o.Id).HasColumnType("char(36)");

        builder.ToTable("support_organization");
    }
}

