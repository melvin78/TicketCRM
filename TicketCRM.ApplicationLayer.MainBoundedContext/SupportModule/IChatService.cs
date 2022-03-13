using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IChatService
    {
        Task<int> AddChatAsync(ChatDTO chatDto);

        List<object> GetChatsAsync(string inboxId);
        
        
        
        Task<int> SendConversationEmail(string message,string attachments,string senderEmailAddress,string receiverEmailAddress,string ticketNumber,string url);
    }
}