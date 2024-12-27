﻿using Microsoft.EntityFrameworkCore;

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
