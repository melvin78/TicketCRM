using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class InboxConfiguration:IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.HasIndex(o => o.Id);

            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.TicketNumber).IsUnique();
            
            builder.OwnsOne(o => o.LastMessage)
                .Property(o => o.SenderId).HasColumnType("char(36)");

            builder.OwnsOne(o => o.RoomUsers, t =>
            {
                t.Property(u => u.IdFrom).HasColumnType("char(36)");
                t.Property(u => u.IdTo).HasColumnType("char(36)");
            });



            builder.ToTable("support_inboxes");
        }
    }
}