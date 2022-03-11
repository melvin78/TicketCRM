using System.Threading.Tasks;
using MimeKit;

namespace IdentityServerAspNetIdentity.EmailService
{
    public interface IEmailIdentityServerService
    {

        Task SendEmailAsync(MimeEntity mimeEntity,string receiverEmailAddress,string subject);


    }
}