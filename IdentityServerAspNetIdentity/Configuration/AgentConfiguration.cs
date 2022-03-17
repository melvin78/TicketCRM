using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServerAspNetIdentity.Configuration
{
    public class AgentConfiguration:IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.Id);

            builder.Property(o => o.UserId).HasColumnType("char(36)");

            builder.Property(o => o.OrganizationId).HasColumnType("char(36)");

            builder.ToTable("support_agents");

        }
    }
}