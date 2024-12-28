using Microsoft.EntityFrameworkCore;

namespace PosAPI.DAL
{
    public class BaseDbContext<TContext> : DbContext 
        where TContext : DbContext
    {
        #region Constructor
        public BaseDbContext(DbContextOptions<TContext> options) : base(options)
        {
            
        }
        #endregion
    }
}
