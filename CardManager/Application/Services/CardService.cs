using CardManager.Application.Interfaces;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Domain.Errors;
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
            return _cardRepository.CreateCard(card);
        }

        public async Task DeleteCardAsync(Guid id)
        {
            var card = await GetByIdAsync(id);
            await _cardRepository.DeleteCard(card);
        }

        public async Task<List<Card>> GetAllAsync()
        {
            return await _cardRepository.GetAll();
        }

        public async Task<List<Card>> GetAllByType(CardType type)
        {
            var typeString = type.ToString();
            if (!Enum.TryParse<CardType>(typeString, out var cardType))
            {
                throw new Exception(Errors.InvalidCardType(typeString));
            }
            return await _cardRepository.GetCardsByType(cardType);
        }

        public async Task<Card> GetByIdAsync(Guid id)
        {
            var card = await _cardRepository.GetCardById(id);

            if (card == null)
            {
                throw new Exception(Errors.CardNotFound(id));
            }

            return card;
        }

        public async Task UpdateCardAsync(Guid id, Card card)
        {
            var result = await GetByIdAsync(id);

            if (result == null)
            {
                throw new Exception(Errors.CardNotFound(id));
            }

            await _cardRepository.UpdateCard(card);
        }
    }
}
