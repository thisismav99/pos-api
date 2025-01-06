using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.Transactions;

namespace PosAPI.DAL.ModelMappings.Transactions
{
    public class TransactionModelMapping : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.AmountPaid).IsRequired();
            builder.Property(x => x.PaymentMethod).IsRequired().HasMaxLength(10);
            builder.Property(x => x.CardId);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.HasOne(x => x.CardModel).WithMany().HasForeignKey(f => f.CardId).OnDelete(DeleteBehavior.ClientCascade);

            builder.ToTable("Transaction");
        }
    }
}
