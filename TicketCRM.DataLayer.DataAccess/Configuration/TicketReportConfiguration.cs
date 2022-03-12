using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketReportConfiguration:IEntityTypeConfiguration<TicketReport>
    {
        public void Configure(EntityTypeBuilder<TicketReport> builder)
        {
            builder.HasNoKey();

            builder.Ignore(o => o.CreatedBy);
            
            builder.Property(o => o.SaccoName).HasColumnName("saccoName");
            
        }
    }
}