using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketClosedSpecification:BaseSpecification<Ticket>
    {
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketStatusId == Guid.Parse("35ba51b2-9987-4d5e-b3a2-460584a23deb");
            }
        }
    }
}