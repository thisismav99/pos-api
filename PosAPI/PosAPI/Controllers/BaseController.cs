using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosAPI.Models;

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        #region Variables
        private readonly LinkGenerator _linkGenerator;
        #endregion

        #region Constructor
        public BaseController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;

        }
        #endregion

        #region Methods
        protected LinkModel GenerateLink(string rel, string method, string controller, Guid? id)
        {
            var url = id is not null ? _linkGenerator.GetUriByAction(HttpContext, method, controller, new { id }) :
                      _linkGenerator.GetUriByAction(HttpContext, method, controller);

            return new LinkModel()
            {
                Rel = rel,
                Url = url
            };
        }
        #endregion
    }
}
