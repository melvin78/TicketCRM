using Microsoft.EntityFrameworkCore;
using TicketCRM.DataAccess.Configuration;
using TicketCRM.DomainLayer.MainBoundedContext;

namespace TicketCRM.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly TicketCRMDbContext _ticketCrmDbContext;

        public Repository(TicketCRMDbContext ticketCrmDbContext)
        {
            _ticketCrmDbContext = ticketCrmDbContext;
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            _ticketCrmDbContext.Set<TEntity>().Add(entity);

            return await _ticketCrmDbContext.SaveChangesAsync();
        }

        public int Add(TEntity entity)
        {
            _ticketCrmDbContext.Set<TEntity>().Add(entity);

            return _ticketCrmDbContext.SaveChanges();
        }

        public async Task<int> AllMatchingCountAsync(ISpecification<TEntity> specification)
        {
            var result = default(int);

            var objectSet = _ticketCrmDbContext.Set<TEntity>();

            var items = objectSet.Where(specification.SpecExpression);

            if (items != null) result = await items.CountAsync();

            return result;
        }

        public int AllMatchingCount(ISpecification<TEntity> specification)
        {
            var result = default(int);

            var objectSet = _ticketCrmDbContext.Set<TEntity>();

            var items = objectSet.Where(specification.SpecExpression);

            if (items != null) result = items.Count();

            return result;
        }

        public bool Remove(TEntity entity, Guid entityId)
        {
            if (entity == null || entityId == Guid.Empty) return false;

            var removeEntity = _ticketCrmDbContext.Set<TEntity>().Find(entityId);

            if (removeEntity != null)
            {
                _ticketCrmDbContext.Set<TEntity>().Remove(removeEntity);
                _ticketCrmDbContext.SaveChangesAsync();
            }

            return false;
        }


        public bool SubscriptionStatus(TEntity entity)
        {
            _ticketCrmDbContext.Entry(entity).State = EntityState.Modified;

            _ticketCrmDbContext.SaveChangesAsync();

            return true;
        }


        public IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification = null)
        {
            return ApplySpecification(specification);
        }

        public void Merge(TEntity original, TEntity current)
        {
            current.ChangeCurrentIdentity(original.Id);

            current.CreatedBy = original.CreatedBy;

            current.CreatedAt = original.CreatedAt;


            current.ModifiedDate = DateTime.Now;


            _ticketCrmDbContext.Entry(original).CurrentValues.SetValues(current);

            _ticketCrmDbContext.SaveChanges();
        }

        public async Task<List<TEntity>> GetKeylessEntitiy(string rawSql)
        {
            return await _ticketCrmDbContext.Set<TEntity>().FromSqlRaw(rawSql).ToListAsync();
        }

        public TEntity? GetKeylessEntityByParams(string rawSql, string parameters)

        {
            return _ticketCrmDbContext.Set<TEntity>().FromSqlRaw(rawSql, parameters)
                .FirstOrDefault();
        }

        public List<TEntity> GetAllKeylessEntityByParams(string rawSql, string parameters)
        {
            return _ticketCrmDbContext.Set<TEntity>().FromSqlRaw(rawSql, parameters)
                .ToList();
        }

        public List<TEntity> GetAllKeylessEntityByMultipleParamsByTicketStatusIdAndSaccoId(string ticketStatusId, string saccoId)
        {
            return _ticketCrmDbContext
                .Set<TEntity>().
                FromSqlInterpolated($"SELECT * FROM `centrino.email`.ticket_summaries where TicketStatusId ={ticketStatusId} and SaccoId = {saccoId}").ToList();;
        }




        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var entity = await _ticketCrmDbContext.Set<TEntity>().FindAsync(entityId);

            if (entity == null) return false;

            _ticketCrmDbContext.Set<TEntity>().Remove(entity);

            await _ticketCrmDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _ticketCrmDbContext.Set<TEntity>().ToListAsync();
        }

        public List<TEntity> GetAll()
        {
            return _ticketCrmDbContext.Set<TEntity>().AsEnumerable().ToList();
        }

        public async Task<TEntity> GetAsync(Guid entityId)
        {
            var entity = await _ticketCrmDbContext.Set<TEntity>().FindAsync(entityId);

            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid entityId)
        {
            return await _ticketCrmDbContext.Set<TEntity>().FindAsync(entityId);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvalutator<TEntity>.GetQueryable(_ticketCrmDbContext.Set<TEntity>().AsQueryable(),
                specification);
        }
    }
}