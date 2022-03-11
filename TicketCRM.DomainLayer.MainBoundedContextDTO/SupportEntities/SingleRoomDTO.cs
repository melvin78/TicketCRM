namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class SingleRoomDTO
    {
        public Guid Id { get; set; }
        
        public string TicketNumber { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public Guid RoomUsersIdFrom { get; set; }
        
        public Guid RoomUsersIdTo { get; set; }

        public  string RoomUsersUserNameFrom { get; set; }
        
        
        public string RoomUsersUserNameTo { get; set; }
        
        public bool LastMessageSaved { get;  set; }

        public bool LastMessageDistributed { get;  set; }

        public bool LastMessageSeen { get;  set; }

        public bool LastMessageNew { get;  set; }


    }
}