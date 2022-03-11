using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification
{
    public class InboxBaseSpecification:BaseSpecification<Inbox>
    {
        public Guid Id { get; set; }
        
        public InboxBaseSpecification(Guid id)
        {
            Id = id;
        }

        public override Expression<Func<Inbox, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == Id;
            }
        }
    }
}