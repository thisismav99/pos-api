namespace PosAPI.BLL.ServiceInterfaces.Products
{
    public interface IProductTransactionService
    {
        Task<(bool, string, Guid?)> AddProductTransactionTemp(Guid productId, string email);
    }
}
