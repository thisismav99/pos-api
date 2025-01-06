using PosAPI.DAL.Models.Products;

namespace PosAPI.Model.ViewModels
{
    public class ProductViewModel
    {
        public ProductModel? Product { get; set; }
        public List<ProductModel>? Products { get; set; }
        public string? Message { get; set; }
        public required List<LinkModel> LinkModel { get; set; }
    }
}
