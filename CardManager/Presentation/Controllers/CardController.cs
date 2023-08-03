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
        private readonly ResponseDto _responseDto;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
            _responseDto = new ResponseDto();
        }

        [HttpGet("list-cards")]
        public async Task<ResponseDto> GetCards()
        {
            try
            {
                var cards = await _cardService.GetAllAsync();
                _responseDto.Result = cards;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet("list-cards-paginated")]
        public async Task<ResponseDto> GetCards([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var cards = await _cardService.GetAllPaginatedAsync(pageNumber, pageSize);
                _responseDto.Result = cards;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet("filter-card-id")]
        public async Task<ResponseDto> GetByIdAsync([FromQuery] Guid id)
        {
            try
            {
                var card = await _cardService.GetByIdAsync(id);
                _responseDto.Result = card;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet("filter-card-type")]
        public async Task<ResponseDto> GetCardByType([FromQuery] CardType type)
        {
            try
            {
                var cards = await _cardService.GetAllByType(type);
                _responseDto.Result = cards;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet("filter-card-cpf")]
        public async Task<ResponseDto> GetCardByOwnerCpf([FromQuery] string owner)
        {
            try
            {
                var cards = await _cardService.GetByOwnerCpfAsync(owner);
                _responseDto.Result = cards;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet("generate-report")]
        public async Task<ResponseDto> DownloadReport([FromQuery] string type)
        {
            var csvData = await _cardService.GenerateReport(type);
            var bytes = Encoding.UTF8.GetBytes(csvData.ToString());
            var stream = new MemoryStream(bytes);

            _responseDto.Result = stream;
            return _responseDto;
        }

        [HttpPost("create-card")]
        public async Task<ResponseDto> CreateCard([FromBody] CardDto cardDto)
        {
            try
            {
                await _cardService.CreateCardAsync(cardDto);
                _responseDto.Message = "Cartão criado com sucesso.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPost("seed-database")]
        public async Task<ResponseDto> UploadFileAndSeedDatabase(IFormFile file)
        {
            try
            {
                var response = await _cardService.SeedDatabaseTask(file);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPost("update-card")]
        public async Task<ResponseDto> UpdateCard([FromQuery] Guid id, [FromBody] UpdateCardDto updateCardDto)
        {
            try
            {
                await _cardService.UpdateCardAsync(id, updateCardDto);
                _responseDto.Message = "Cartão atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            
            return _responseDto;
        }

        [HttpDelete("delete-card")]
        public async Task<ResponseDto> DeleteCard([FromQuery] Guid id)
        {
            try
            {
                await _cardService.DeleteCardAsync(id);
                _responseDto.Message = "Cartão apagado com sucesso.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            
            return _responseDto;
        }

        [HttpDelete("delete-card-by")]
        public async Task<ResponseDto> DeleteCard([FromQuery] string cardSerial)
        {
            try
            {
                await _cardService.DeleteCardAsync(cardSerial);
                _responseDto.Message = "Cartão apagado com sucesso.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            
            return _responseDto;
        }

        [HttpDelete("delete-all-cards")]
        public async Task<ResponseDto> DeleteAllCards()
        {
            try
            {
                await _cardService.DeleteAllCardsAsync();
                _responseDto.Message = "Todos os cartões foram apagados com sucesso.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            
            return _responseDto;
        }
    }
}