using System.Collections;
using TicketCRM.DataAccess.Configuration;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace TicketCRM.DataAccess
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly TicketCRMDbContext _ticketCrmDbContext;

        private Hashtable _repositories;

        public UnitOfWork(TicketCRMDbContext ticketCrmDbContext, Hashtable repositories)
        {
            _ticketCrmDbContext = ticketCrmDbContext;
            _repositories = repositories;
        }
        public void Dispose()
        {
            _ticketCrmDbContext.Dispose();
        }

        public IRepository<TEntity>? Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.Contains(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(TEntity)), _ticketCrmDbContext);
                _repositories.Add(type,repositoryInstance);

                
                
            }

            return (IRepository<TEntity>) _repositories[type]!;
        }

        public Guid GetRelatedEntityIdentity<TEntity>() where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public int Complete()
        {
            return _ticketCrmDbContext.SaveChanges();

        }
    }
}