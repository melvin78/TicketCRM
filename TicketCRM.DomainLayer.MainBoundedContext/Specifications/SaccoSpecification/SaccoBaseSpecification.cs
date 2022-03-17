using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.SaccoSpecification
{
    public class SaccoBaseSpecification:BaseSpecification<Organization>
    {
        public Guid? OrganizationId { get; set; }

        public SaccoBaseSpecification(Guid? organizationId)
        {
            OrganizationId = organizationId;

        }
        public override Expression<Func<Organization, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == OrganizationId;
            }
        }
    }
}