using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface ITicketAssignmentService
    {
        Task<List<TicketAssignment>> GetAllAssignedTickets();

        TicketAssignment GetAssignedTicketReportByTicketNo(string ticketNo);
        
        Task<List<TicketAssignment>> GetAssignedTicketReportByAgentId(Guid agentId);

    }
}