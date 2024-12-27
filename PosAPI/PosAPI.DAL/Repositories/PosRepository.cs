using Microsoft.EntityFrameworkCore;

namespace PosAPI.DAL.Repositories
{
    public class PosRepository<T, TContext> : IPosRepository<T> 
        where T : class
        where TContext : BaseDbContext<T>
    {
        #region Variables
        private readonly TContext _context;
        #endregion

        #region Constructor
        public PosRepository(TContext context)
        {

            _context = context;
        }
        #endregion

        #region Methods
        public async Task Add(T entity)
        {
            await _context.Set<T>()
                          .AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            await _context.Set<T>()
                          .Where(x => x == entity)
                          .ExecuteDeleteAsync();
        }

        public async Task<T?> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>?> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            await _context.Set<T>()
                          .Where(x => x == entity)
                          .ExecuteUpdateAsync(s => 
                            s.SetProperty(e => e, entity));
        }
        #endregion
    }
}
