using Microsoft.Extensions.Logging;
using PosAPI.BLL.Helpers;
using PosAPI.BLL.ServiceInterfaces.Products;
using PosAPI.DAL;
using PosAPI.DAL.Models.Products;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;

namespace PosAPI.BLL.Services.Products
{
    public class ProductTransactionService : IProductTransactionService
    {
        #region Variables
        private readonly IRepository<ProductTransactionModel, PosDbContext> _productTransactionRepository;
        private readonly IRepository<ProductModel, PosDbContext> _productRepository;
        private readonly IUnitOfWork<PosDbContext> _unitOfWork;
        private readonly ILogger<ProductTransactionService> _logger;
        #endregion

        #region Constructor
        public ProductTransactionService(IRepository<ProductTransactionModel, PosDbContext> productTransactionRepository,
                                         IRepository<ProductModel, PosDbContext> productRepository,
                                         IUnitOfWork<PosDbContext> unitOfWork,
                                         ILogger<ProductTransactionService> logger)
        {
            _productTransactionRepository = productTransactionRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<(bool, string, Guid?)> AddProductTransactionTemp(Guid productId, string email)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("AddProductTransactionTemp", null, 1));

            try
            {
                _logger.LogDebug(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                var product = await _productRepository.Get(productId);

                if (product is not null)
                {
                    var productTransactionTemp = new ProductTransactionModel()
                    {
                        TransactionId = null,
                        ProductId = product.Id,
                        IsSaved = false,
                        CreatedBy = email,
                        DateCreated = DateTime.Now
                    };
                    await _productTransactionRepository.Add(productTransactionTemp);
                    await _unitOfWork.SaveChanges();
                    await _unitOfWork.CommitTransaction();

                    var result = (true, "Product Transaction added temporarily", productTransactionTemp.Id);

                    _logger.LogInformation(LoggerHelper.LoggerMessage("AddProductTransactionTemp", null, 3));

                    return result;
                }
                else
                    throw new NullReferenceException();
            }
            catch(Exception ex) 
            { 
                await _unitOfWork.RollbackTransaction(); 

                var result = (false, ex.Message, (Guid?)null);

                _logger.LogError(LoggerHelper.LoggerMessage("AddProductTransactionTemp", ex.Message, 4));

                return result;
            }
        }
        #endregion
    }
}
