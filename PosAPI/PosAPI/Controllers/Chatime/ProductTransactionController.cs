using Microsoft.AspNetCore.Mvc;
using PosAPI.BLL.ServiceInterfaces.Products;
using PosAPI.DAL;
using PosAPI.Models.ViewModels;
using PosAPI.Models;

namespace PosAPI.Controllers.Chatime
{
    [Route("api/[controller]")]
    public class ProductTransactionController : BaseController
    {
        #region Variables
        private readonly IProductTransactionService<PosDbContext> _productTransactionService;
        #endregion

        #region Constructor
        public ProductTransactionController(IProductTransactionService<PosDbContext> productTransactionService,
                                            LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _productTransactionService = productTransactionService;
        }
        #endregion

        #region Methods
        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> AddProductTransaction([FromQuery]Guid productId, [FromQuery]string email)
        {
            var productTransaction = await _productTransactionService.AddProductTransactionTemp(productId, email);
            var result = new ProductTransactionViewModel()
            {
                Message = productTransaction.Item2,
                ProductTransactionId = productTransaction.Item3,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("Self", "AddProductTransaction", "ProductTransaction", null)
                }
            };

            if (!productTransaction.Item1)
                return BadRequest(result);
            else
                return Ok(result);

        }
        #endregion
    }
}
