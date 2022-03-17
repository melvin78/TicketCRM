using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketSaccoSpecification:BaseSpecification<Ticket>
    {
        public Guid OrganizationId { get; }

        public TicketSaccoSpecification(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.OrganizationId == OrganizationId;
            }
        }
    }
}