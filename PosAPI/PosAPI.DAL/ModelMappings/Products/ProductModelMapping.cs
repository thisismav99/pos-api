using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.Products;

namespace PosAPI.DAL.ModelMappings.Products
{
    public class ProductModelMapping : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProductAmount).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
