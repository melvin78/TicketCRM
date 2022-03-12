using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class AgentConfiguration:IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.Id);

            builder.Property(o => o.UserId).HasColumnType("char(36)");

            builder.ToTable("support_agents");

        }
    }
}