using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace TicketCRM.DomainLayer.MainBoundedContext
{
    //class to change users specifications to IQueryable type.When its IQueryable it means it wont
    //have to be loaded into memory. The class has one static method meaning it doesnt have to be instantiated in order
    //to access it methods or properties. The static method has a return type of IQuearyable which has a genric type of
    //Tentity(the domain class or type entity or the businees model)
    public class SpecificationEvalutator<TEntity> where TEntity : class
    {
        //Method accepts an Iqueryable type of type Tentity(domain/object) and an instance of class that implements 
        //Ispecification 
        public static IQueryable<TEntity> GetQueryable(IQueryable<TEntity> queryable,
            ISpecification<TEntity> specification)
        {
         //variable that is assigned type queryable
            var query = queryable;
           
            if (specification.Criteria != null ) query = query.Where(specification.Criteria);
            
       
          
            if (specification.SpecExpression!= null) query = query.Where(specification.SpecExpression);
            {
                
            }
            {
                
            }

            query = specification.Includes.Aggregate(query,
                (curr, incl) => curr.Include(incl));
            query = specification.IncludeStrings.Aggregate(query, (curr, include) => curr.Include(include));

            if (specification.OrderByAscending != null)
                query = query.OrderBy(specification.OrderByAscending);
            else if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);
           

            if (specification.GroupBy != null) query = query.GroupBy(specification.GroupBy).SelectMany(x => x);

            if (specification.IsPagingEnabled)
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);
            if (specification.Take!=0)
            {
                query = query.Take(specification.Take);
            }

        

       
            return query;
        }
        
      
    }
}