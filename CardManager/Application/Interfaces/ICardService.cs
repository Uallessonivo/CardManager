﻿using CardManager.Application.DTO;
using CardManager.Application.Responses;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using System.Text;

namespace CardManager.Application.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllAsync();
        Task<List<Card>> GetAllPaginatedAsync(int pageNumber, int pageSize);
        Task<Card> GetByIdAsync(Guid id);
        Task<Card> GetByOwnerCpfAsync(string cpf);
        Task<List<Card>> GetAllByType(CardType type);
        Task<StringBuilder> GenerateReport(string type);
        Task<Card> CreateCardAsync(CardDto card);
        Task<bool> CardExists(CardDto card);
        Task<Card> UpdateCardAsync(Guid id, UpdateCardDto card);
        Task DeleteCardAsync(Guid id);
        Task DeleteCardAsync(string cardSerial);
        Task DeleteAllCardsAsync();
        Task<SeedDatabaseResponse> SeedDatabaseTask(IFormFile file);
    }
}
