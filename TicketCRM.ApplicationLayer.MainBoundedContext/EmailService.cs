using System.Net.Mail;
using Centrino.ApplicationLayer.MainBoundedContext.EmailModule;
using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TicketCRM
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;


        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }


        public async Task SendEmailAsync(MimeEntity mimeEntity, string receiverEmailAddress, string subject)
        {
            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_emailSettings.SendersName, _emailSettings.SmtpUserName),
                Subject = subject
            };


            emailMessage.From.Add(new MailboxAddress(_emailSettings.EmailDisplayName, _emailSettings.SmtpUserName));
            // emailMessage.Body = new TextPart(TextFormat.Html) { Text = razorString };
            emailMessage.Body = mimeEntity;

            var toAddress = receiverEmailAddress;

            emailMessage.To.Add(MailboxAddress.Parse(toAddress));


            try
            {
                using (var smtp = new SmtpClient())
                {
                    var socketOption = _emailSettings.EnableSsl
                        ? SecureSocketOptions.StartTls
                        : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpServerPort, socketOption);

                    if (!string.IsNullOrEmpty(_emailSettings.SmtpUserName))
                        await smtp.AuthenticateAsync(_emailSettings.SmtpUserName, _emailSettings.SmtpPassword);

                    await smtp.SendAsync(emailMessage);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }

        public void SendEmail(MimeEntity mimeEntity, string receiverEmailAddress, string subject)
        {
            var emailMessage = new MimeMessage
            {
                Sender = new MailboxAddress(_emailSettings.SendersName, _emailSettings.SmtpUserName),
                Subject = subject
            };


            emailMessage.From.Add(new MailboxAddress(_emailSettings.EmailDisplayName, _emailSettings.SmtpUserName));
            // emailMessage.Body = new TextPart(TextFormat.Html) { Text = razorString };
            emailMessage.Body = mimeEntity;

            var toAddress = receiverEmailAddress;

            emailMessage.To.Add(MailboxAddress.Parse(toAddress));


            try
            {
                using (var smtp = new SmtpClient())
                {
                    var socketOption = _emailSettings.EnableSsl
                        ? SecureSocketOptions.StartTls
                        : SecureSocketOptions.Auto;
                    smtp.Connect(_emailSettings.SmtpServer, _emailSettings.SmtpServerPort, socketOption);

                    if (!string.IsNullOrEmpty(_emailSettings.SmtpUserName))
                        smtp.Authenticate(_emailSettings.SmtpUserName, _emailSettings.SmtpPassword);

                    smtp.Send(emailMessage);
                    smtp.Disconnect(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}