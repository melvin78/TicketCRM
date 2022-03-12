using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification
{
    public sealed class EnquiryCategoryByEnquirySpecification:BaseSpecification<EnquiryCategory>
    {
        public EnquiryCategoryByEnquirySpecification():base()
        {
            AddInclude(o=>o.Enquiries);
        }

        public EnquiryCategoryByEnquirySpecification(string enquiryCategoryVal)
            :base(o=>o.EnquiryCategoryVal==enquiryCategoryVal)
        {
            AddInclude(o=>o.Enquiries);
        }
        public override Expression<Func<EnquiryCategory, bool>> SpecExpression { get; }
    
    }
}