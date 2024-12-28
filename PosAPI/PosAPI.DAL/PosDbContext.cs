using Microsoft.EntityFrameworkCore;
using PosAPI.DAL.ModelMappings.Cards;
using PosAPI.DAL.ModelMappings.Products;
using PosAPI.DAL.ModelMappings.Transactions;

namespace PosAPI.DAL
{
    public class PosDbContext : BaseDbContext<PosDbContext>
    {
        #region Constructor
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
        {
            
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CardModelMapping());
            modelBuilder.ApplyConfiguration(new ProductModelMapping());
            modelBuilder.ApplyConfiguration(new ProductTransactionModelMapping());
            modelBuilder.ApplyConfiguration(new TransactionModelMapping());
        }
        #endregion
    }
}
