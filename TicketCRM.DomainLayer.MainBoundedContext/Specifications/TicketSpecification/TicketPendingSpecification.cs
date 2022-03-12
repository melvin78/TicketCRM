using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketPendingSpecification:BaseSpecification<Ticket>
    {
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                //overdue
                return o => o.TicketStatusId == Guid.Parse("e70fc103-471c-ec11-b063-14cb19ba19a9");
            }
        }
    }
}