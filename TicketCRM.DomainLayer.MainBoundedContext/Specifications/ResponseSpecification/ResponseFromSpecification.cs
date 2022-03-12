using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseFromSpecification:BaseSpecification<Response>
    {
        public Guid UserId { get; set; }
            
        public ResponseFromSpecification(Guid userId)
        {
            UserId = userId;

        }
        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.From == UserId;
            }
        }
    }
}