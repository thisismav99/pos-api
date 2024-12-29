using PosAPI.DAL.Models.Transactions;

namespace PosAPI.BLL.ServiceInterfaces.Transactions
{
    public interface ITransactionService
    {
        Task<Dictionary<bool, string>> AddTransaction(TransactionModel transactionModel, List<Guid> productTransactionId);
    }
}
