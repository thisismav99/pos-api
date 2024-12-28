using Microsoft.EntityFrameworkCore;

namespace PosAPI.DAL.UnitOfWorks
{
    public interface IUnitOfWork<TContext>
        where TContext : DbContext
    {
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task SaveChanges();
    }
}
