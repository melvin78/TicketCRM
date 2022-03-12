using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketAssignmentConfiguration:IEntityTypeConfiguration<TicketAssignment>
    {
        public void Configure(EntityTypeBuilder<TicketAssignment> builder)
        {
            builder.HasNoKey();

            builder.Ignore(o => o.CreatedBy);

            builder.Ignore(o => o.CreatedAt);

            builder.Ignore(o => o.ModifiedDate);

            builder.Ignore(o => o.Id);
            

        }
    }
}