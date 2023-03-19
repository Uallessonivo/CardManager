using CardManager.Application.Interfaces;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Domain.Errors;
using CardManager.Infrastructure.Interfaces;
using CardManager.Presentation.DTO;

namespace CardManager.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public Task CreateCardAsync(CardDTO card)
        {
            var newCard = new Card
            {
                CardId = Guid.NewGuid(),
                CardSerial = card.CardSerial,
                CardOwnerName = card.CardOwnerName,
                CardOwnerCpf = card.CardOwnerCpf,
                CardType = card.CardType
            };

            return _cardRepository.CreateCard(newCard);
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

        public async Task UpdateCardAsync(Guid id, CardDTO card)
        {
            var result = await GetByIdAsync(id);

            if (result == null)
            {
                throw new Exception(Errors.CardNotFound(id));
            }

            var updatedCard = new Card
            {
                CardId = id,
                CardSerial = card.CardSerial,
                CardOwnerName = card.CardOwnerName,
                CardOwnerCpf = card.CardOwnerCpf,
                CardType = card.CardType
            };

            await _cardRepository.UpdateCard(updatedCard);
        }
    }
}
