using System;

namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.ResolvedTicket
{
    public class ResolvedTicketViewModel
    {
        public string TicketNumber { get; set; }

        public DateTime DateTime { get; set; }
        
        public ResolvedTicketViewModel(string ticketNumber,DateTime dateTime)
        {
            TicketNumber = ticketNumber;

            DateTime = dateTime;

        }
    }
}