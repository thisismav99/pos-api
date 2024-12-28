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
        private readonly IRepository<ProductTransactionModel, PosDbContext> _repository;
        private readonly IUnitOfWork<PosDbContext> _unitOfWork;
        #endregion

        #region Constructor
        public ProductTransactionService(IRepository<ProductTransactionModel, PosDbContext> repository,
                                         IUnitOfWork<PosDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public Task<Dictionary<bool, string>> AddProductTransaction(Guid transactionId, Guid productId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
