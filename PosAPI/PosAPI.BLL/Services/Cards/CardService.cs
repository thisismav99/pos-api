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
        private readonly IRepository<CardModel, PosDbContext> _cardRepository;
        private readonly IUnitOfWork<PosDbContext> _unitOfWork;
        #endregion

        #region Constructor
        public CardService(IRepository<CardModel, PosDbContext> cardRepository,
                           IUnitOfWork<PosDbContext> unitOfWork)
        {
            _cardRepository = cardRepository;
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
                await _cardRepository.Add(cardModel);
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
                await _cardRepository.Delete(id);
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
            return _cardRepository.Get(id);
        }

        public Task<List<CardModel>?> GetCards()
        {
            return _cardRepository.GetAll();
        }

        public async Task<Dictionary<bool, string>> UpdateCard(CardModel cardModel)
        {
            var result = new Dictionary<bool, string>();

            try
            {
                await _unitOfWork.BeginTransaction();
                _cardRepository.Update(cardModel);
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
