using CardManager.Application.Interfaces;
using CardManager.Domain.Entities;
using CardManager.Infrastructure.Interfaces;

namespace CardManager.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Task CreateCardAsync(Card card)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Card>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCardAsync(Guid id, Card card)
        {
            throw new NotImplementedException();
        }
    }
}
