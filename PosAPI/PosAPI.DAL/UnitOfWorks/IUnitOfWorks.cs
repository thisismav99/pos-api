namespace PosAPI.DAL.UnitOfWorks
{
    public interface IUnitOfWorks<T> where T : class
    {
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task SaveChanges();
    }
}
