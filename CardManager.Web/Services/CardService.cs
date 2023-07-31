using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;
using CardManager.Web.Utility;

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
        var result = await _httpClient
            .PostAsJsonAsync(BackendConn.CardManagerBackendUrl + "api/card/create-card", newCardData);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateDatabaseAsync(IFormFile fileData)
    {
        var result = await _httpClient
            .PostAsJsonAsync(BackendConn.CardManagerBackendUrl + "api/card/seed-database", fileData);
        return result.IsSuccessStatusCode;
    }

    public async Task<byte[]?> GenerateCsvReport(string cardType)
    {
        var result = await _httpClient
                .GetAsync(BackendConn.CardManagerBackendUrl + "api/card/generate-report?type=" + cardType);
        
        if (!result.IsSuccessStatusCode)
            return null;
        
        var resultContent = await result.Content.ReadAsByteArrayAsync();
        return resultContent;
    }

    public async Task<CardDto> GetCardAsync(string owner)
    {
        var result = await _httpClient
            .GetFromJsonAsync<CardDto>(
                BackendConn.CardManagerBackendUrl + "api/card/owner-card-cpf?owner=" + owner);
        return result;
    }

    public async Task<bool> DeleteCardAsync(string cardSerial)
    {
        var result = await _httpClient
            .DeleteAsync(BackendConn.CardManagerBackendUrl + "api/card/delete-card-by?cardSerial=" + cardSerial);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAllCardsAsync()
    {
        var result = await _httpClient
            .DeleteAsync(BackendConn.CardManagerBackendUrl + "api/card/delete-all-cards");
        return result.IsSuccessStatusCode;
    }
}