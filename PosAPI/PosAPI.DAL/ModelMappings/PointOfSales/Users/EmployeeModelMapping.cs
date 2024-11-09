using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.PointOfSales.Users;

namespace PosAPI.DAL.ModelMappings.PointOfSales.Users
{
    public class EmployeeModelMapping : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.AddressID).IsRequired();
            builder.Property(x => x.PositionID).IsRequired();
            builder.Property(x => x.CompanyID).IsRequired();
            builder.Property(x => x.CompanyOrgID).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedBy);
            builder.Property(x => x.UpdatedOn);
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(a => a.Address).WithMany().HasForeignKey(f => f.AddressID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Position).WithMany().HasForeignKey(f => f.PositionID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Company).WithMany().HasForeignKey(f => f.CompanyID).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(co => co.CompanyOrg).WithMany().HasForeignKey(f => f.CompanyOrgID).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Employee");
        }
    }
}
