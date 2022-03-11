using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketSaccoSpecification:BaseSpecification<Ticket>
    {
        public Guid SaccoId { get; }

        public TicketSaccoSpecification(Guid saccoId)
        {
            SaccoId = saccoId;
        }
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.SaccoId == SaccoId;
            }
        }
    }
}