using System.Threading.Tasks;
using MimeKit;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace IdentityServerAspNetIdentity.EmailService
{
    public interface IEmailTemplateResolver<TModel>
    {
        
       

        Task<BodyBuilder> BuildEmailBodyAsync(MailViewModelDTO mailViewModelDto, TModel model);


    }
}