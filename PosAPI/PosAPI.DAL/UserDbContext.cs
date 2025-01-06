using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PosAPI.DAL
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        #region Constructor
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        #endregion
    }
}
