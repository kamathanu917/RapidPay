using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UATP_RapidPay.Interfaces.CardManagement;
using UATP_RapidPay.Models;

namespace UATP_RapidPay.Controllers.CardManagement
{
    [Route("api/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Card>> CreateCard()
        {
            var card = await _cardService.CreateCardAsync();
            return Ok(card);
        }

        [HttpPost("pay")]
        public async Task<ActionResult<bool>> Pay(int cardId, decimal amount)
        {
            var result = await _cardService.PayAsync(cardId, amount);
            if (result)
                return Ok("Payment successful");
            return BadRequest("Insufficient balance or invalid card");
        }

        [HttpGet("balance/{cardId}")]
        public async Task<ActionResult<decimal>> GetCardBalance(int cardId)
        {
            var balance = await _cardService.GetCardBalanceAsync(cardId);
            return Ok(balance);
        }
    }
}
