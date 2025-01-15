using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosAPI.BLL.ServiceInterfaces.JwtToken;
using PosAPI.Models;
using PosAPI.Models.RequestModels;
using PosAPI.Models.ViewModels;

namespace PosAPI.Controllers.Auths
{
    [Route("api/auth")]
    public class TokenController : BaseController
    {
        #region Variables
        private readonly IJwtTokenService _jwtTokenService;
        #endregion

        #region Constructor
        public TokenController(LinkGenerator linkGenerator,
                               IJwtTokenService jwtTokenService) : base(linkGenerator)
        {
            _jwtTokenService = jwtTokenService;
        }
        #endregion

        #region Methods
        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> GenerateToken([FromBody]TokenRequestModel tokenRequestModel)
        {
            var token = await _jwtTokenService.GenerateJwtToken(tokenRequestModel.Email, tokenRequestModel.Password);

            if(token.Any(x => x.Key == true))
            {
                var result = new TokenViewModel()
                {
                    Token = token.FirstOrDefault(x => x.Key == true).Value,
                    TokenError = null,
                    LinkModel = new List<LinkModel>()
                    {
                        GenerateLink("Self", "GenerateToken", "Token", null)
                    }
                };

                return Ok(result);
            }
            else
            {
                var result = new TokenViewModel()
                {
                    Token = null,
                    TokenError = token.FirstOrDefault(x => x.Key == false).Value,
                    LinkModel = new List<LinkModel>()
                    {
                        GenerateLink("Self", "GenerateToken", "Token", null)
                    }
                };

                return BadRequest(result);
            }
        }
        #endregion
    }
}
