using Microsoft.EntityFrameworkCore;
using PosAPI.DAL.Models.Transactions;

namespace PosAPI.BLL.ServiceInterfaces.Transactions
{
    public interface ITransactionService<TContext> where TContext : DbContext
    {
        Task<Dictionary<bool, string>> AddTransaction(TransactionModel transactionModel, List<Guid> productTransactionId);
    }
}
