using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;
namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketTransferredSpecification:BaseSpecification<Ticket>
    {
        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                //transferred
                return o => o.TicketStatusId == Guid.Parse("1ad2bda2-c923-ec11-8172-84a93e1f9479");
            }
        }
    }
}