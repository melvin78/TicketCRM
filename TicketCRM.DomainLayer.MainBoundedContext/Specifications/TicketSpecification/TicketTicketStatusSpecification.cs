using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public sealed class TicketTicketStatusSpecification:BaseSpecification<Ticket>
    {
        public TicketTicketStatusSpecification(Expression<Func<Ticket, bool>> specExpression)
        {
            SpecExpression = specExpression;
            AddInclude(o=>o.TicketStatus);
        }

        public TicketTicketStatusSpecification(Guid ticketStatus, Expression<Func<Ticket, bool>> specExpression)
            :base(o=>o.TicketStatusId==ticketStatus)
        {
            SpecExpression = specExpression;
            AddInclude(o=>o.TicketStatus);
        }
        public override Expression<Func<Ticket, bool>> SpecExpression { get; }
      
    }
}