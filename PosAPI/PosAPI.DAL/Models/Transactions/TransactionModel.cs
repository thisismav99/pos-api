using PosAPI.DAL.Models.Cards;

namespace PosAPI.DAL.Models.Transactions
{
    public class TransactionModel : BaseModel
    {
        public required string CustomerName { get; set; }

        public int AmountPaid { get; set; }

        public required string PaymentMethod { get; set; }

        public Guid CardId { get; set; }

        public virtual CardModel? CardModel { get; set; }
    }
}
