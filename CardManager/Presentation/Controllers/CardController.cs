using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CardManager.Domain.Enums;
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

        [HttpGet("list")]
        public async Task<IActionResult> GetCards()
        {
            try
            {
                var cards = await _cardService.GetAllAsync();
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(NotFound().StatusCode, ex.Message);
            }
        }

        [HttpGet("filter/id/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
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

        [HttpGet("filter/type/{type}")]
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
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

        [HttpPost("upload/seed-database")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                await _cardService.SeedDatabaseTask(file);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id,[FromBody] UpdateCardDto updateCardDto)
        {
            try
            {
                await _cardService.UpdateCardAsync(id, updateCardDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
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

        [HttpGet("filter/owner/{owner}")]
        public async Task<IActionResult> GetCardByOwnerCpf([FromRoute] string owner)
        {
            try
            {
                var cards = await _cardService.GetByOwnerCpfAsync(owner);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(NotFound().StatusCode, ex.Message);
            }
        }
    }
}
