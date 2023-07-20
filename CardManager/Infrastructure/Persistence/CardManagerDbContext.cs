using CardManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardManager.Infrastructure.Persistence
{
    public class CardManagerDbContext : DbContext
    {
        public CardManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
    }
}
