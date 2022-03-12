using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketsOpenedByUserSpecification:BaseSpecification<Ticket>
    {
        public Guid UserId { get; set; }
        
        public TicketsOpenedByUserSpecification(Guid userId)
        {
            UserId = userId;

        }

        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.CustomerId == UserId;
            }
        }
    }
}