using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketCRM.DomainLayer.MainBoundedContext;
using TicketCRM.EfCoreTests.Contracts;

namespace TicketCRM.EfCoreTests.Repositories
{
    public class GenericReadRepository : IGenericReadRepository
    {
        private readonly TicketCRMDBContext _ticketCrmdbContext;

        public GenericReadRepository(TicketCRMDBContext dbContext)
        {
            _ticketCrmdbContext = dbContext;
        }

        public async Task<bool> IfExistsAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await ApplySpecification(spec).AnyAsync();
        }
        public async Task<int> CountAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await ApplySpecification(spec).CountAsync<T>();
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return SpecificationEvalutator<T>.GetQueryable(_ticketCrmdbContext.Set<T>().AsQueryable(), spec);
        }

        public IQueryable<T> GetAll<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return ApplySpecification(spec).AsNoTracking();
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await _ticketCrmdbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync<T>() where T : BaseEntity
        {
            return await _ticketCrmdbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await GetAll(spec).ToListAsync();
        }

        public async Task<T> GetAsync<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
    }
}
