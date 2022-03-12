using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketSummaryConfiguration:IEntityTypeConfiguration<TicketSummary>
    {
        public void Configure(EntityTypeBuilder<TicketSummary> builder)
        {
            builder.HasNoKey();

            builder.ToTable("ticket_summaries");

            builder.Ignore(o => o.CreatedBy);
        }
    }
}