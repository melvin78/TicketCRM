namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.ForgotPassword
{
    public class ForgotPasswordCustomModel
    {
        public string ResetPasswordLink { get; set; }

        public ForgotPasswordCustomModel(string resetPasswordLink)
        {
            ResetPasswordLink = resetPasswordLink;

        }
    }
}