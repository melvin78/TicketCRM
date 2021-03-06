using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.UserSpecification
{
    public class UserAgentWithTokenSpecification:BaseSpecification<Agent>
    {
        

        public override Expression<Func<Agent, bool>> SpecExpression
        {
            get
            {
                return o => o.HasToken == true;
            }
        }

     
    }
}