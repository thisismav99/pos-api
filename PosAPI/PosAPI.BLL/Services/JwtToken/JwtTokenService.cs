using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PosAPI.BLL.Helpers;
using PosAPI.BLL.ServiceInterfaces.JwtToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PosAPI.BLL.Services.JwtToken
{
    public class JwtTokenService : IJwtTokenService
    {
        #region Variables
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtTokenService> _logger;
        #endregion

        #region Constructor
        public JwtTokenService(UserManager<IdentityUser> userManager,
                               IConfiguration configuration,
                               ILogger<JwtTokenService> logger)
        {
            _userManager = userManager;   
            _configuration = configuration;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<Dictionary<bool, string>> GenerateJwtToken(string email, string password)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("GenerateJwtToken", null, 1));
            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogInformation(LoggerHelper.LoggerMessage(null, null, 2));

                var identityUser = await _userManager.FindByEmailAsync(email);

                if(identityUser is null)
                {
                    result.Add(false, "Invalid Credentials");

                    _logger.LogWarning(LoggerHelper.LoggerMessage("GenerateJwtToken", "Invalid Credentials", 5));

                    return result;
                }

                var validUser = await _userManager.CheckPasswordAsync(identityUser!, password);

                if (!validUser)
                {
                    result.Add(false, "Invalid Credentials");

                    _logger.LogWarning(LoggerHelper.LoggerMessage("GenerateJwtToken", "Invalid Credentials", 5));

                    return result;
                }
                else
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: credentials);

                    result.Add(true, new JwtSecurityTokenHandler().WriteToken(token));

                    _logger.LogInformation(LoggerHelper.LoggerMessage("GenerateJwtToken", null, 3));

                    return result;
                }
            }
            catch(Exception ex)
            {
                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("GenerateJwtToken", ex.Message, 4));

                return result;
            }
        }
        #endregion
    }
}
