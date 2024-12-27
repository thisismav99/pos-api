using Microsoft.EntityFrameworkCore.Storage;

namespace PosAPI.DAL.UnitOfWorks
{
    public class UnitOfWorks<T, TContext> : IUnitOfWorks<T> 
        where T : class
        where TContext : BaseDbContext<T>
    {
        #region Variables
        private readonly TContext _context;
        private IDbContextTransaction? _transaction;
        #endregion

        #region Constructor
        public UnitOfWorks(TContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task BeginTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            if(_transaction is not null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransaction()
        {
            if( _transaction is not null)
                await _transaction.RollbackAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
