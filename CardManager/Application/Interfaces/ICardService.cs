using CardManager.Domain.Entities;
using CardManager.Domain.Enums;

namespace CardManager.Application.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllAsync();
        Task<Card> GetByIdAsync(Guid id);
        Task<List<Card>> GetAllByType(CardType type);
        Task CreateCardAsync(Card card);
        Task UpdateCardAsync(Guid id, Card card);
        Task DeleteCardAsync(Guid id);

    }
}
