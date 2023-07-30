using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CardManager.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        [HttpGet("list-cards")]
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

        [HttpGet("list-cards-paginated")]
        public async Task<IActionResult> GetCards([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var cards = await _cardService.GetAllPaginatedAsync(pageNumber, pageSize);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return StatusCode(NotFound().StatusCode, ex.Message);
            }
        }

        [HttpGet("filter-card-id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid id)
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

        [HttpGet("filter-card-type")]
        public async Task<IActionResult> GetCardByType([FromQuery] CardType type)
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

        [HttpGet("filter-card-cpf")]
        public async Task<IActionResult> GetCardByOwnerCpf([FromQuery] string owner)
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

        [HttpGet("generate-report")]
        public async Task<FileResult> DownloadReport([FromQuery] string type)
        {

            var csvData = await _cardService.GenerateReport(type);
            var bytes = Encoding.UTF8.GetBytes(csvData.ToString());
            var stream = new MemoryStream(bytes);
            var contentType = "text/csv";
            var fileName = $"{DateTime.Now:yyyyMMdd}-{type}.csv";

            return File(stream, contentType, fileName);
        }

        [HttpPost("create-card")]
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

        [HttpPost("seed-database")]
        public async Task<IActionResult> UploadFileAndSeedDatabase(IFormFile file)
        {
            try
            {
                var response = await _cardService.SeedDatabaseTask(file);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpPost("update-card")]
        public async Task<IActionResult> UpdateCard([FromQuery] Guid id, [FromBody] UpdateCardDto updateCardDto)
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

        [HttpDelete("delete-card")]
        public async Task<IActionResult> DeleteCard([FromQuery] Guid id)
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
        
        [HttpDelete("delete-card-by")]
        public async Task<IActionResult> DeleteCard([FromQuery] string cardSerial)
        {
            try
            {
                await _cardService.DeleteCardAsync(cardSerial);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }

        [HttpDelete("delete-all-cards")]
        public async Task<IActionResult> DeleteAllCards()
        {
            try
            {
                await _cardService.DeleteAllCardsAsync();
                return Ok("Todos os cartões foram apagados da base.");
            }
            catch (Exception ex)
            {
                return StatusCode(BadRequest().StatusCode, ex.Message);
            }
        }
    }
}