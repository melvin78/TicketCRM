namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class TicketInfoDTO
    {
        
        /// <summary>
        /// Dont change the casing of the property names.!important!
        /// </summary>
        public string ticketnumber { get; set; }
        
       
        public string description { get; set; }
        public string ticketstatus { get; set; }
        
        public string resolvedon { get; set; }
        
        public string firstmessage { get; set; }
        
        public object attachments { get; set; }

        public string enquirycategory { get; set; }
        
        public string openedon { get; set; }
        
        public string caretaker { get; set; }
        
        public string dateassigned { get; set; }
        
        public string closedon { get; set; }
        
        public string remarks { get; set; }
        
        public string reopeneddate { get; set; }
        
        public string saccoid { get; set; }
        
    }
}