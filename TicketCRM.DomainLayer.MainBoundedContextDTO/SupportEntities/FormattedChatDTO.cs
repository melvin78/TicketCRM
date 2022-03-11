namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class FormattedChatDTO
    {
        public int  _id { get; set; } 
        
        public string content { get; set; }

        public Guid senderId  { get; set; }

        public string username { get; set; }
        
        public  string avatar { get; set; }
        
        public string   date { get; set; }
         
        public string timestamp { get; set; }
    
        public string files { get; set; }
            
    }
}