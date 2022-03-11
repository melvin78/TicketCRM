using System.Collections.Generic;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.DataLayer.EmailTemplates.Views.Emails.ConfirmAccount
{
    public class ConfirmAccountEmailViewModel
    {
        public List<MailingListDTO> MailingListDtos { get; set; }

        public ConfirmAccountEmailViewModel(List<MailingListDTO> mailingListDto)
        {
            MailingListDtos = mailingListDto;

        }
         

        public string ConfirmEmailUrl { get; set; }
        
   
    
    }
}
