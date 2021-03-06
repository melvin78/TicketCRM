using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification
{
    public class EnquiryCategoryByIdSpecification:BaseSpecification<EnquiryCategory>
    {
        public Guid Id { get; set; }
        
        public EnquiryCategoryByIdSpecification(Guid id)
        {
            Id = id;

        }

        public override Expression<Func<EnquiryCategory, bool>> SpecExpression
        {
            get
            {
                return o => o.EnquiryId == Id;
            }
        }

      
    }
}