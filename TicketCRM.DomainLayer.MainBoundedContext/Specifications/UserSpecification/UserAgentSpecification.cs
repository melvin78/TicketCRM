using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class UserAgentSpecification:BaseSpecification<Agent>
    {
        public UserAgentSpecification()
        {
            ApplyOrderByAscending(o=>o.TokenAssignmentDate);
        }
        public override Expression<Func<Agent, bool>> SpecExpression
        {
            get
            {
                return o => o.Id != null;
            }
        }
    }
}