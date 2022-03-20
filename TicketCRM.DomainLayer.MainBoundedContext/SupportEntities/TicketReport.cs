namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class TicketReport:BaseEntity
    {
        public string EmailAddress { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        
        public string TicketStatusVal { get; set; }
        
        public string FirstMessage { get; set; }
        
        public string UserId { get; set; }
        
        public  string CareTakerName { get; set; }
        
        public string OrganizationName { get; set; }
        
        
        public string EnquiryCategoryVal { get; set; }
        
        public string TicketNo { get; set; }
        
        public  Guid CareTaker {get; set;}
        
        public string EnquiryType { get; set; }

        public string Attachments { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        
        
        
        
        
        
    }
}