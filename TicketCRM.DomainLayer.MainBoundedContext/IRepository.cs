namespace TicketCRM.DomainLayer.MainBoundedContext
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<int> AddAsync(TEntity entity);

        int Add(TEntity entity);
        
        Task<int> AllMatchingCountAsync(ISpecification<TEntity> specification);
        
        int AllMatchingCount(ISpecification<TEntity> specification);

        
        bool Remove(TEntity entity, Guid entityId);
        
        bool SubscriptionStatus(TEntity entity);
        
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification = null);
        
       // TEntity Find(ISpecification<TEntity> specification = null);

        
        void Merge(TEntity original, TEntity current);


        Task<List<TEntity>> GetKeylessEntitiy(string rawSql);

        TEntity? GetKeylessEntityByParams(string rawSql,string parameters);
        
        
        List<TEntity> GetAllKeylessEntityByParams(string rawSql,string parameters);
        
        List<TEntity> GetAllKeylessEntityByMultipleParamsByTicketStatusIdAndOrganizationId(string ticketStatusId, string organization);



        Task<bool> RemoveAsync(Guid entityId);

        Task<List<TEntity>> GetAllAsync();

        List<TEntity> GetAll();
        Task<TEntity> GetAsync(Guid entityId);


        Task<TEntity> GetByIdAsync(Guid entityId);

   







    }
}