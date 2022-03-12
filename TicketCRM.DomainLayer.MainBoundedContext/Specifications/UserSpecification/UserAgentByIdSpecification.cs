using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class UserAgentByIdSpecification:BaseSpecification<Agent>
    {
        public Guid AgentId { get; set; }
        
        public UserAgentByIdSpecification(Guid agentId)
        {
            AgentId = agentId;

        }
        public override Expression<Func<Agent, bool>> SpecExpression
        {
            get
            {
                return o => o.UserId == AgentId;
            }
        }
    }
}