namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class OverdueTicketDTO
    {
        public string TicketNo { get; set; }

        public string FirstMessage { get; set; }

        public Guid? CareTaker { get; set; }

        public Guid CustomerId { get; set; }

        public Guid EnquiryCategoryId { get; set; }

        public string Attachments { get; set; }

        public int PriorityLevel { get; set; }

    }
}