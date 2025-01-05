using Microsoft.AspNetCore.Mvc;
using PosAPI.BLL.ServiceInterfaces.Cards;
using PosAPI.DAL;
using PosAPI.DAL.Models.Cards;
using PosAPI.Model;
using PosAPI.Model.ViewModels;

namespace PosAPI.Controllers.Chatime
{
    [Route("api/chatime")]
    public class CardController : BaseController
    {
        #region Variables
        private readonly ICardService<PosDbContext> _cardService;
        #endregion

        #region Constructor
        public CardController(ICardService<PosDbContext> cardService,
                              LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _cardService = cardService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("card/{id:guid}")]
        public async Task<IActionResult> GetCard(Guid id)
        {
            var card = await _cardService.GetCard(id);
            var result = new CardViewModel()
            {
                Card = card,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("Self", "GetCard", "Card", id),
                    GenerateLink("List", "GetCards", "Card", null),
                    GenerateLink("Add", "AddCard", "Card", null),
                    GenerateLink("Update", "UpdateCard", "Card", null),
                    GenerateLink("Delete", "DeleteCard", "Card", id)
                }
            };

            if (card is null)
                return NotFound(result);
            else
                return Ok(result);
        }

        [HttpGet]
        [Route("cards")]
        public async Task<IActionResult> GetCards()
        {
            var cards = await _cardService.GetCards();
            var result = new CardViewModel()
            {
                Cards = cards,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetCard", "Card", Guid.NewGuid()),
                    GenerateLink("Self", "GetCards", "Card", null),
                    GenerateLink("Add", "AddCard", "Card", null),
                    GenerateLink("Update", "UpdateCard", "Card", null),
                    GenerateLink("Delete", "DeleteCard", "Card", Guid.NewGuid())
                }
            };

            if (cards is null) 
                return NotFound(result);
            else 
                return Ok(result);
        }

        [HttpPost]
        [Route("card/add")]
        public async Task<IActionResult> AddCard([FromBody]CardModel cardModel)
        {
            var card = await _cardService.AddCard(cardModel);
            var result = new CardViewModel()
            {
                Message = card.Any(x => x.Key == true) ? card.FirstOrDefault(x => x.Key == true).Value :
                          card.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                    {
                        GenerateLink("ById", "GetCard", "Card", Guid.NewGuid()),
                        GenerateLink("Self", "GetCards", "Card", null),
                        GenerateLink("Add", "AddCard", "Card", null),
                        GenerateLink("Update", "UpdateCard", "Card", null),
                        GenerateLink("Delete", "DeleteCard", "Card", Guid.NewGuid())
                    }
            };

            if (card.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPut]
        [Route("card/update")]
        public async Task<IActionResult> UpdateCard([FromBody]CardModel cardModel)
        {
            var card = await _cardService.UpdateCard(cardModel);
            var result = new CardViewModel()
            {
                Message = card.Any(x => x.Key == true) ? card.FirstOrDefault(x => x.Key == true).Value :
                          card.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetCard", "Card", Guid.NewGuid()),
                    GenerateLink("Self", "GetCards", "Card", null),
                    GenerateLink("Add", "AddCard", "Card", null),
                    GenerateLink("Update", "UpdateCard", "Card", null),
                    GenerateLink("Delete", "DeleteCard", "Card", Guid.NewGuid())
                }
            };

            if (card.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpDelete]
        [Route("card/delete/{id:guid}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var card = await _cardService.DeleteCard(id);
            var result = new CardViewModel()
            {
                Message = card.Any(x => x.Key == true) ? card.FirstOrDefault(x => x.Key == true).Value :
                          card.FirstOrDefault(x => x.Key == false).Value,
                LinkModel = new List<LinkModel>()
                {
                    GenerateLink("ById", "GetCard", "Card", id),
                    GenerateLink("Self", "GetCards", "Card", null),
                    GenerateLink("Add", "AddCard", "Card", null),
                    GenerateLink("Update", "UpdateCard", "Card", null),
                    GenerateLink("Delete", "DeleteCard", "Card", id)
                }
            };

            if (card.Any(x => x.Key == false))
                return BadRequest(result);
            else
                return Ok(result);
        }
        #endregion
    }
}
