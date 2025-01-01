using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PosAPI.BLL.Helpers;
using PosAPI.BLL.ServiceInterfaces.Products;
using PosAPI.DAL.Models.Products;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;

namespace PosAPI.BLL.Services.Products
{
    public class ProductService<TContext> : IProductService<TContext> 
        where TContext : DbContext
    {
        #region Variables
        private readonly IRepository<ProductModel, TContext> _productRepository;
        private readonly IUnitOfWork<TContext> _unitOfWork;
        private readonly ILogger<ProductService<TContext>> _logger;
        #endregion

        #region Constructor
        public ProductService(IRepository<ProductModel, TContext> productRepository,
                              IUnitOfWork<TContext> unitOfWork,
                              ILogger<ProductService<TContext>> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<Dictionary<bool, string>> AddProduct(ProductModel productModel)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("AddProduct", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogDebug(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                await _productRepository.Add(productModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Product added successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("AddProduct", null, 3));

                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("AddProduct", ex.Message, 4));

                return result;
            }
        }

        public async Task<Dictionary<bool, string>> DeleteProduct(Guid id)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("DeleteProduct", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogDebug(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                await _productRepository.Delete(id);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Product deleted successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("DeleteProduct", null, 3));

                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("DeleteProduct", ex.Message, 4));

                return result;
            }
        }

        public async Task<ProductModel?> GetProduct(Guid id)
        {
            return await _productRepository.Get(id);
        }

        public async Task<List<ProductModel>?> GetProducts()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Dictionary<bool, string>> UpdateProduct(ProductModel productModel)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("UpdateProduct", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogDebug(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                _productRepository.Update(productModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Product updated successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("UpdateProduct", null, 3));

                return result;
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("UpdateProduct", ex.Message, 4));

                return result;
            }
        }
        #endregion
    }
}
