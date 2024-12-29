using PosAPI.DAL.Models.Transactions;

namespace PosAPI.DAL.Models.Products
{
    public class ProductTransactionModel : BaseModel
    {
        public Guid? TransactionId { get; set; }

        public Guid ProductId { get; set; }

        public bool IsSaved { get; set; }

        public virtual TransactionModel? TransactionModel { get; set; }

        public virtual ProductModel? ProductModel { get; set; }
    }
}
