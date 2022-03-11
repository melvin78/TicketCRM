namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.RegisteredAccount
{
    public class NewAccountViewModel
    {
        public string ConfirmationUrl { get; set; }

        public NewAccountViewModel(string confirmationUrl)
        {
            ConfirmationUrl = confirmationUrl;

        }
    }
}