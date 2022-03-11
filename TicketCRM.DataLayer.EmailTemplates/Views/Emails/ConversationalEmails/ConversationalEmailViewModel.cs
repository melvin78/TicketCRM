namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.ConversationalEmails
{
    public class ConversationalEmailViewModel
    {
        public string Message { get; set; }
        
        public string Attachments { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string TicketNumber { get; set; }
        
        
        public string Url { get; set; }
        
        public ConversationalEmailViewModel(string message,string attachments,string emailAddress,string ticketNumber,string url)
        {
            Message = message;

            Attachments = attachments;

            EmailAddress = emailAddress;
            
            TicketNumber = ticketNumber;

            Url = url;

        }
    }
}