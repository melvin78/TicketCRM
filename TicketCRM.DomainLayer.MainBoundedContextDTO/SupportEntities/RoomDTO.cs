namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class RoomDTO
    {
        public string roomId { get; set; }
        //ticket no
        
        public string roomName { get; set; }
        //To
        
        public int UnreadCount { get; set; }
        //Is read
        
        public string avatar { get; set; }
        
        public LastMessageDTO LastMessage { get; set; }
        
        public string index { get; set; }
        
        public List<UserInfo> users { get; set; }
    }
}