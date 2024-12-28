using PosAPI.DAL.Models.Products;

namespace PosAPI.BLL.ServiceInterfaces.Products
{
    public interface IProductService
    {
        Task<Dictionary<bool, string>> AddProduct(ProductModel productModel);
        Task<Dictionary<bool, string>> DeleteProduct(Guid id);
        Task<Dictionary<bool, string>> UpdateProduct(ProductModel productModel);
        Task<ProductModel?> GetProduct(Guid id);
        Task<List<ProductModel>?> GetProducts();
    }
}
