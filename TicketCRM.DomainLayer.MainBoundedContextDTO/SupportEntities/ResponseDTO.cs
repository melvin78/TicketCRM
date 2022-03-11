namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class ResponseDTO
    {
        public string ResponseText { get; set; }

        public  string Attachments { get; set; }

        public string TicketNumber { get; set; }
        
        public string SocketId { get; set; }
        
        public string CreatedAt { get; set; }
        
        public bool IsRead { get; set; }
        
        public Guid From { get; set; }

        public Guid To { get; set; }
        
        public Guid Id { get; set; }
    }
}