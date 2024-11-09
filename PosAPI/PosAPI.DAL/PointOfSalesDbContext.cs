using Microsoft.EntityFrameworkCore;
using PosAPI.DAL.ModelMappings.PointOfSales.Companies;
using PosAPI.DAL.ModelMappings.PointOfSales.Customers;
using PosAPI.DAL.ModelMappings.PointOfSales.Products;
using PosAPI.DAL.ModelMappings.PointOfSales.Transactions;
using PosAPI.DAL.ModelMappings.PointOfSales.Users;

namespace PosAPI.DAL
{
    public class PointOfSalesDbContext : DbContext
    {
        #region Constructor
        public PointOfSalesDbContext(DbContextOptions<PointOfSalesDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeModelMapping());
            modelBuilder.ApplyConfiguration(new AddressModelMapping());
            modelBuilder.ApplyConfiguration(new PositionModelMapping());
            modelBuilder.ApplyConfiguration(new CompanyModelMapping());
            modelBuilder.ApplyConfiguration(new CompanyAddressModelMapping());
            modelBuilder.ApplyConfiguration(new CompanyOrgModelMapping());
            modelBuilder.ApplyConfiguration(new CustomerModelMapping());
            modelBuilder.ApplyConfiguration(new ProductModelMapping());
            modelBuilder.ApplyConfiguration(new TransactionModelMapping());
        }
        #endregion
    }
}
