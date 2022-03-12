using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class TicketStatusFactory
    {

        public static TicketStatus AddNewTicketStatus(string ticketStatusval)
        {

            var ticketStatus = new TicketStatus();
            
            ticketStatus.GenerateNewIdentity();

            ticketStatus.TicketStatusVal = ticketStatusval;
            
            ticketStatus.CreatedAt= DateTime.Now;

            return ticketStatus;
        }
        
    }
}