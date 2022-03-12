using TicketCRM.DomainLayer.MainBoundedContext.ValueObjects;

namespace TicketCRM.DomainLayer.MainBoundedContext.SupportEntities
{
    public class Inbox:BaseEntity
    {
        public string TicketNumber { get; set; }
        
        public int UnreadCount { get; set; }
        
        
        public  string Index { get; set; }
        
        
        public string Avatar { get; set; }
        
        
        public virtual LastMessage LastMessage { get; set; }
        
        public virtual RoomUsers RoomUsers { get; set; }
        
    }
}