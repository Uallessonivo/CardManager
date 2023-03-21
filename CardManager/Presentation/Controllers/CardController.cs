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
            try
            {
                var cards = await _cardService.GetAllAsync();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
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
                return StatusCode(NotFound().StatusCode, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCardByType([FromRoute] CardType type)
        {
            try
            {
                var cards = await _cardService.GetAllByType(type);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(NotFound().StatusCode, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CardDTO cardDto)
        {
            try
            {
                await _cardService.CreateCardAsync(cardDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id,[FromBody] CardDTO cardDto)
        {
            try
            {
                await _cardService.UpdateCardAsync(id, cardDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            try
            {
                await _cardService.DeleteCardAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCardByOwnerCpf([FromRoute] string ownerCpfName)
        {
            try
            {
                var cards = await _cardService.GetByOwnerCpfAsync(ownerCpfName);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(NotFound().StatusCode, ex.Message);
            }
        }
    }
}
