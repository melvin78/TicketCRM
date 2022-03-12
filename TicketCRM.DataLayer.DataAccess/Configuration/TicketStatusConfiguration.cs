using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketStatusConfiguration:IEntityTypeConfiguration<TicketStatus>
    {
        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {
            builder.HasIndex(o => o.Id);
            
            builder.Property(o => o.TicketStatusVal).HasMaxLength(20);

            builder.ToTable("support_ticketstatuses");
        }
    }
}