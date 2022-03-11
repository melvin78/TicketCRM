using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseSpecificationByReceiver:BaseSpecification<Response>
    {
        public Guid To { get; set; }
        
        public ResponseSpecificationByReceiver(Guid toId)
        {
            To = toId;

        }

        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.To == To;
            }
        }
    }
}