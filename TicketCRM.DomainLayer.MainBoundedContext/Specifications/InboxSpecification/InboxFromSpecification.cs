using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification
{
    public class InboxFromSpecification:BaseSpecification<Inbox>
    {
        public Guid UserFrom { get; set; }
        
        public InboxFromSpecification(Guid userFrom)
        {
            UserFrom = userFrom;

        }

        public override Expression<Func<Inbox, bool>> SpecExpression
        {
            get
            {
                return o => o.RoomUsers.IdFrom == UserFrom;
            }
        }
    }
}