using TicketCRM.DomainLayer.MainBoundedContext.ValueObjects;

namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Chats:BaseEntity
    {
        
        public string? IndexId { get; set; }
        
        public string? Content { get; set; }
        

        public string? Date { get; set; }
        
        public string? ChatId { get; set; }
        
        public string? InboxId { get; set; }
        
        public Guid? SenderId { get; set; }
        
        public string? UserName { get; set; }

        public string? Avatar { get; set; }

        public string? TimeStamp { get; set; }
        
        public bool? System { get; set; }

        public bool? Saved { get; set; }

        public bool? Distributed { get; set; }

        public bool? Seen { get; set; }
        
        public bool? Deleted { get; set; }
        
        public bool? DisabledActions { get; set; }
        
        public bool? DisabledReactions { get; set; }
        
        public ChatFile ChatFile { get; set; }
        
        public  ReplyMessage ReplyMessage { get; set; }
    }
}