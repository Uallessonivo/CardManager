using CardManager.Application.Interfaces;
using CardManager.Domain.Enums;
using CardManager.Presentation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CardManager.Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var cards = await _cardService.GetAllAsync();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var card = await _cardService.GetByIdAsync(id);
                return Ok(card);
            } 
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCardByType([FromRoute] CardType type)
        {
            var cards = await _cardService.GetAllByType(type);
            return Ok(cards);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CardDTO cardDTO)
        {
            await _cardService.CreateCardAsync(cardDTO);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id,[FromBody] CardDTO cardDTO)
        {
            await _cardService.UpdateCardAsync(id, cardDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            await _cardService.DeleteCardAsync(id);
            return Ok();
        }
    }
}
