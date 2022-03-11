using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

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