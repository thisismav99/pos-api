using PosAPI.DAL.Models.CompanyOne;

namespace PosAPI.DAL.Models.Payment
{
    public class TransactionModel : BaseModel
    {
        public required string CustomerName { get; set; }

        public int AmountPaid { get; set; }

        public required string PaymentMethod { get; set; }

        public Guid CardId { get; set; }

        public Guid ProductId { get; set; }

        public virtual CardModel? CardModel { get; set; }

        public virtual required ProductModel ProductModel { get; set; }
    }
}
