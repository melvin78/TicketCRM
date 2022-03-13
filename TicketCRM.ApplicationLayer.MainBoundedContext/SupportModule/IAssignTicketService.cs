using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IAssignTicketService
    {
        Task<bool> SendEmailNotificationAsync(string ticketNo, string receiverEmailAddress);

        bool SendEmailNotification(string ticketNo,
            string receiverEmailAddress,
            string firstMessage,
            string enquiry,
            string createdOn,
            string clientEmailAddress,
            string url);

        List<TicketDetailsDTO> GetAllUnassignedTickets();

        List<Agent> GetAgentList();


        void AssignRoundRobin(CancellationToken cancellationToken);

        void SendEmail(CancellationToken cancellationToken);
        
        void MarkTicketsAsOverdue(CancellationToken cancellationToken);


        void SendPusher(CancellationToken cancellationToken);
        
        void SaveActivity(CancellationToken cancellationToken);

        void CreateRoom(CancellationToken cancellationToken);

    }
}