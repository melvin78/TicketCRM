using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.RecentActivitySpecification
{
    public class RecentActivitySaccoSpecification:BaseSpecification<RecentActivity>
    {
        public Guid SaccoId { get; }

        public RecentActivitySaccoSpecification(Guid saccoId)
        {
            SaccoId = saccoId;
        }
        public override Expression<Func<RecentActivity, bool>> SpecExpression
        {
            get
            {
                return o => o.SaccoId == SaccoId;
            }
        }
    }
}