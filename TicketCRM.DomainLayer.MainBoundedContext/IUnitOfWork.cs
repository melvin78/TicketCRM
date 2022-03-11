using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace Centrino.DomainLayer.MainBoundedContext
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Guid GetRelatedEntityIdentity<TEntity>() where TEntity : BaseEntity;
        
        int Complete();
    }
}
