using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.PointOfSales.Transactions;

namespace PosAPI.DAL.ModelMappings.PointOfSales.Transactions
{
    public class TransactionModelMapping : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.EmployeeID);
            builder.Property(x => x.ProductID);
            builder.Property(x => x.CustomerID);
            builder.Property(x => x.IsVoid).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(e => e.Employee).WithMany().HasForeignKey(f => f.EmployeeID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Product).WithMany().HasForeignKey(f => f.ProductID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Customer).WithMany().HasForeignKey(f => f.CustomerID).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Transaction");
        }
    }
}
