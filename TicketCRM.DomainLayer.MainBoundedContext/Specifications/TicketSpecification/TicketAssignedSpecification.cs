using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketAssignedSpecification:BaseSpecification<Ticket>
    {
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                //open tickets
                return o => o.TicketStatusId == Guid.Parse("2f049c19-471c-ec11-b063-14cb19ba19a9");
            }
        }
    }
}