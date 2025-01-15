namespace PosAPI.Models.ViewModels
{
    public class TokenViewModel
    {
        public string? Token { get; set; }
        public string? TokenError { get; set; }
        public required List<LinkModel> LinkModel { get; set; }
    }
}
