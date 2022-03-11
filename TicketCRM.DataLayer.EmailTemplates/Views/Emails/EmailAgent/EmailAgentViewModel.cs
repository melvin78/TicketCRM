namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.EmailAgent
{
    public class EmailAgentViewModel
    {
        public string TicketNo { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string Enquiry { get; set; }
        
        public string CreatedOn { get; set; }
        
        public string ClientEmailAddress { get; set; }
        
        public string Url { get; set; }
        
        public EmailAgentViewModel(string ticketNo,string firstMessage,
            string enquiry,string createdOn,string clientEmailAddress,string url)
        {

            TicketNo= ticketNo;

            FirstMessage = firstMessage;

            Enquiry = enquiry;

            CreatedOn = createdOn;

            ClientEmailAddress = clientEmailAddress;

            Url = url;
        }
        
    }
}