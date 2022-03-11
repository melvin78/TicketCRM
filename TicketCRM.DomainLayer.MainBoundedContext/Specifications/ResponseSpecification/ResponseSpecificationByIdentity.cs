using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseSpecificationByIdentity:BaseSpecification<Response>
    {
        public Guid Id { get; set; }

        public ResponseSpecificationByIdentity(Guid id)
        {
            Id = id;
        }
        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == Id;
            }
        }
    }
}