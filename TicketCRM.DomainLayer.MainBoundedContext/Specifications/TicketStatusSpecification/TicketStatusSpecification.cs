using System;
using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;
using TicketCRM.DomainLayer.MainBoundedContext.Specifications;

namespace Centrino.DomainLayer.MainBoundedContext.Specifications.TicketStatusSpecification
{
    public class TicketStatusSpecification:BaseSpecification<TicketStatus>
    {
        public Guid TicketStatusId { get; set; }

        public TicketStatusSpecification(Guid ticketStatusId)
        {
            TicketStatusId = ticketStatusId;

        }
        public override Expression<Func<TicketStatus, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == TicketStatusId;
            }
        }

      
    }
}