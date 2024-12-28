using PosAPI.BLL.ServiceInterfaces.Cards;
using PosAPI.DAL;
using PosAPI.DAL.Models.Cards;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;

namespace PosAPI.BLL.Services.Cards
{
    public class CardService : ICardService
    {
        #region Variables
        private readonly IRepository<CardModel, PosDbContext> _repository;
        private readonly IUnitOfWork<PosDbContext> _unitOfWork;
        #endregion

        #region Constructor
        public CardService(IRepository<CardModel, PosDbContext> repository,
                           IUnitOfWork<PosDbContext> unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public async Task<Dictionary<bool, string>> AddCard(CardModel cardModel)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                await _repository.Add(cardModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Card added successfully");

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                return result;
            }
        }

        public async Task<Dictionary<bool, string>> DeleteCard(Guid id)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                await _repository.Delete(id);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Card deleted successfully");

                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                return result;
            }
        }

        public Task<CardModel?> GetCard(Guid id)
        {
            return _repository.Get(id);
        }

        public Task<List<CardModel>?> GetCards()
        {
            return _repository.GetAll();
        }

        public async Task<Dictionary<bool, string>> UpdateCard(CardModel cardModel)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                _repository.Update(cardModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Card updated successfully");

                return result;
            }
            catch(Exception ex)
            { 
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                return result;
            }
        }
        #endregion
    }
}
