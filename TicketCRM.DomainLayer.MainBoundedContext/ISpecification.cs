using System.Linq.Expressions;

namespace TicketCRM.DomainLayer.MainBoundedContext;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> SpecExpression { get; }
        
 
    Expression<Func<T, bool>> Criteria { get; }
        

    Expression<Func<T, object>> SpecificColumn { get; }

        
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
        
        
     
        
        
      
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
        

    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> GroupBy { get; }
        
    Expression<Func<T,object>> DistinctBy { get; }

    Expression<Func<T,object>> OrderByAscending { get; }
        
        
}