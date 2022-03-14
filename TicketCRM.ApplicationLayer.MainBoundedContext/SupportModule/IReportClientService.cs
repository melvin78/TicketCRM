using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IReportClientService
    {
        Task<List<TicketReport>> GetTicketReports();

        TicketReport? GetTicketReportsByTicketNo(string ticketNo);

        List<TicketReport> GetAllTicketReportsByUserId(Guid userId);

    }
}