using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface ITicketStatusService
    {
        Task<int> AddNewTicketStatus(TicketStatusDTO ticketStatusDto);
        
        Task<List<TicketStatus>> GetTicketStatus();
    }
}