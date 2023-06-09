﻿using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CardManager.Application.Utils;
using CardManager.Application.Validators;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Domain.Errors;
using CardManager.Infrastructure.Interfaces;
using FluentValidation;

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

        public async Task CreateCardAsync(CardDto card)
        {
            var validationResult = await _cardValidator.ValidateAsync(card);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                throw new Exception(Errors.InvalidFields(string.Join(", ", errors)));
            }

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
            var card = await GetByIdAsync(id) ?? throw new Exception(Errors.CardNotFound(id));
            await _cardRepository.DeleteCard(card);
        }

        public async Task SeedDatabaseTask(IFormFile file)
        {
            var cards = await ProcessFile.Parse(file);

            foreach (var card in cards)
            {
                switch (card.CardType)
                {
                    case "MATRIZ":
                        card.CardType = CardType.DespesasMatriz.ToString();
                        break;
                    case "FILIAL":
                        card.CardType = CardType.DespesasFilial.ToString();
                        break;
                }

                await CreateCardAsync(card);
            }
        }

        public async Task<List<Card>> GetAllAsync()
        {
            var cards = await _cardRepository.GetAll();
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

        public async Task UpdateCardAsync(Guid id, UpdateCardDto card)
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
        }
    }
}