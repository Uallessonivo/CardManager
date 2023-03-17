using CardManager.Domain.Entities;

namespace CardManager.Application.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllAsync();
        Task<Card> GetByIdAsync(Guid id);
        Task CreateCardAsync(Card card);
        Task UpdateCardAsync(Guid id, Card card);
        Task DeleteCardAsync(Guid id);

    }
}
