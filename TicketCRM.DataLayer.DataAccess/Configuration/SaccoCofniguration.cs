using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class SaccoCofniguration:IEntityTypeConfiguration<Sacco>
    {
        public void Configure(EntityTypeBuilder<Sacco> builder)
        {
            builder.HasIndex(o => o.Id);

            builder.Property(o => o.SaccoName).HasMaxLength(20);

            builder.ToTable("support_saccos");
        }
    }
}