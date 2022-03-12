namespace TicketCRM.DomainLayer.MainBoundedContext.ValueObjects
{
    public class LastMessage:ValueObject<LastMessage>
    {
        public string Content { get; private set; }

        public Guid? SenderId { get; private set; }

        public string UserName { get; private set; }

        public DateTime? Timestamp { get; private set; }

        public bool Saved { get; private set; }

        public bool Distributed { get; private set; }

        public bool Seen { get; private set; }

        public bool New { get; private set; }


        public LastMessage(string content,Guid? senderId,string userName,DateTime? timestamp,
            bool saved,bool distributed,bool seen,bool newStatus)
        {
            Content = content;

            SenderId = senderId;

            UserName = userName;

            Timestamp = timestamp;

            Saved = saved;

            Distributed = distributed;

            Seen = seen;

            New = newStatus;

        }

        public LastMessage()
        {
            
        }
    }
}