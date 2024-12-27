namespace PosAPI.DAL.Repositories
{
    public interface IPosRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T?> Get(Guid id);
        Task<List<T>?> GetAll();
    }
}
