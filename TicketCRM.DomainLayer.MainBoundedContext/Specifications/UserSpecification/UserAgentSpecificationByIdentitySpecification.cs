using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class UserAgentSpecificationByIdentitySpecification:BaseSpecification<Agent>
    {
        public Guid UserId { get; set; }

        public UserAgentSpecificationByIdentitySpecification(Guid userId)
        {
            UserId = userId;

        }
        public override Expression<Func<Agent, bool>> SpecExpression
        {
            get
            {
                return o => o.UserId == UserId;
            }
        }
    }
}