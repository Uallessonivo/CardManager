using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Infrastructure.Context;
using CardManager.Infrastructure.Interfaces;

namespace CardManager.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly DbContext _dbContext;

        public CardRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCard(Card card)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCard(Card card)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Card>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Card>> GetCardsByType(CardTypes type)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCard(Card card)
        {
            throw new NotImplementedException();
        }
    }
}
