namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class EnquiryCategory:BaseEntity
    {
        public string EnquiryCategoryVal { get; set; }

        public Guid EnquiryId { get; set; }
        
        public Enquiries Enquiries { get; set; }
        
        public Ticket Tickets { get; set; }



    }
}