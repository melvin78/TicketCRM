using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketStatusSpecification
{
    public class TicketStatusSpecification:BaseSpecification<TicketStatus>
    {
        public Guid TicketStatusId { get; set; }

        public TicketStatusSpecification(Guid ticketStatusId)
        {
            TicketStatusId = ticketStatusId;

        }
        public override Expression<Func<TicketStatus, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == TicketStatusId;
            }
        }

      
    }
}