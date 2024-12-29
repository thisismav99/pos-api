namespace PosAPI.BLL.ServiceInterfaces.Products
{
    public interface IProductTransactionService
    {
        Task<Dictionary<bool, string>> AddProductTransactionTemp(Guid productId, string email);
    }
}
