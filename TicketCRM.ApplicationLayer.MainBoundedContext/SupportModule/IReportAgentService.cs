using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IReportAgentService
    {
        Task<List<TicketReport>> GetTicketReports();

        List<TicketSummary> GetTicketSummaries(Guid ticketStatus);


        List<TicketSummary> FindTicketSummaryByOrganizationId(Guid ticketStatus, Guid organizationId);

        TicketEmailReportSummary GetEmailTicketReportSummaries(string ticketNo);
        
        Task<List<TicketSummary>> FindResolvedTicketsByOrganizationId(string organizationId);


        Task<List<TicketSummary>> FindAllResolvedTickets();


    }
}