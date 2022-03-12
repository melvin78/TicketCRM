using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class ResponseFactory
    {
        public static Response AddNewResponseFactory(string ticketNo,string responseText,
            Guid from,Guid to,string attachments,bool isRead)
        {
            var response = new Response();
            
            
            response.GenerateNewIdentity();

            response.TicketNumber = ticketNo;

            response.ResponseText = responseText;

            response.From = from;

            response.To = to;

            response.IsRead = isRead;

            response.CreatedAt=DateTime.Now;

            response.Attachments = attachments;

            return response;
        }
        
    }
}