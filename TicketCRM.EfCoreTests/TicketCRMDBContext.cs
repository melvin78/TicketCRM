using System.Reflection;
using IdentityServerAspNetIdentity.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketCRM.EfCoreTests;

public class TicketCRMDBContext:DbContext
{
    public TicketCRMDBContext(DbContextOptions<TicketCRMDBContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ApplicationUser>().ToTable("aspnetusers");
    }
}