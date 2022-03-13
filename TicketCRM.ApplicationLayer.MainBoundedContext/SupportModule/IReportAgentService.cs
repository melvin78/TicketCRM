using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IReportAgentService
    {
        Task<List<TicketReport>> GetTicketReports();

        List<TicketSummary> GetTicketSummaries(Guid ticketStatus);


        List<TicketSummary> FindTicketSummaryBySaccoId(Guid ticketStatus, Guid SaccoId);

        TicketEmailReportSummary GetEmailTicketReportSummaries(string ticketNo);
        
        Task<List<TicketSummary>> FindResolvedTicketsBySaccoId(string saccoId);


        Task<List<TicketSummary>> FindAllResolvedTickets();


    }
}