using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public sealed class TicketMessageSpecification:BaseSpecification<Ticket>
    {
        public string TicketNo { get; set; }
        
        public TicketMessageSpecification(string ticketNo)
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