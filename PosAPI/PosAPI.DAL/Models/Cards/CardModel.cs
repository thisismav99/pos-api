namespace PosAPI.DAL.Models.Cards
{
    public class CardModel : BaseModel
    {
        public required string CardBankName { get; set; }

        public required string CardType { get; set; }

        public required string CardAccountName { get; set; }

        public required string CardAccountNumber { get; set; }

        public DateTime CardExpiry { get; set; }

        public int CardCvcNumber { get; set; }
    }
}
