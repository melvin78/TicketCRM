using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketUserSpecification:BaseSpecification<Ticket>
    {
        public string TicketNo { get; set; }
        
        public TicketUserSpecification(string ticketno)
        {
            TicketNo = ticketno;
        }
        
   

        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNo == TicketNo;
            }
        }

      
    }
    
}