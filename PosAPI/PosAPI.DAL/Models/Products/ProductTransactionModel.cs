using PosAPI.DAL.Models.Transactions;

namespace PosAPI.DAL.Models.Products
{
    public class ProductTransactionModel : BaseModel
    {
        public Guid TransactionId { get; set; }

        public Guid ProductId { get; set; }

        public virtual required TransactionModel TransactionModel { get; set; }

        public virtual required ProductModel ProductModel { get; set; }
    }
}
