using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.PointOfSales.Companies;

namespace PosAPI.DAL.ModelMappings.PointOfSales.Companies
{
    public class CompanyOrgModelMapping : IEntityTypeConfiguration<CompanyOrgModel>
    {
        public void Configure(EntityTypeBuilder<CompanyOrgModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.Division).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Department).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.IsActive).IsRequired();

            builder.ToTable("Company_Organization");
        }
    }
}
