using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.SaccoSpecification
{
    public class SaccoBaseSpecification:BaseSpecification<Organization>
    {
        public Guid? SaccoId { get; set; }

        public SaccoBaseSpecification(Guid? saccoId)
        {
            SaccoId = saccoId;

        }
        public override Expression<Func<Organization, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == SaccoId;
            }
        }
    }
}