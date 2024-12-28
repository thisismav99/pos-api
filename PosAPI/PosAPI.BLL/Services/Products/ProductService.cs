using PosAPI.BLL.ServiceInterfaces.Products;
using PosAPI.DAL;
using PosAPI.DAL.Models.Products;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;

namespace PosAPI.BLL.Services.Products
{
    public class ProductService : IProductService
    {
        #region Variables
        private readonly IRepository<ProductModel, PosDbContext> _repository;
        private readonly IUnitOfWork<PosDbContext> _unitOfWork;
        #endregion

        #region Constructor
        public ProductService(IRepository<ProductModel, PosDbContext> repository,
                              IUnitOfWork<PosDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Dictionary<bool, string>> AddProduct(ProductModel productModel)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                await _repository.Add(productModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Product added successfully");

                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                return result;
            }
        }

        public async Task<Dictionary<bool, string>> DeleteProduct(Guid id)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                await _repository.Delete(id);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Product deleted successfully");

                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                return result;
            }
        }

        public async Task<ProductModel?> GetProduct(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<ProductModel>?> GetProducts()
        {
            return await _repository.GetAll();
        }

        public async Task<Dictionary<bool, string>> UpdateProduct(ProductModel productModel)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                _repository.Update(productModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Product updated successfully");

                return result;
            }
            catch (Exception ex) 
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                return result;
            }
        }
        #endregion
    }
}
