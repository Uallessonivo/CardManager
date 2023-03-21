using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Presentation.DTO;

namespace CardManager.Application.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllAsync();
        Task<Card> GetByIdAsync(Guid id);
        Task<Card> GetByOwnerCpfAsync(string cpf);
        Task<List<Card>> GetAllByType(CardType type);
        Task CreateCardAsync(CardDTO card);
        Task UpdateCardAsync(Guid id, CardDTO card);
        Task DeleteCardAsync(Guid id);

    }
}
