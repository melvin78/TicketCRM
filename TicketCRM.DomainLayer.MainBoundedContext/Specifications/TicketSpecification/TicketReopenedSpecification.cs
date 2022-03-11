using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketReopenedSpecification:BaseSpecification<Ticket>
    {
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketStatusId == Guid.Parse("3e763c25-5677-4ea4-a754-9b3d9b09c463");
            }
        }
    }
}