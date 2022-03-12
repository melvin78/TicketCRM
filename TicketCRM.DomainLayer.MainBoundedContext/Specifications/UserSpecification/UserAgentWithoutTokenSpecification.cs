using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class UserAgentWithoutTokenSpecification:BaseSpecification<Agent>
    {
        public UserAgentWithoutTokenSpecification()
        {
            ApplyOrderByAscending(o=>o.TokenAssignmentDate);
        }
        public override Expression<Func<Agent, bool>> SpecExpression
        {
            get
            {
                return o => o.HasToken == false;
            }
        }
    }
}