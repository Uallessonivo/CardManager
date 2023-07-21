using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Infrastructure.Persistence;
using CardManager.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardManager.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly CardManagerDbContext _dbContext;

        public CardRepository(CardManagerDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<Card?> GetCardByCpfOwner(string cpf)
        {
            return await _dbContext.Cards
                .FirstOrDefaultAsync(x => x.CardOwnerCpf == cpf);
        }

        public async Task<Card?> GetCardBySerialNumber(string serial)
        {
            return await _dbContext.Cards
                .FirstOrDefaultAsync(x => x.CardSerial == serial);
        }

        public async Task CreateCard(Card card)
        {
           await _dbContext.Cards.AddAsync(card);
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCard(Card card)
        {
            _dbContext.Cards.Remove(card);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteAllCards()
        {
            var cards = await _dbContext.Cards.ToListAsync();
            _dbContext.Cards.RemoveRange(cards);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateCard(Card card)
        {
            _dbContext.Cards.Update(card);
            await _dbContext.SaveChangesAsync();
        }
    }
}
