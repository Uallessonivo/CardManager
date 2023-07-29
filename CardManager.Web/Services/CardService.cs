using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;

namespace CardManager.Web.Services;

public class CardService : ICardService
{
    public async Task<bool> CreateNewCardAsync(CardDto newCardData)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateDatabaseAsync(IFormFile fileData)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CardDto>> GetCardsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CardDto> GetCardBySerialAsync(string cardSerial)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCardAsync(string cardSerial)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAllCardsAsync()
    {
        throw new NotImplementedException();
    }
}