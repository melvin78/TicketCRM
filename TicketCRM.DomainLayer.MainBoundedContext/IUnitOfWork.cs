namespace TicketCRM.DomainLayer.MainBoundedContext
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity>? Repository<TEntity>() where TEntity : class;

        Guid GetRelatedEntityIdentity<TEntity>() where TEntity : BaseEntity;
        
        int Complete();
    }
}
