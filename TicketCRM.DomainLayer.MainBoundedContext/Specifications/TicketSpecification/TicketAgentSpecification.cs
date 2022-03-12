using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketAgentSpecification:BaseSpecification<Ticket>
    {
        public Guid AgentId{ get; set; }
        
        public TicketAgentSpecification(Guid agentId)
        {
            AgentId = agentId;

        }
      
        
        
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.CareTaker == AgentId;
            }
        }
    }
}