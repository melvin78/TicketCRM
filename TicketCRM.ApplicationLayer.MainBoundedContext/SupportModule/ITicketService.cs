using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface ITicketService
    {
        Task<TicketDetailsDTO> AddNewTicket(TicketDTO ticketDto);

       void SendReminderNotification(CancellationToken cancellationToken);
        
        Task<List<TicketInfoDTO>> GetClosedTicketInformation();

        Task<string> GetCareTakerAsync(string ticketNo);


        Task<bool> SendSupportEmail(string ticketNumber, string clientEmailAddress, string enquiry, string issue,
            string url,List<string> agentsAddressed,string subject);

        Task<bool> SendTransferredEmailNotification(string ticketNo, string enquiry, string oldAgent,
            string receiverEmailAddress);

        Task<bool> TransferTicket(string ticketNo, string agentId);

        Task<bool> SendEmailNotification(TicketDetailsDTO ticketDetailsDto, string receiverEmailAddress);

        Task<bool> SendResolvedEmailNotification(TicketDetailsDTO ticketDetailsDto, string receiverEmailAddress);

        Task<IEnumerable<Ticket>> FindTicketUserDetails(TicketDetailsDTO ticketDetailsDto);

        int FindTransferredTickets();


        int FindOpenedTickets();

        int FindResolvedTickets();

        int FindOverdueTickets();

        int FindNewTickets();

        int FindClosedTickets();

        int FindReopenedTickets();
        
        int FindSaccoTransferredTickets(Guid saccoId);

        int FindSaccoReopenedTickets(Guid saccoId);

        int FindSaccoOpenedTickets(Guid saccoId);

        int FindSaccoResolvedTickets(Guid saccoId);

        int FindSaccoOverdueTickets(Guid saccoId);

        int FindSaccoNewTickets(Guid saccoId);

        int FindSaccoClosedTickets(Guid saccoId);

        Task<bool> ResolveTicket(string ticketNo);

        bool ReOpenTicket(string ticketNo, string remarks);

        bool CloseTicket(string ticketNo);

        bool MarkTicketAsPending(string ticketNo);


        bool AssignAgentToUser(string ticketNo, Guid agentId);

        Task<bool> ResolveSupportTicket(string ticketNo);

        Task<string> GetFirstMessage(string ticketNo);

        Task<List<string>> GetAllTicketsAssignedToAgent(Guid agentId);

        Task<List<string>> GetAllTicketsOpenedByUser(Guid userId);

        Task<List<TicketInfoDTO>> GetAllTicketInformationByCustomerId(Guid customerId);

        Task<List<TicketInfoDTO>> GetResolvedTicketInformation();


        void MarkTicketsAsOverdue(CancellationToken cancellationToken);
        
        void SendOverdueTicketEmail(string ticketNo,string firstMessage,string url,string careTaker,string receiverEmailAddress);
    }
}