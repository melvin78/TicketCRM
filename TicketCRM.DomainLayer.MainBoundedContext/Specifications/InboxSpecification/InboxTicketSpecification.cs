using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification
{
    public class InboxTicketSpecification:BaseSpecification<Inbox>
    {
        public string TicketNumber { get; set; }

        public InboxTicketSpecification(string ticketNumber)
        {
            TicketNumber = ticketNumber;

        }
        public override Expression<Func<Inbox, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNumber == TicketNumber;
            }
        }
    }
}