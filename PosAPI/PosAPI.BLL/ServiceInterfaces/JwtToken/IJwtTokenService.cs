namespace PosAPI.BLL.ServiceInterfaces.JwtToken
{
    public interface IJwtTokenService
    {
        Task<Dictionary<bool, string>> GenerateJwtToken(string email, string password);
    }
}
