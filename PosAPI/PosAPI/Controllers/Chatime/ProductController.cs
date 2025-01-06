using Microsoft.AspNetCore.Mvc;
using PosAPI.BLL.ServiceInterfaces.Products;
using PosAPI.DAL;
using PosAPI.Model.ViewModels;
using PosAPI.Model;
using PosAPI.DAL.Models.Products;

namespace PosAPI.Controllers.Chatime
{
    [Route("api/chatime")]
    public class ProductController : BaseController
    {
        #region Variables
        private readonly IProductService<PosDbContext> _productService;
        #endregion

        #region Constructor
        public ProductController(IProductService<PosDbContext> productService,
                                 LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _productService = productService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("[controller]/{id:guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProduct(id);
            var result = new ProductViewModel()
            {
                Product = product,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("Self", "GetProduct", "Product", Guid.NewGuid()),
                    GenerateLink("List", "GetProducts", "Product", null),
                    GenerateLink("Add", "AddProduct", "Product", null),
                    GenerateLink("Update", "UpdateProduct", "Product", null),
                    GenerateLink("Delete", "DeleteProduct", "Product", Guid.NewGuid())
                }
            };

            if (product is null)
                return NotFound(result);
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            var result = new ProductViewModel()
            {
                Products = products,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetProduct", "Product", Guid.NewGuid()),
                    GenerateLink("Self", "GetProducts", "Product", null),
                    GenerateLink("Add", "AddProduct", "Product", null),
                    GenerateLink("Update", "UpdateProduct", "Product", null),
                    GenerateLink("Delete", "DeleteProduct", "Product", Guid.NewGuid())
                }
            };

            if (products is null)
                return NotFound(result);
            else
                return Ok(result);
        }

        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> AddProduct([FromBody]ProductModel productModel)
        {
            var product = await _productService.AddProduct(productModel);
            var result = new ProductViewModel()
            {
                Message = product.Any(x => x.Key == true) ? product.FirstOrDefault(x => x.Key == true).Value :
                          product.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetProduct", "Product", Guid.NewGuid()),
                    GenerateLink("List", "GetProducts", "Product", null),
                    GenerateLink("Self", "AddProduct", "Product", null),
                    GenerateLink("Update", "UpdateProduct", "Product", null),
                    GenerateLink("Delete", "DeleteProduct", "Product", Guid.NewGuid())
                }
            };

            if (product.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPut]
        [Route("[controller]/update")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductModel productModel)
        {
            var product = await _productService.UpdateProduct(productModel);
            var result = new ProductViewModel()
            {
                Message = product.Any(x => x.Key == true) ? product.FirstOrDefault(x => x.Key == true).Value :
                          product.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetProduct", "Product", Guid.NewGuid()),
                    GenerateLink("List", "GetProducts", "Product", null),
                    GenerateLink("Add", "AddProduct", "Product", null),
                    GenerateLink("Self", "UpdateProduct", "Product", null),
                    GenerateLink("Delete", "DeleteProduct", "Product", Guid.NewGuid())
                }
            };

            if (product.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpDelete]
        [Route("[controller]/delete/{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.DeleteProduct(id);
            var result = new ProductViewModel()
            {
                Message = product.Any(x => x.Key == true) ? product.FirstOrDefault(x => x.Key == true).Value :
                          product.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetProduct", "Product", Guid.NewGuid()),
                    GenerateLink("List", "GetProducts", "Product", null),
                    GenerateLink("Add", "AddProduct", "Product", null),
                    GenerateLink("Update", "UpdateProduct", "Product", null),
                    GenerateLink("Self", "DeleteProduct", "Product", Guid.NewGuid())
                }
            };

            if (product.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }
        #endregion
    }
}
