using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class RecentActivityConfiguration:IEntityTypeConfiguration<RecentActivity>
    {
        public void Configure(EntityTypeBuilder<RecentActivity> builder)
        {
            builder.HasIndex(o => o.Id);

            builder.ToTable("support_recent_activity");
        }
    }
}