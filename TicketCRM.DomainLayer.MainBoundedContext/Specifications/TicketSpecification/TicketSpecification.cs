using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{

    public class TicketSpecification:BaseSpecification<Ticket>
    {
        public string TicketSpec { get; set; }
        
        public TicketSpecification(string ticketSpec)
        {
            TicketSpec = ticketSpec;
        }

        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNo == TicketSpec;
            }
        }

       
    }
    
}