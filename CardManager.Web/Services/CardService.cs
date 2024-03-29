﻿using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;
using CardManager.Web.Utility;

namespace CardManager.Web.Services;

public class CardService : ICardService
{
    private readonly IBaseService _baseService;

    public CardService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateNewCardAsync(CardDto newCardData)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Data = newCardData,
            Url = BackendConn.CardManagerBackendUrl + "api/Card/create-card"
        })!;
    }

    public async Task<ResponseDto> UpdateDatabaseAsync(IFormFile fileData)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Data = fileData,
            Url = BackendConn.CardManagerBackendUrl + "api/Card/seed-database"
        })!;
    }

    public async Task<GenerateFileResponseDto> GenerateCsvReport(string cardType)
    {
        using (HttpClient client = new HttpClient())
        {
            string backendUrl = BackendConn.CardManagerBackendUrl + "api/Card/generate-report?type=" + cardType;

            GenerateFileResponseDto response = new GenerateFileResponseDto();

            var csvData = await client.GetAsync(backendUrl);

            if (csvData.IsSuccessStatusCode)
            {
                var contentBytes = await csvData.Content.ReadAsByteArrayAsync();

                response.Content = contentBytes;
                response.ContentType = "text/csv";
                response.FileName = $"{DateTime.Now:yyyyMMdd}-{cardType}.csv";
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = "Erro ao obter os dados do servidor.";
            }

            return response;
        }
    }

    public async Task<ResponseDto> GetCardAsync(string owner)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = BackendConn.CardManagerBackendUrl + "api/Card/filter-card-cpf?owner=" + owner
        })!;
    }

    public async Task<ResponseDto> GetAllCardsAsync()
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = BackendConn.CardManagerBackendUrl + "api/Card/list-cards"
        })!;
    }

    public async Task<ResponseDto> DeleteCardAsync(string cardSerial)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = BackendConn.CardManagerBackendUrl + "api/Card/delete-card?cardSerial=" + cardSerial
        })!;
    }

    public async Task<ResponseDto> DeleteAllCardsAsync()
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = BackendConn.CardManagerBackendUrl + "api/Card/delete-cards/all"
        })!;
    }
}