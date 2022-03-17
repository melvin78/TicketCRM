using System;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using Xunit;

namespace TicketCRM.EfCoreTests.Tests;

public class TicketRepositoryTests
{
    [Fact]
    public void SaveAsync_Book_RightRecord()
    {
        var helper = new TicketDBHelper();

        // Repositories with InMemory Database
        var readyRepo = helper.GetInMemoryReadRepository();
        var writeRepo = helper.GetInMemoryWriteRepository();

        // use Write Repository to add mock data

        var ticket = new Ticket()
        {
            TicketNo = "CRM-2FC4-040257-174E",
            FirstMessage ="Hello,I am just checking to see if the tests are running :)",
            CareTaker = Guid.Parse("debfbd47-ffc8-478c-8f2e-4e98af9ba944"),
            TicketStatusId = Guid.Parse("0e06e836-471c-ec11-b063-14cb19ba19a9"),
            CustomerId = Guid.Parse("6aa1b670-2941-4f26-955a-7f6c236c8b3e"),
            EnquiryCategoryId = Guid.Parse("b7020354-0009-4268-8473-ac95d0e4815b"),
            EnquiryId = Guid.Parse("b01e2bad-e787-4777-a2f6-8219e609c503"),
            OrganizationId=Guid.Parse("2fc42189-9497-4638-b5de-22ce8d46c595"),
            Attachments = "[{\"filename\":\"6db9c66b-86ea-4d18-8e73-d0b35c66bfb7-9056\",\"type\":\"application/pdf\"}]",
            Remarks = "",
            ClosedOn = null,
            ResolvedOn = null,
            PriorityLevel = 1,
            ReopenedOn = null,
            CancelledOn = null,
            TransferredOn = null,
            AssignedOn = null,
            PastDueDate = null,
            ExpectedDueDate = null

        };
        writeRepo.AddAsync(ticket);

        // Commit insert
        writeRepo.SaveAsync().GetAwaiter();

        // use Specification in Read Repository and get data
        var result = readyRepo.GetAsync(new
            TicketSpecification(ticket.TicketNo)).Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("CRM-2FC4-040257-174E", result.TicketNo);
        Assert.Equal("Hello,I am just checking to see if the tests are running :)", result.FirstMessage);
        Assert.Equal(Guid.Parse("debfbd47-ffc8-478c-8f2e-4e98af9ba944"), result.CareTaker);
        Assert.Equal(Guid.Parse("0e06e836-471c-ec11-b063-14cb19ba19a9"),result.TicketStatusId);
        Assert.Equal( Guid.Parse("6aa1b670-2941-4f26-955a-7f6c236c8b3e"), result.CustomerId);
        Assert.Equal(Guid.Parse("b7020354-0009-4268-8473-ac95d0e4815b"),result.EnquiryCategoryId);
        Assert.Equal(Guid.Parse("b01e2bad-e787-4777-a2f6-8219e609c503"),result.EnquiryId);
        Assert.Equal( Guid.Parse("2fc42189-9497-4638-b5de-22ce8d46c595"),result.OrganizationId);
        Assert.Equal("[{\"filename\":\"6db9c66b-86ea-4d18-8e73-d0b35c66bfb7-9056\",\"type\":\"application/pdf\"}]",result.Attachments);
        Assert.Empty(result.Remarks);
        Assert.Null(result.ClosedOn);
        Assert.Null(result.ResolvedOn);
        Assert.Equal(1,result.PriorityLevel);
        Assert.Null(result.ReopenedOn);
        Assert.Null(result.CancelledOn);
        Assert.Null(result.TransferredOn);
        Assert.Null(result.AssignedOn);
        Assert.Null(result.PastDueDate);
        Assert.Null(result.ExpectedDueDate);



    }

 
}