using System.Linq.Expressions;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications
{
    public class Or<T>:BaseSpecification<T>
    {
         ISpecification<T> left;

          ISpecification<T> right;

          public Or(
              ISpecification<T> left,
              ISpecification<T> right)
          {
              this.left = left;
              this.right = right;
          }


          public override Expression<Func<T, bool>> SpecExpression
          {
              get
              {
                  var objParam = Expression.Parameter(typeof(T), "obj");

                  var newExpr = Expression.Lambda<Func<T, bool>>(
                      Expression.OrElse(
                          Expression.Invoke(left.SpecExpression, objParam),
                          Expression.Invoke(right.SpecExpression, objParam)
                      ),
                      objParam
                  );

                  return newExpr;
              }
          }
    }
}