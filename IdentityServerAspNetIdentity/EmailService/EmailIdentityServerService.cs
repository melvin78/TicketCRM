using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;

namespace IdentityServerAspNetIdentity.EmailService
{

    public class EmailIdentityServerService: IEmailIdentityServerService
    {
        private readonly EmailIdentityServerSettings _emailIdentityServerSettings;



        public EmailIdentityServerService(EmailIdentityServerSettings emailIdentityServerSettings)
        {
            _emailIdentityServerSettings = emailIdentityServerSettings;
         
        }


        public async Task SendEmailAsync(MimeEntity mimeEntity,string receiverEmailAddress,string subject)
        {
    
            
            var emailMessage = new MimeMessage()
            {
                 Sender = new MailboxAddress(_emailIdentityServerSettings.SendersName,_emailIdentityServerSettings.SmtpUserName),
                 Subject = subject
                 
            };
            
   


     
            emailMessage.From.Add(new MailboxAddress(_emailIdentityServerSettings.EmailDisplayName,_emailIdentityServerSettings.SmtpUserName));
            // emailMessage.Body = new TextPart(TextFormat.Html) { Text = razorString };
            emailMessage.Body = mimeEntity;

            var toAddress = receiverEmailAddress;
            
            emailMessage.To.Add(MailboxAddress.Parse(toAddress));

            
            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOption = _emailIdentityServerSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_emailIdentityServerSettings.SmtpServer, _emailIdentityServerSettings.SmtpServerPort, socketOption);

                    if (!string.IsNullOrEmpty(_emailIdentityServerSettings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_emailIdentityServerSettings.SmtpUserName, _emailIdentityServerSettings.SmtpPassword);
                    }

                    await smtp.SendAsync(emailMessage);
                    await smtp.DisconnectAsync(true);
                }

            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }



    }
}