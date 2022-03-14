namespace TicketCRM
{
    public class EmailSettings
    {
        public EmailSettings(string smtpServer, string smtpUserName, string smtpPassword, int smtpServerPort, string emailDisplayName, string sendersName)
        {
            SmtpServer = smtpServer;
            SmtpUserName = smtpUserName;
            SmtpPassword = smtpPassword;
            SmtpServerPort = smtpServerPort;
            EmailDisplayName = emailDisplayName;
            SendersName = sendersName;
            EmailDisplayName = emailDisplayName;
            SendersName = sendersName;
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