namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class TicketDetailsDTO
    {
        public string TicketNo { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public Guid CustomerId { get; set; }
        
        
        public Guid? CareTaker { get; set; }

        
        public Guid EnquiryCategoryId { get; set; }

    }
}