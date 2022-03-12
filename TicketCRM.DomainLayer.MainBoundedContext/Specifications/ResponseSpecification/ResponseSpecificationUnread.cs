using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseSpecificationUnread:BaseSpecification<Response>
    {

        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.IsRead == false;
            }
        }
    }
}