﻿using Microsoft.Extensions.Logging;
using PosAPI.BLL.Helpers;
using PosAPI.BLL.ServiceInterfaces.Transactions;
using PosAPI.DAL;
using PosAPI.DAL.Models.Products;
using PosAPI.DAL.Models.Transactions;
using PosAPI.DAL.Repositories;
using PosAPI.DAL.UnitOfWorks;

namespace PosAPI.BLL.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        #region Variables
        private readonly IRepository<TransactionModel, PosDbContext> _transactionRepository;
        private readonly IRepository<ProductTransactionModel, PosDbContext> _productTransactionRepository;
        private readonly IUnitOfWork<PosDbContext> _unitOfWork;
        private readonly ILogger<TransactionService> _logger;
        #endregion

        #region Constructor
        public TransactionService(IRepository<TransactionModel, PosDbContext> transactionRepository,
                                  IRepository<ProductTransactionModel, PosDbContext> productTransactionRepository,
                                  IUnitOfWork<PosDbContext> unitOfWork,
                                  ILogger<TransactionService> logger)
        {
            _transactionRepository = transactionRepository;
            _productTransactionRepository = productTransactionRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<Dictionary<bool, string>> AddTransaction(TransactionModel transactionModel, List<Guid> productTransactionId)
        {
            _logger.LogInformation(LoggerHelper.LoggerMessage("AddTransaction", null, 1));

            var result = new Dictionary<bool, string>();

            try
            {
                _logger.LogDebug(LoggerHelper.LoggerMessage(null, null, 2));

                await _unitOfWork.BeginTransaction();
                await _transactionRepository.Add(transactionModel);
                await _unitOfWork.SaveChanges();

                foreach(var id in productTransactionId)
                {
                    var productTransaction = await _productTransactionRepository.Get(id);
                    
                    if(productTransaction is not null)
                    {
                        productTransaction.TransactionId = transactionModel.Id;
                        productTransaction.IsSaved = true;

                        _productTransactionRepository.Update(productTransaction);
                        await _unitOfWork.SaveChanges();
                    }
                    else
                        throw new NullReferenceException();
                }

                await _unitOfWork.CommitTransaction();

                result.Add(true, "Transaction added successfully");

                _logger.LogInformation(LoggerHelper.LoggerMessage("AddTransaction", null, 3));

                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();

                result.Add(false, ex.Message);

                _logger.LogError(LoggerHelper.LoggerMessage("AddTransaction", ex.Message, 4));

                return result;
            }
        }
        #endregion
    }
}
