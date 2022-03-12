using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketByTicketInformationSpecification:BaseSpecification<TicketInformation>
    {
        public Guid CustomerId { get; set; }

        public TicketByTicketInformationSpecification(Guid customerId)
        {
            CustomerId = customerId;

        }

        public override Expression<Func<TicketInformation, bool>> SpecExpression
        {
            get
            {
                return o => o.CustomerId == CustomerId;
            }
        }
    }
}