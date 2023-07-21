namespace CardManager.Application.DTO;

public class SeedDatabaseResponseDto
{
    public List<CardDto> FailedCards { get; set; } = new List<CardDto>();
    public string Message { get; set; } = string.Empty;
}