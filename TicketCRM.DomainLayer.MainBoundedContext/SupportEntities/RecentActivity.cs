namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class RecentActivity:BaseEntity
    {
        public string EmailAddress { get; set; }

        public string TicketNumber { get; set; }
        
        public string Task { get; set; }
        
        public string Color { get; set; }
        
        public string Icon { get; set; }
        
        public Guid SaccoId { get; set; }
        
    }
}