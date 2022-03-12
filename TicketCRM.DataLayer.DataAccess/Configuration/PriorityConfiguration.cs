using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    
    
    
    public class PriorityConfiguration:IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.HasIndex(o => o.Id);
            
            builder.ToTable("support_priority");
        }
    }
}