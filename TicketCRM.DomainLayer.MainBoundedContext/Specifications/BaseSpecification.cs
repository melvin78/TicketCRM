using System.Linq.Expressions;
using Centrino.DomainLayer.MainBoundedContext;

namespace TicketCRM.DomainLayer.MainBoundedContext.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression;

        private Func<T, bool> CompiledExpression
        {
            get { return _compiledExpression ?? (_compiledExpression = SpecExpression.Compile()); }
        }

        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }




        public Expression<Func<T, bool>> Criteria { get; set; }
        public Expression<Func<T, object>> SpecificColumn { get; private set; }


        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification()
        {

        }




        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        
        public Expression<Func<T, object>> DistinctBy { get; private set; }


        public Expression<Func<T, object>> OrderByAscending { get; set; }


        protected void ApplyTake(int val)
        {
            Take = val;
        }


        protected void ApplyOrderByDescending(Expression<Func<T, object>> expression)
        {
            OrderByDescending = expression;
        }
        
        

        protected void ApplyGroupBy(Expression<Func<T, object>> expression)
        {
            GroupBy = expression;
        }

        protected void ApplyOrderByAscending(Expression<Func<T, object>> expression)
        {
            OrderByAscending = expression;
        }

        protected void ApplyDistinctBy(Expression<Func<T, object>> expression)
        {
            DistinctBy = expression;
            ApplyGroupBy(expression);
        }
        protected virtual void AddInclude(string Include)
        {
            IncludeStrings.Add(Include);
        }

        protected virtual void AddInclude(Expression<Func<T, object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }

        protected  void SelectSpecificColumn(Expression<Func<T, object>> expression)
        {
            SpecificColumn = expression;
        }
}
}