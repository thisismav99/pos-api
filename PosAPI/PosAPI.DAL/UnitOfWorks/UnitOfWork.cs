using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PosAPI.DAL.UnitOfWorks
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> 
        where TContext : DbContext
    {
        #region Variables
        private readonly BaseDbContext<TContext> _context;
        private IDbContextTransaction? _transaction;
        #endregion

        #region Constructor
        public UnitOfWork(BaseDbContext<TContext> context)
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
