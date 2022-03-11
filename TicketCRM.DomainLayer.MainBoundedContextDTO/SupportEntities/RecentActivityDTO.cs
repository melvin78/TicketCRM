namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class RecentActivityDTO
    {

        public string emailAddress { get; set; }

        public string ticketNo { get; set; }
        
        public string task { get; set; }

        public bool show { get; set; } = true;
        
        public string color { get; set; }
        
        public string icon { get; set; }
        
        public Guid saccoid { get; set; }
    }
}