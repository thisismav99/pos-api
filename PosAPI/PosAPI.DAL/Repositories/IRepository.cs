﻿namespace PosAPI.DAL.Repositories
{
    public interface IRepository<T, TContext> 
        where T : class
        where TContext : class
    {
        Task Add(T entity);
        void Update(T entity);
        Task Delete(Guid id);
        Task<T?> Get(Guid id);
        Task<List<T>?> GetAll();
    }
}