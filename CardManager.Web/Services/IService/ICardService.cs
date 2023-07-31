﻿using System.Text;
using CardManager.Web.Models.Dtos;

namespace CardManager.Web.Services.IService;

public interface ICardService
{
    Task<bool> CreateNewCardAsync(CardDto newCardData);
    Task<bool> UpdateDatabaseAsync(IFormFile fileData);
    Task<byte[]?> GenerateCsvReport(string cardType);
    Task<CardDto> GetCardAsync(string owner);
    Task<bool> DeleteCardAsync(string cardSerial);
    Task<bool> DeleteAllCardsAsync();
}