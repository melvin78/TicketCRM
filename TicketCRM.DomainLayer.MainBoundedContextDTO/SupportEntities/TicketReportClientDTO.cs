namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class TicketReportClientDTO
    {
        public string Id { get; set; }

        public string EnquiryCategoryVal { get; set; }
        
        public string TicketNo { get; set; }
        
        public string EnquiryType { get; set; }
        
        public  Guid CareTaker {get; set;}
        
        public string CareTakerName { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string Attachments { get; set; }
        
        public string TicketStatusVal { get; set; }

        public string CreatedAt { get; set; }
    }
}