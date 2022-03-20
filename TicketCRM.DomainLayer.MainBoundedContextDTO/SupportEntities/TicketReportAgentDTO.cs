namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class TicketReportAgentDTO
    {
        public string EmailAddress { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public string OrganizationName { get; set; }
        
        public string EnquiryCategoryVal { get; set; }
        
        public string TicketNo { get; set; }
        
        public string EnquiryType { get; set; }
        
        public string EnquiryMessage { get; set; }
        
        public string Attachments { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}