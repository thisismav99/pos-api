namespace PosAPI.Models.RequestModels
{
    public class AddAccountRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
