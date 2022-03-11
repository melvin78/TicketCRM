﻿using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseTicketSpecification:BaseSpecification<Response>
    {
        public override Expression<Func<Response, bool>> SpecExpression
        {
            get
            {
                return o => o.TicketNumber != null;
            }
        }
    }
}