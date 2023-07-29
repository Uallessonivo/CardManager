using CardManager.Web.Models.Dtos;

namespace CardManager.Web.Services.IService;

public interface ICardService
{
    Task<bool> CreateNewCardAsync(CardDto newCardData);
    Task<bool> UpdateDatabaseAsync(IFormFile fileData);
    Task<IEnumerable<CardDto>> GetCardsAsync();
    Task<CardDto> GetCardBySerialAsync(string cardSerial);
    Task<bool> DeleteCardAsync(string cardSerial);
    Task<bool> DeleteAllCardsAsync();
}