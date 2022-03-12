using System.Reflection;
using IdentityServerAspNetIdentity.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketCRM.DataAccess.Configuration
{
    public class TicketCRMDbContext : DbContext
    {
        public TicketCRMDbContext(DbContextOptions<TicketCRMDbContext> options) : base(options)
        {
        }
        
   
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ApplicationUser>().ToTable("aspnetusers");
        }
    }
}