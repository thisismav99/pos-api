using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.PointOfSales.Companies;

namespace PosAPI.DAL.ModelMappings.PointOfSales.Companies
{
    public class CompanyModelMapping : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CompanyEmail).IsRequired();
            builder.Property(x => x.CompanyContactNumber).IsRequired();
            builder.Property(x => x.CompanyAddressID).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(ca => ca.CompanyAddress).WithMany().HasForeignKey(f => f.CompanyAddressID).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Company");
        }
    }
}
