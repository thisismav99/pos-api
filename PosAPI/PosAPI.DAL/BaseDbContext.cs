using Microsoft.EntityFrameworkCore;

namespace PosAPI.DAL
{
    public class BaseDbContext<T> : DbContext where T : class
    {
        #region Constructor
        public BaseDbContext(DbContextOptions<BaseDbContext<T>> options) : base(options)
        {
            
        }
        #endregion
    }
}
