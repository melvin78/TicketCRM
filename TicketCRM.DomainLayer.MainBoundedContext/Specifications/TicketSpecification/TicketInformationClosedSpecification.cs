﻿using System.Linq.Expressions;
using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.TicketSpecification
{
    public class TicketInformationClosedSpecification:BaseSpecification<TicketInformation>
    {
        public override Expression<Func<TicketInformation, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketStatusVal == "Closed";
            }
        }
    }
}