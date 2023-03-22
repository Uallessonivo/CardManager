using CardManager.Application.Common;
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

        public async Task CreateCardAsync(CardDto card)
        {
            var cardExists = await _cardRepository.GetCardByCpfOwner(card.CardOwnerCpf);

            if (cardExists != null)
            {
                throw new Exception(Errors.CardAlreadyExists());
            }

            var cardType = CheckCardType.IsCardValid(card.CardType);

            var newCard = new Card
            {
                CardId = Guid.NewGuid(),
                CardSerial = card.CardSerial,
                CardOwnerName = card.CardOwnerName,
                CardOwnerCpf = card.CardOwnerCpf,
                CardType = cardType
            };

            await _cardRepository.CreateCard(newCard);
        }

        public async Task DeleteCardAsync(Guid id)
        {
            var card = await GetByIdAsync(id);

            if (card == null)
            {
                throw new Exception(Errors.CardNotFound(id));
            }

            await _cardRepository.DeleteCard(card);
        }

        public async Task<List<Card>> GetAllAsync()
        {
            var cards = await _cardRepository.GetAll();
            return cards;
        }

        public async Task<Card> GetByOwnerCpfAsync(string cpf)
        {
            var card = await _cardRepository.GetCardByCpfOwner(cpf);

            if (card == null)
            {
                throw new Exception(Errors.CardNotFound(cpf));
            }

            return card;
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

        public async Task UpdateCardAsync(Guid id, CardDto card)
        {
            var result = await GetByIdAsync(id);

            if (result == null)
            {
                throw new Exception(Errors.CardNotFound(id));
            }

            var updatedCard = new Card
            {
                CardId = result.CardId,
                CardType = result.CardType,
                CardOwnerName = card.CardOwnerName,
                CardOwnerCpf = card.CardOwnerCpf,
                CardSerial = result.CardSerial
            };

            await _cardRepository.UpdateCard(updatedCard);
        }
    }
}
