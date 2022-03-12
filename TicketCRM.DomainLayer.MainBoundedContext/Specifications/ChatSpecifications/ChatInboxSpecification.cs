using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ChatSpecifications;

public class ChatInboxSpecification:BaseSpecification<Chats>
{
    public string TicketNumber { get; set; }
    

    public ChatInboxSpecification(string ticketNumber)
    {
        TicketNumber = ticketNumber;
            
        ApplyOrderByAscending(o=>o.CreatedAt);
    }

    public override Expression<Func<Chats, bool>> SpecExpression
    {
        get
        {
            return o => o.InboxId == TicketNumber;
        }
    }
}