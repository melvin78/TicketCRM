﻿using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.EnquiryCategorySpecification
{
    public class EnquiryCategorySpecificationId:BaseSpecification<EnquiryCategory>
    {
        public Guid EnquiryCategoryId { get; set; }

        public EnquiryCategorySpecificationId(Guid enquiryCategoryId)
        {
            EnquiryCategoryId = enquiryCategoryId;

        }
        public override Expression<Func<EnquiryCategory, bool>> SpecExpression
        {
            get
            {
                return o => o.Id == EnquiryCategoryId;
            }
        }
    }
}