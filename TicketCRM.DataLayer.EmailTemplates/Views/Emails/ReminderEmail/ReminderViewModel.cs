namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.ReminderEmail
{
    public class ReminderViewModel
    {
       
        public string TicketNo { get; set; }
        
        public int OverdueBy { get; set; }
        
        public string Url { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string RaisedBy { get; set; }
        
        public string Organization { get; set; }
        
        
        public ReminderViewModel(string ticketNo,int overdueBy,string url,string firstMessage,string raisedBy,string organization)
        {
            TicketNo = ticketNo;

            OverdueBy = overdueBy;

            Url = url;

            FirstMessage = firstMessage;

            RaisedBy = raisedBy;

            Organization = organization;

        }
    }
}