namespace PosAPI.DAL.Repositories
{
    public interface IPosRepository<T> where T : class
    {
        Task Add(T entity);
        void Update(T entity);
        Task Delete(Guid id);
        Task<T?> Get(Guid id);
        Task<List<T>?> GetAll();
    }
}
