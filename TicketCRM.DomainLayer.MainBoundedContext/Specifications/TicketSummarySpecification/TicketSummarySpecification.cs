using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSummarySpecification
{
    public class TicketSummarySpecification:BaseSpecification<TicketSummary>
    {
        public override Expression<Func<TicketSummary, bool>> SpecExpression
        {
            get
            {
                return o => o.PastDueDate != null && o.ResolvedOn == null;
            }
        }
    }
}