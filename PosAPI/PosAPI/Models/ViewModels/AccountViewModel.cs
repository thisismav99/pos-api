using Microsoft.AspNetCore.Identity;

namespace PosAPI.Models.ViewModels
{
    public class AccountViewModel
    {
        public IdentityUser? IdentityUser { get; set; }
        public List<IdentityUser>? IdentityUsers { get; set; }
        public string? Token { get; set; }
        public List<IdentityError>? IdentityErrors { get; set; }
        public string? TokenError { get; set; }
        public required List<LinkModel> LinkModel { get; set; }
    }
}
