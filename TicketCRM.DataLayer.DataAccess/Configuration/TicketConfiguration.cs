using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketConfiguration:IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasIndex(o => o.Id);
            
            builder.Property(o => o.CustomerId).HasColumnType("char(36)");

            builder.Property(o => o.CareTaker).HasColumnType("char(36)");

            builder.Property(o => o.SaccoId).HasColumnType("char(36)");

            builder.Property(o => o.EnquiryCategoryId).HasColumnType("char(36)");

            builder.Property(o => o.TicketStatusId).HasColumnType("char(36)");

            builder.Property(o => o.FirstMessage).HasColumnType("longtext");

            builder.HasMany(o => o.EnquiryCategory)
                .WithOne(o => o.Tickets)
                .HasForeignKey(o => o.Id);

            builder.HasMany(o => o.Sacco)
                .WithOne(o => o.Ticket)
                .HasForeignKey(o => o.Id);
            
            builder.HasMany(o => o.TicketStatus)
                .WithOne(o => o.Ticket)
                .HasForeignKey(o => o.Id);
            
               

            builder.ToTable("support_tickets");

        }
    }
}