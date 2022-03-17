using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.RecentActivitySpecification
{
    public class RecentActivitySaccoSpecification:BaseSpecification<RecentActivity>
    {
        public Guid OrgnazationId { get; }

        public RecentActivitySaccoSpecification(Guid organizationId)
        {
            OrgnazationId = organizationId;
        }
        public override Expression<Func<RecentActivity, bool>> SpecExpression
        {
            get
            {
                return o => o.OrganizationId == OrgnazationId;
            }
        }
    }
}