namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Priority:BaseEntity
    {
        public int Level { get; set; }

        public string Description { get; set; }
        
    }
}