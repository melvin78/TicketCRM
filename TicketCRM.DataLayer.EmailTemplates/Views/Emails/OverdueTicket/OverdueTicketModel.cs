namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.OverdueTicket
{
    public class OverdueTicketModel
    {
        public string TicketNo { get; set; }

        public string FirstMessage { get; set; }

        public string Url { get; set; }

        public string CareTaker { get; set; }
        
        public OverdueTicketModel(string ticketNo,string firstMessage,string url,string careTaker)

        {

            TicketNo = ticketNo;

            FirstMessage = firstMessage;

            Url = url;

            CareTaker = careTaker;

        }
    }
}