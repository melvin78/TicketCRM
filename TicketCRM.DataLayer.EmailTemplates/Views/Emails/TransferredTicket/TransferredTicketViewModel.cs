namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.TransferredTicket
{
    public class TransferredTicketViewModel
    {
        public string TicketNo { get; set; }

        public string Enquiry
        {
            get; set;
            
        }
        
        public string OldAgent { get; set; }


        public TransferredTicketViewModel(string ticketNo,string enquiry,string  oldAgent)
        {
            TicketNo = ticketNo;

            Enquiry = enquiry;

            OldAgent = oldAgent;

        }
    }
}