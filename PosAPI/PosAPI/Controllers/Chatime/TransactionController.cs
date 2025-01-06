using Microsoft.AspNetCore.Mvc;
using PosAPI.BLL.ServiceInterfaces.Transactions;
using PosAPI.DAL;
using PosAPI.Models;
using PosAPI.Models.RequestModels;
using PosAPI.Models.ViewModels;

namespace PosAPI.Controllers.Chatime
{
    [Route("api/chatime")]
    public class TransactionController : BaseController
    {
        #region Variables
        private readonly ITransactionService<PosDbContext> _transactionService;
        #endregion

        #region Constructor
        public TransactionController(ITransactionService<PosDbContext> transactionService,
                                     LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _transactionService = transactionService;
        }
        #endregion

        #region Methods
        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> AddTransaction([FromBody]TransactionRequestModel transactionRequestModel)
        {
            var transaction = await _transactionService.AddTransaction(transactionRequestModel.Transaction, transactionRequestModel.ProductTransactionId);
            var result = new TransactionViewModel()
            {
                Message = transaction.Any(x => x.Key == true) ? transaction.FirstOrDefault(x => x.Key == true).Value :
                          transaction.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("Self", "AddTransaction", "Transaction", null)
                }
            };

            if (transaction.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }
        #endregion
    }
}
