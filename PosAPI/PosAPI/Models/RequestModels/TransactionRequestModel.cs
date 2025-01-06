using PosAPI.DAL.Models.Transactions;

namespace PosAPI.Models.RequestModels
{
    public class TransactionRequestModel
    {
        public required TransactionModel Transaction { get; set; }
        public required List<Guid> ProductTransactionId { get; set; }
    }
}
