using Microsoft.EntityFrameworkCore;
using TicketCRM.EfCoreTests.Contracts;
using TicketCRM.EfCoreTests.Repositories;

namespace TicketCRM.EfCoreTests;

public class TicketDBHelper
{
    private readonly TicketCRMDBContext _ticketCrmdbContext;
    public TicketDBHelper()
    {
        var builder = new DbContextOptionsBuilder<TicketCRMDBContext>();
        builder.UseInMemoryDatabase(databaseName: "LibraryDbInMemory");

        var dbContextOptions = builder.Options;
        _ticketCrmdbContext = new TicketCRMDBContext(dbContextOptions);
        // Delete existing db before creating a new one
        _ticketCrmdbContext.Database.EnsureDeleted();
        _ticketCrmdbContext.Database.EnsureCreated();
    }
    
    public IGenericReadRepository GetInMemoryReadRepository()
    {
        return new GenericReadRepository(_ticketCrmdbContext);
    }

    public IGenericWriteRepository GetInMemoryWriteRepository()
    {
        return new GenericWriteRepository(_ticketCrmdbContext);
    }
}