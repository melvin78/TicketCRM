using System.Collections.Generic;

namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.SupportEmail
{
    public class SupportEmailViewModel
    {
        public string TicketNumber { get; set; }
        
        public string ClientEmailAddress { get; set; }
        
        public string Enquiry { get; set; }
        
        public string Issue { get; set; }

        public string Attachments { get; set; }

        public List<string> AgentsAddressed { get; set; }

        public string Url { get; set; }

        public SupportEmailViewModel(string ticketNumber,string clientEmailAddress,
            string enquiry,string issue,string url,List<string> agentsAddressed)
        {
            TicketNumber = ticketNumber;

            ClientEmailAddress = clientEmailAddress;

            Enquiry = enquiry;

            Issue = issue;

            AgentsAddressed = agentsAddressed;
            
            Url = url;


        }
    }
}