using PosAPI.DAL.Models.Transactions;

namespace PosAPI.Models.ViewModels
{
    public class TransactionViewModel
    {
        public TransactionModel? Transaction { get; set; }
        public List<TransactionModel>? Transactions { get; set; }
        public string? Message { get; set; }
        public required List<LinkModel> LinkModel { get; set; }
    }
}
