using PosAPI.DAL.Models.Cards;

namespace PosAPI.Models.ViewModels
{
    public class CardViewModel
    {
        public CardModel? Card { get; set; }
        public List<CardModel>? Cards { get; set; }
        public string? Message { get; set; }
        public required List<LinkModel> LinkModel { get; set; }
    }
}
