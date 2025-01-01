using Microsoft.EntityFrameworkCore;

namespace PosAPI.BLL.ServiceInterfaces.Products
{
    public interface IProductTransactionService<TContext> where TContext : DbContext
    {
        Task<(bool, string, Guid?)> AddProductTransactionTemp(Guid productId, string email);
    }
}
