namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class ChatResponseDTO
    {
        public string TicketNumber { get; set; }

        public Guid Id { get; set; }
        
        public string ResponseText { get; set; }

        public  string Attachments { get; set; }

        public Guid From { get; set; }

        public Guid To { get; set; }

    }
}