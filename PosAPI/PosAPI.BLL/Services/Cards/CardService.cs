using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PosAPI.BLL.Helpers;
using PosAPI.BLL.ServiceInterfaces.Cards;
using PosAPI.DAL.Models.Cards;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;

namespace PosAPI.BLL.Services.Cards
{
    public class CardService<TContext> : ICardService<TContext> 
        where TContext : DbContext
    {
        #region Variables
        private readonly IRepository<CardModel, TContext> _cardRepository;
        private readonly IUnitOfWork<TContext> _unitOfWork;
        private readonly ILogger<CardService<TContext>> _logger;
        #endregion

        #region Constructor
        public CardService(IRepository<CardModel, TContext> cardRepository,
                           IUnitOfWork<TContext> unitOfWork,
                           ILogger<CardService<TContext>> logger)
        {
            _cardRepository = cardRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<Dictionary<bool, string>> AddCard(CardModel cardModel)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("AddCard", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogInformation(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                await _cardRepository.Add(cardModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Card added successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("AddCard", null, 3));

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("AddCard", ex.Message, 4));

                return result;
            }
        }

        public async Task<Dictionary<bool, string>> DeleteCard(Guid id)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("DeleteCard", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogInformation(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                await _cardRepository.Delete(id);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Card deleted successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("DeleteCard", null, 3));

                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("DeleteCard", ex.Message, 4));

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
            _logger.LogInformation(LoggerHelper.LoggerMessage("UpdateCard", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogInformation(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                _cardRepository.Update(cardModel);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                result.Add(true, "Card updated successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("UpdateCard", null, 3));

                return result;
            }
            catch(Exception ex)
            { 
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("UpdateCard", ex.Message, 4));

                return result;
            }
        }
        #endregion
    }
}
