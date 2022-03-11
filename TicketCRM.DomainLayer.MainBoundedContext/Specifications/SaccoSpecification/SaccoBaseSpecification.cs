using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.SaccoSpecification
{
    public class SaccoBaseSpecification:BaseSpecification<Sacco>
    {
        public Guid? SaccoId { get; set; }

        public SaccoBaseSpecification(Guid? saccoId)
        {
            SaccoId = saccoId;

        }
        public override Expression<Func<Sacco, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == SaccoId;
            }
        }
    }
}