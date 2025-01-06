using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosAPI.DAL.Models.Cards;

namespace PosAPI.DAL.ModelMappings.Cards
{
    public class CardModelMapping : IEntityTypeConfiguration<CardModel>
    {
        public void Configure(EntityTypeBuilder<CardModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CardBankName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CardType).IsRequired().HasMaxLength(10);
            builder.Property(x => x.CardAccountName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CardAccountNumber).IsRequired().HasMaxLength(15);
            builder.Property(x => x.CardExpiry).IsRequired();
            builder.Property(x => x.CardCvcNumber).IsRequired().HasMaxLength(3);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            builder.ToTable("Card");
        }
    }
}
