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
        #endregion

        #region Constructor
        public ProductTransactionService(IRepository<ProductTransactionModel, PosDbContext> productTransactionRepository,
                                         IRepository<ProductModel, PosDbContext> productRepository,
                                         IUnitOfWork<PosDbContext> unitOfWork)
        {
            _productTransactionRepository = productTransactionRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<(bool, string, Guid?)> AddProductTransactionTemp(Guid productId, string email)
        {
            try
            {
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

                    return result;
                }
                else
                    throw new NullReferenceException();
            }
            catch(Exception ex) 
            { 
                await _unitOfWork.RollbackTransaction(); 

                var result = (false, ex.Message, (Guid?)null);

                return result;
            }
        }
        #endregion
    }
}
