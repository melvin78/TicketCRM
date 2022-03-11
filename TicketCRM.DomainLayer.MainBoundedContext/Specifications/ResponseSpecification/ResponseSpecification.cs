using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseSpecification:BaseSpecification<Response>
    {
        public string TicketNo { get; set; }


   
        public ResponseSpecification(string ticketNo)
        {
            TicketNo = ticketNo;
            
            ApplyOrderByAscending(o=>o.CreatedAt);
            
        }

        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNumber == TicketNo;
            }
        }
    }
}