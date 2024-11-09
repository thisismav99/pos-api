using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.PointOfSales.Customers;

namespace PosAPI.DAL.ModelMappings.PointOfSales.Customers
{
    public class CustomerModelMapping : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.ContactNumber).IsRequired().HasMaxLength(25);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.IsActive).IsRequired();

            builder.ToTable("Customer");
        }
    }
}
