using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketReportSpecification
{
    public class TicketReportBaseSpecification:BaseSpecification<TicketReport>
    {
        
        public string TicketNo { get; set; }
        
        public TicketReportBaseSpecification(string ticketNo)
        {
            TicketNo = ticketNo;

        }

        public override Expression<Func<TicketReport, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNo == TicketNo;
            }
        }

       
    }
}