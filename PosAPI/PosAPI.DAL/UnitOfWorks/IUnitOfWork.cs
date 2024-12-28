namespace PosAPI.DAL.UnitOfWorks
{
    public interface IUnitOfWork<TContext>
        where TContext : class
    {
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task SaveChanges();
    }
}
