using System.Collections.Generic;
using System.Threading.Tasks;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace TicketCRM.EfCoreTests.Repositories
{
    public interface IGenericWriteRepository
    {
        Task<T> InsertAsync<T>(T entity) where T : BaseEntity;
        
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        
        Task<int> SaveAsync();
        
        Task<int> InsertAsync<T>(IEnumerable<T> entities) where T : BaseEntity;
        
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
    }
}