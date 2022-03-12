using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class EnquiriesConfiguration:IEntityTypeConfiguration<Enquiries>
    {


        public void Configure(EntityTypeBuilder<Enquiries> builder)
        {
            builder.HasIndex(o => o.Id);
            
            builder.Property(o => o.EnquiryType).HasMaxLength(20);

            builder.ToTable("support_enquiries");
        }
    }
}