﻿using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.RecentActivitySpecification
{
    public class RecentActivityBaseSpecification : BaseSpecification<RecentActivity>
    {
        public RecentActivityBaseSpecification()
        {
            ApplyOrderByDescending(o => o.CreatedAt);
            
            ApplyTake(10);
        }


        public override Expression<Func<RecentActivity, bool>> SpecExpression
        {
            get { return o => o.Id != Guid.Empty; }
        }
    }
}