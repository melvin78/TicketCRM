namespace TicketCRM.DomainLayer.MainBoundedContext.ValueObjects
{
    public class LinkedResource:ValueObject<LinkedResource>
    {
        public LinkedResource(string contentId,string contentPath,string contentType)
        {
            ContentId = contentId;
            ContentPath = contentPath;
            ContentType = contentType;
        }

    
        public string ContentId { get; set; }
        public string ContentPath { get; set; }
        public string ContentType { get; set; }
    }
}