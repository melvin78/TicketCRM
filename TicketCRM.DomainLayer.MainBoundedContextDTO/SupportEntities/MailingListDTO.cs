using System.ComponentModel;

namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class MailingListDTO
    {
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [DisplayName("Subscribed")]
        public bool IsSubscribed { get; set; }
    }
}