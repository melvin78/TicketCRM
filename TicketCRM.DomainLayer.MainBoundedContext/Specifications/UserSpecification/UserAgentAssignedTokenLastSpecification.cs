using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class UserAgentAssignedTokenLastSpecification:BaseSpecification<Agent>
    {
        public override Expression<Func<Agent, bool>> SpecExpression
        {
            get
            {
                return o => o.Id != null;
            }
        }
    }
}