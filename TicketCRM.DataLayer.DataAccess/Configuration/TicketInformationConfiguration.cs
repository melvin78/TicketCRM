using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketInformationConfiguration:IEntityTypeConfiguration<TicketInformation>
    {
        public void Configure(EntityTypeBuilder<TicketInformation> builder)
        {

            builder.Ignore(o => o.Id);

            builder.Ignore(o => o.ModifiedDate);

            builder.Ignore(o => o.CreatedBy);

            builder.ToTable("ticket_information");

            builder.HasNoKey();
        }
    }
}