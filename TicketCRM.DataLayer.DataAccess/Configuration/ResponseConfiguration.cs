using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class ResponseConfiguration:IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.Property(o => o.ResponseText).HasColumnType("LONGTEXT");

            builder.HasIndex(o => o.Id);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Attachments).HasColumnType("LONGTEXT");

            builder.Property(o => o.InboxId).HasColumnType("char(36)");

            builder.ToTable("support_responses");
        }
    }
}