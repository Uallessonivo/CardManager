using CardManager.Web.Models.Dtos;

namespace CardManager.Web.Services.IService;

public interface ICardService
{
    Task<ResponseDto> CreateNewCardAsync(CardDto newCardData);
    Task<ResponseDto> UpdateDatabaseAsync(IFormFile fileData);
    Task<GenerateFileResponseDto> GenerateCsvReport(string cardType);
    Task<ResponseDto> GetCardAsync(string owner);
    Task<ResponseDto> GetAllCardsAsync();
    Task<ResponseDto> DeleteCardAsync(string cardSerial);
    Task<ResponseDto> DeleteAllCardsAsync();
}