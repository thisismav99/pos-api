using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.PointOfSales.Products;

namespace PosAPI.DAL.ModelMappings.PointOfSales.Products
{
    public class ProductModelMapping : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ProductPrice).IsRequired();
            builder.Property(x => x.ProductDescription).HasMaxLength(1000);
            builder.Property(x => x.CompanyID).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(c => c.Company).WithMany().HasForeignKey(f => f.CompanyID).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Product");
        }
    }
}
