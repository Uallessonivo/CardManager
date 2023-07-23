using CardManager.Application.DTO;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;

namespace CardManager.Application.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllAsync();
        Task<List<Card>> GetAllPaginatedAsync(int pageNumber, int pageSize);
        Task<Card> GetByIdAsync(Guid id);
        Task<Card> GetByOwnerCpfAsync(string cpf);
        Task<List<Card>> GetAllByType(CardType type);
        Task CreateCardAsync(CardDto card);
        Task CardExists(CardDto card);
        Task UpdateCardAsync(Guid id, UpdateCardDto card);
        Task DeleteCardAsync(Guid id);
        Task DeleteAllCardsAsync();
        Task<SeedDatabaseResponseDto> SeedDatabaseTask(IFormFile file);
    }
}
