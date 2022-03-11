namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class InboxDTO
    {
        public Guid Id { get; set; }
        
        public string Avatar { get; set; }
        public string TicketNumber { get; set; }
        
        public int UnreadCount { get; set; }
        
        
        public  string Index { get; set; }
        
        public string LastMessageContent { get;  set; }

        public Guid LastMessageSenderId { get;  set; }

        public string LastMessageUserName { get;  set; }

        public DateTime? LastMessageTimestamp { get;  set; }

        public bool LastMessageSaved { get;  set; }

        public bool LastMessageDistributed { get;  set; }

        public bool LastMessageSeen { get;  set; }

        public bool LastMessageNew { get;  set; }

        public Guid RoomUsersIdFrom { get; set; }
        
        public Guid RoomUsersIdTo { get; set; }

        public  string RoomUsersUserNameFrom { get; set; }
        
        public string RoomUsersUserNameTo { get; set; }
        
        
        public string RoomUsersAvatarFrom { get; set; }
        
        
        public string RoomUsersAvatarTo { get; set; }
        
    }
}