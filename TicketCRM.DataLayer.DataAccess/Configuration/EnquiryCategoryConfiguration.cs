using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class EnquiryCategoryConfiguration:IEntityTypeConfiguration<EnquiryCategory>
    {
        public void Configure(EntityTypeBuilder<EnquiryCategory> builder)
        {

            builder.HasIndex(o => o.Id);

            builder.Property(o => o.EnquiryCategoryVal).HasMaxLength(20);
            
            builder.Property(o => o.EnquiryId).HasColumnType("char(36)");

            builder.
                HasOne(o => o.Enquiries)
                .WithMany(o => o.EnquiryCategories)
                .HasForeignKey(o => o.EnquiryId);

            builder.ToTable("support_enquirycategories");

        }
    }
}