using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PosAPI.Models.ViewModels;
using PosAPI.Models;
using Microsoft.EntityFrameworkCore;
using PosAPI.BLL.ServiceInterfaces.JwtToken;
using PosAPI.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;

namespace PosAPI.Controllers.Users
{
    [Route("api/users")]
    public class AccountController : BaseController
    {
        #region Variables
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        #endregion

        #region Constructor
        public AccountController(UserManager<IdentityUser> userManager,
                                 IJwtTokenService jwtTokenService,
                                 LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _userManager = userManager;   
            _jwtTokenService = jwtTokenService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("[controller]/{id:guid}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var account = await _userManager.FindByIdAsync(id.ToString());
            var result = new AccountViewModel()
            {
                IdentityUser = account,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("Self", "GetAccountById", "Account", id),
                    GenerateLink("ByEmail", "GetAccountByEmail", "Account", id),
                    GenerateLink("List", "GetAccounts", "Account", null),
                    GenerateLink("Add", "AddAccount", "Account", null),
                    GenerateLink("Delete", "DeleteAccount", "Account", id)
                }
            };

            if(account is null)
                return NotFound(result);
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("[controller]/{email}")]
        public async Task<IActionResult> GetAccountByEmail(string email)
        {
            var account = await _userManager.FindByEmailAsync(email);
            var result = new AccountViewModel()
            {
                IdentityUser = account,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetAccountById", "Account", Guid.NewGuid()),
                    GenerateLink("Self", "GetAccountByEmail", "Account", null),
                    GenerateLink("List", "GetAccounts", "Account", null),
                    GenerateLink("Add", "AddAccount", "Account", null),
                    GenerateLink("Delete", "DeleteAccount", "Account", Guid.NewGuid())
                }
            };

            if (account is null)
                return NotFound(result);
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _userManager.Users.ToListAsync();
            var result = new AccountViewModel()
            {
                IdentityUsers = accounts,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetAccountById", "Account", Guid.NewGuid()),
                    GenerateLink("ByEmail", "GetAccountByEmail", "Account", null),
                    GenerateLink("Self", "GetAccounts", "Account", null),
                    GenerateLink("Add", "AddAccount", "Account", null),
                    GenerateLink("Delete", "DeleteAccount", "Account", Guid.NewGuid())
                }
            };

            if (accounts is null)
                return NotFound(result);
            else
                return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]/register")]
        public async Task<IActionResult> AddAccount([FromBody]AddAccountRequestModel addAccountRequestModel)
        {
            var identityUser = new IdentityUser
            {
                Email = addAccountRequestModel.Email,
                UserName = addAccountRequestModel.Email
            };

            var account = await _userManager.CreateAsync(identityUser, addAccountRequestModel.Password);
            
            if(account.Succeeded)
            {
                var token = await _jwtTokenService.GenerateJwtToken(addAccountRequestModel.Email, addAccountRequestModel.Password);

                if(token.Any(x => x.Key == true))
                {
                    var result = new AccountViewModel()
                    {
                        IdentityUser = await _userManager.FindByEmailAsync(addAccountRequestModel.Email),
                        IdentityErrors = null,
                        Token = token.FirstOrDefault(x => x.Key == true).Value,
                        TokenError = null,
                        LinkModel = new List<LinkModel>()
                        {
                            GenerateLink("ById", "GetAccountById", "Account", Guid.NewGuid()),
                            GenerateLink("ByEmail", "GetAccountByEmail", "Account", null),
                            GenerateLink("List", "GetAccounts", "Account", null),
                            GenerateLink("Self", "AddAccount", "Account", null),
                            GenerateLink("Delete", "DeleteAccount", "Account", Guid.NewGuid())
                        }
                    };

                    return Ok(result);
                }
                else
                {
                    var result = new AccountViewModel()
                    {
                        IdentityErrors = null,
                        Token = null,
                        TokenError = token.FirstOrDefault(x => x.Key == false).Value,
                        LinkModel = new List<LinkModel>()
                        {
                            GenerateLink("ById", "GetAccountById", "Account", Guid.NewGuid()),
                            GenerateLink("ByEmail", "GetAccountByEmail", "Account", null),
                            GenerateLink("List", "GetAccounts", "Account", null),
                            GenerateLink("Self", "AddAccount", "Account", null),
                            GenerateLink("Delete", "DeleteAccount", "Account", Guid.NewGuid())
                        }
                    };

                    return BadRequest(result);
                }
            }
            else
            {
                var result = new AccountViewModel()
                {
                    IdentityErrors = account.Errors.ToList(),
                    Token = null,
                    TokenError = null,
                    LinkModel = new List<LinkModel>()
                    {
                        GenerateLink("ById", "GetAccountById", "Account", Guid.NewGuid()),
                        GenerateLink("ByEmail", "GetAccountByEmail", "Account", null),
                        GenerateLink("List", "GetAccounts", "Account", null),
                        GenerateLink("Self", "AddAccount", "Account", null),
                        GenerateLink("Delete", "DeleteAccount", "Account", Guid.NewGuid())
                    }
                };

                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("[controller]/delete/{id:guid}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var account = await _userManager.FindByIdAsync(id.ToString());

            if (account is not null)
            {
                var deleteAccount = await _userManager.DeleteAsync(account);
                var result = new AccountViewModel()
                {
                    IdentityErrors = !deleteAccount.Succeeded ? deleteAccount.Errors.ToList() : null,
                    LinkModel = new List<LinkModel>()
                    {
                        GenerateLink("ById", "GetAccountById", "Account", Guid.NewGuid()),
                        GenerateLink("ByEmail", "GetAccountByEmail", "Account", null),
                        GenerateLink("List", "GetAccounts", "Account", null),
                        GenerateLink("Add", "AddAccount", "Account", null),
                        GenerateLink("Self", "DeleteAccount", "Account", Guid.NewGuid())
                    }
                };

                if (!deleteAccount.Succeeded)
                    return BadRequest(result);
                else
                    return Ok(result);
            }
            else
                return NotFound();
        }
        #endregion
    }
}
