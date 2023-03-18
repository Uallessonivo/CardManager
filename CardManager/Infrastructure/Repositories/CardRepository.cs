using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Infrastructure.Persistence;
using CardManager.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardManager.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly CardManagerDbContextInMemory _dbContext;

        public CardRepository(CardManagerDbContextInMemory dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCard(Card card)
        {
           await _dbContext.Cards.AddAsync(card);
        }

        public async Task DeleteCard(Card card)
        {
            _dbContext.Cards.Remove(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Card>> GetAll()
        {
            return await _dbContext.Cards
                .ToListAsync();
        }

        public async Task<Card?> GetCardById(Guid id)
        {
            return await _dbContext.Cards
                .FirstOrDefaultAsync(x => x.CardId == id);
        }

        public async Task<List<Card>> GetCardsByType(CardType type)
        {
            return await _dbContext.Cards
                .Where(x => x.CardType == type)
                .ToListAsync();
        }

        public async Task UpdateCard(Card card)
        {
            _dbContext.Cards.Update(card);
            await _dbContext.SaveChangesAsync();
        }
    }
}
