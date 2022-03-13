namespace Centrino.ApplicationLayer.MainBoundedContext.EmailModule
{
    public class EmailSettings
    {
        public EmailSettings(string smtpServer, string smtpUserName, string smtpPassword, int smtpServerPort)
        {
            SmtpServer = smtpServer;
            SmtpUserName = smtpUserName;
            SmtpPassword = smtpPassword;
            SmtpServerPort = smtpServerPort;
        }

        public EmailSettings()
        {
        }

        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpServerPort { get; set; }

        public bool EnableSsl { get; set; }

        public string EmailDisplayName { get; set; }

        public string SendersName { get; set; }
    }
}