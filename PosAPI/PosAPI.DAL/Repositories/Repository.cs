using Microsoft.EntityFrameworkCore;

namespace PosAPI.DAL.Repositories
{
    public class Repository<T, TContext> : IRepository<T, TContext> 
        where T : class
        where TContext : DbContext
    {
        #region Variables
        private readonly BaseDbContext<TContext> _context;
        #endregion

        #region Constructor
        public Repository(BaseDbContext<TContext> context)
        {

            _context = context;
        }
        #endregion

        #region Methods
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await Get(id);

            if (entity is not null) 
                _context.Set<T>().Remove(entity);
        }

        public async Task<T?> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>?> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
        }
        #endregion
    }
}
