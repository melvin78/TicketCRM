using System;

namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.TicketEmails
{
    public class TicketNumberViewModel
    {
        public string TicketNo { get; set; }

        public DateTime DateTime
        {
            get;
            set;
        }

        public TicketNumberViewModel(string ticketno, DateTime dateTime)
        {
            TicketNo = ticketno;
            DateTime = dateTime;

        }
    }
}