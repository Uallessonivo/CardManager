using CardManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardManager.Infrastructure.Persistence
{
    public class CardManagerDbContextInMemory : DbContext
    {
        public CardManagerDbContextInMemory(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
    }
}
