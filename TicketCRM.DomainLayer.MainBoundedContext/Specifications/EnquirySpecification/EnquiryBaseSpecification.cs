using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquirySpecification
{
    public class EnquiryBaseSpecification:BaseSpecification<Enquiries>
    {
        public string EnquiryVal { get; set; }
        
        public EnquiryBaseSpecification(string enquiryVal)
        {
            EnquiryVal = enquiryVal;

        }

        public override Expression<Func<Enquiries, bool>> SpecExpression
        {
            get
            {
                return o => o.EnquiryType == EnquiryVal;
            }
        }

      
    }
}