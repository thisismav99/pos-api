using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.Products;

namespace PosAPI.DAL.ModelMappings.Products
{
    public class ProductTransactionModelMapping : IEntityTypeConfiguration<ProductTransactionModel>
    {
        public void Configure(EntityTypeBuilder<ProductTransactionModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TransactionId);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.IsSaved).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(x => x.TransactionModel).WithMany().HasForeignKey(f => f.TransactionId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.ProductModel).WithMany().HasForeignKey(f => f.ProductId).OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("ProductTransaction");
        }
    }
}
