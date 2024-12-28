namespace PosAPI.BLL.ServiceInterfaces.Products
{
    public interface IProductTransactionService
    {
        Task<Dictionary<bool, string>> AddProductTransaction(Guid transactionId, Guid productId);
    }
}
