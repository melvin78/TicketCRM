using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketEmailReportSummaryConfiguration:IEntityTypeConfiguration<TicketEmailReportSummary>
    {
        public void Configure(EntityTypeBuilder<TicketEmailReportSummary> builder)
        {
            builder.Ignore(o => o.Id);

            builder.Ignore(o => o.ModifiedDate);
            builder.HasNoKey();

            builder.Ignore(o => o.CreatedBy);
            
        }
    }
}