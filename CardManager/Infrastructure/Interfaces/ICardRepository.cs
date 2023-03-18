﻿using CardManager.Domain.Entities;
using CardManager.Domain.Enums;

namespace CardManager.Infrastructure.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAll();
        Task<List<Card>> GetCardsByType(CardType type);
        Task<Card?> GetCardById(Guid id);
        Task CreateCard(Card card);
        Task UpdateCard(Card card);
        Task DeleteCard(Card card);
    }
}
