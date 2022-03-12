using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.InboxSpecification
{
    public class InboxToSpecification:BaseSpecification<Inbox>
    {
        public Guid UserTo { get; set; }
        
        public InboxToSpecification(Guid userTo)
        {
            UserTo = userTo;

        }

        public override Expression<Func<Inbox, bool>> SpecExpression
        {
            get
            {
                return o => o.RoomUsers.IdTo == UserTo;
            }
        }
    }
}