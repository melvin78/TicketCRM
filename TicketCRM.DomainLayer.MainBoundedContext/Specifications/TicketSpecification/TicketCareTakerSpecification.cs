using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketCareTakerSpecification:BaseSpecification<Ticket>
    {
        public string TicketNo { get; set; }
        
        public TicketCareTakerSpecification(string ticketNo)
        {
            TicketNo = ticketNo;
        }

        public override Expression<Func<Ticket, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNo == TicketNo;
            }
        }
    }
}