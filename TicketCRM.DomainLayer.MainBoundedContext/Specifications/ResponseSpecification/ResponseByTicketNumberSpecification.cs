using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseByTicketNumberSpecification:BaseSpecification<Response>
    {
        public string TicketNumber { get; set; }
        
        public ResponseByTicketNumberSpecification(string ticketNumber)
        {
            TicketNumber = ticketNumber;

        }

        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNumber == TicketNumber;
            }
        }
    }
}