using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext.Specifications;
using Centrino.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications.ResponseSpecification
{
    public class ResponseDistinctSpecification:BaseSpecification<Response>
    {
        public Guid UserId { get; set; }
        
        public ResponseDistinctSpecification(Guid userId)
        {
            UserId = userId;

        }
        public ResponseDistinctSpecification()
        {
            
          ApplyGroupBy(o=>new{o.From,o.To});
          
          ApplyOrderByDescending(o=>o.CreatedAt);
          
        }
        public override Expression<Func<Response, bool>> SpecExpression { get; }
    }
}