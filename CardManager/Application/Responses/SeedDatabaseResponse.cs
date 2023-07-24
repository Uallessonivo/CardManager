namespace CardManager.Application.Responses;

public class SeedDatabaseResponse
{
    public List<FailedCardResponse> FailedCards { get; set; } = new List<FailedCardResponse>();
    public string Message { get; set; } = string.Empty;
}