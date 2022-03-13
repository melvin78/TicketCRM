using MimeKit;

namespace TicketCRM
{
    public interface IEmailService
    {

            
        Task SendEmailAsync(MimeEntity mimeEntity,string receiverEmailAddress,string subject);
        
        void SendEmail(MimeEntity mimeEntity,string receiverEmailAddress,string subject);



    }
}