using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class ChatsConfiguration:IEntityTypeConfiguration<Chats>
    {
        public void Configure(EntityTypeBuilder<Chats> builder)
        {
            builder.HasIndex(o => o.Id);
            

            builder.Property(o => o.SenderId).HasColumnType("char(36)");

            builder.OwnsOne(o => o.ChatFile);

            builder.OwnsOne(o => o.ReplyMessage, t =>
            {
                t.OwnsOne(o => o.ChatFile);
                t.Property(u => u.SenderId).HasColumnType("char(36)");
            });
                
            builder.ToTable("support_chats");

        }
    }
}