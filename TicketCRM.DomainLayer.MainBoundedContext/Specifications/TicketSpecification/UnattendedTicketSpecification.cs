using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class UnattendedTicketSpecification:BaseSpecification<Ticket>
    {
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.CareTaker == Guid.Empty;
            }
        }

    }
}