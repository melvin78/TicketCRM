using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class OrganizationCofniguration:IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasIndex(o => o.Id);

            builder.Property(o => o.OrganizationName).HasMaxLength(50);

            builder.ToTable("support_organizations");
        }
    }
}