using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification
{
    public class EnquiryCategoryBaseSpecifiaction:BaseSpecification<EnquiryCategory>
    {

        public string EnquiryCategory { get; set; }
        
        public EnquiryCategoryBaseSpecifiaction(string enquiryCategory)
        {
            EnquiryCategory = enquiryCategory;
        }

        public override Expression<Func<EnquiryCategory, bool>> SpecExpression
        {
            get
            {
                return o => o.EnquiryCategoryVal == EnquiryCategory;
            }
        }

    }
}