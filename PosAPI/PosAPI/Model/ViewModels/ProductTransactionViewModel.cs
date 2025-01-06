using PosAPI.DAL.Models.Products;

namespace PosAPI.Model.ViewModels
{
    public class ProductTransactionViewModel
    {
        public ProductTransactionModel? ProductTransaction { get; set; }
        public List<ProductTransactionModel>? ProductTransactions { get; set; }
        public string? Message { get; set; }
        public Guid? ProductTransactionId { get; set; }
        public required List<LinkModel> LinkModel { get; set; }
    }
}
