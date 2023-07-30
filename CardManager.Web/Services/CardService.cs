using System.Text;
using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;

namespace CardManager.Web.Services;

public class CardService : ICardService
{
    private readonly HttpClient _httpClient;

    public CardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateNewCardAsync(CardDto newCardData)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateDatabaseAsync(IFormFile fileData)
    {
        throw new NotImplementedException();
    }

    public async Task<StringBuilder> GenerateReport(string cardType)
    {
        throw new NotImplementedException();
    }

    public async Task<CardDto> GetCardAsync(string filter)
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