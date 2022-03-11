namespace TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities
{
    public class LastMessageDTO
    {
        /// <summary>
        /// Response text
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// From
        /// </summary>
        
        public string senderId { get; set; }
        /// <summary>
        /// to
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        
        public string timeStamp { get; set; }
        
        public bool seen { get; set; }
        

    }
}