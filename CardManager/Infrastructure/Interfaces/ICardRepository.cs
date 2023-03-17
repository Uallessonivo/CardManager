using CardManager.Domain.Entities;
using CardManager.Domain.Enums;

namespace CardManager.Infrastructure.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAll();
        Task<List<Card>> GetCardsByType(CardTypes type);
        Task CreateCard(Card card);
        Task UpdateCard(Card card);
        Task DeleteCard(Card card);
    }
}
