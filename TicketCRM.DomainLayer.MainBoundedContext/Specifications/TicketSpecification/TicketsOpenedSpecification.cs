using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketsOpenedSpecification:BaseSpecification<Ticket>
    {
        
      
        
        
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketStatusId == Guid.Parse("0e06e836-471c-ec11-b063-14cb19ba19a9");
            }
        }
    }
}