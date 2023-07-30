using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CardManager.Application.Responses;
using CardManager.Application.Utils;
using CardManager.Application.Validators;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Domain.Errors;
using CardManager.Domain.Models;
using CardManager.Infrastructure.Interfaces;
using FluentValidation;
using System.Text;

namespace CardManager.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IValidator<CardDto> _cardValidator;
        private readonly IValidator<UpdateCardDto> _updatedCardValidator;

        public CardService(ICardRepository cardRepository, IValidator<CardDto> cardValidator,
            IValidator<UpdateCardDto> updatedCardValidator)
        {
            _cardRepository = cardRepository;
            _cardValidator = cardValidator;
            _updatedCardValidator = updatedCardValidator;
        }

        public async Task<Card> CreateCardAsync(CardDto card)
        {
            var validationResult = await _cardValidator.ValidateAsync(card);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                throw new Exception(Errors.InvalidFields(string.Join(", ", errors)));
            }

            await CardExists(card);

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

            return newCard;
        }

        public async Task<bool> CardExists(CardDto card)
        {
            var cardExists = await _cardRepository.GetCardBySerialNumber(card.CardSerial);

            if (cardExists != null)
            {
                throw new Exception(Errors.CardAlreadyExists());
            }

            return false;
        }

        public async Task<SeedDatabaseResponse> SeedDatabaseTask(IFormFile file)
        {
            var cards = ProcessFile.Parse(file);
            var failedCards = new List<FailedCardResponse>();

            foreach (var card in cards)
            {
                if (card.CardStatus == "Cancelado")
                {
                    continue;
                }

                card.CardType = card.CardType.Split(" ").First();

                try
                {
                    await CreateCardAsync(card);
                }
                catch (Exception ex)
                {
                    var cardError = new FailedCardResponse
                    {
                        ErrorMessage = ex.Message,
                        CardOwnerCpf = card.CardOwnerCpf ?? "N/A",
                        CardOwnerName = card.CardOwnerName ?? "N/A",
                        CardSerialNumber = card.CardSerial ?? "N/A",
                        CardType = card.CardType ?? "N/A"
                    };

                    failedCards.Add(cardError);
                }
            }

            if (failedCards.Count > 0)
            {
                return new SeedDatabaseResponse
                {
                    FailedCards = failedCards,
                    Message = "Os dados desses cartões não foram salvos no sistema."
                };
            }

            return new SeedDatabaseResponse
            {
                Message = "Todos os cartões foram salvos com êxito no banco de dados."
            };
        }

        public async Task<StringBuilder> GenerateReport(string type)
        {
            var cardType = CheckCardType.IsCardValid(type);

            var cards = await _cardRepository.GetCardsByType(cardType);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Numero de Serie; CPF; Valor da Carga; Observacao");

            foreach (var card in cards)
            {
                var row = new CardReportDataModel
                {
                    CardSerial = card.CardSerial.PadLeft(15, '0'),
                    CardOwnerCpf = card.CardOwnerCpf.Replace(".", "").Replace("-", "").PadLeft(11, '0'),
                    CardValue = "",
                    CardOwnerName = card.CardOwnerName.Length > 35 ? card.CardOwnerName.Substring(0, 35) : card.CardOwnerName
                };

                stringBuilder.AppendLine($"{row.CardSerial}; {row.CardOwnerCpf}; {row.CardValue}; {row.CardOwnerName}");
            }

            return stringBuilder;
        }

        public async Task<List<Card>> GetAllAsync()
        {
            var cards = await _cardRepository.GetAll();
            return cards;
        }

        public async Task<List<Card>> GetAllPaginatedAsync(int pageNumber = 1, int pageSize = 10)
        {
            var cards = await _cardRepository.GetAllPaginated(pageNumber, pageSize);
            return cards;
        }

        public async Task<Card> GetByOwnerCpfAsync(string cpf)
        {
            var card = await _cardRepository.GetCardByCpfOwner(cpf);
            return card ?? throw new Exception(Errors.CardNotFound(cpf));
        }

        public async Task<List<Card>> GetAllByType(CardType type)
        {
            var cardType = CheckCardType.IsCardValid(type.ToString());
            return await _cardRepository.GetCardsByType(cardType);
        }

        public async Task<Card> GetByIdAsync(Guid id)
        {
            var card = await _cardRepository.GetCardById(id) ?? throw new Exception(Errors.CardNotFound(id));
            return card;
        }

        public async Task<Card> UpdateCardAsync(Guid id, UpdateCardDto card)
        {
            var validationResult = await _updatedCardValidator.ValidateAsync(card);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                throw new Exception(Errors.InvalidFields(string.Join(", ", errors)));
            }

            var result = await GetByIdAsync(id) ?? throw new Exception(Errors.CardNotFound(id));

            var updatedCard = new Card
            {
                CardId = result.CardId,
                CardType = result.CardType,
                CardOwnerName = card.CardOwnerName,
                CardOwnerCpf = card.CardOwnerCpf,
                CardSerial = result.CardSerial
            };

            await _cardRepository.UpdateCard(updatedCard);

            return updatedCard;
        }

        public async Task DeleteCardAsync(Guid id)
        {
            var card = await GetByIdAsync(id) ?? throw new Exception(Errors.CardNotFound(id));
            await _cardRepository.DeleteCard(card);
        }
        
        public async Task DeleteCardAsync(string cardSerial)
        {
            var card = await _cardRepository.GetCardBySerialNumber(cardSerial) ?? throw new Exception(Errors.CardNotFound(cardSerial));
            await _cardRepository.DeleteCard(card);
        }

        public async Task DeleteAllCardsAsync()
        {
            await _cardRepository.DeleteAllCards();
        }
    }
}