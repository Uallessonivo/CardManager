using System.Net;
using System.Text;
using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;
using CardManager.Web.Utility;
using Newtonsoft.Json;

namespace CardManager.Web.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDto> SendAsync(RequestDto requestDto)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("CardManagerBackend");

            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");

            if (requestDto.Url != null) message.RequestUri = new Uri(requestDto.Url);

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8,
                    "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            message.Method = requestDto.ApiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };
            
            apiResponse = await client.SendAsync(message);
            
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDto() { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new ResponseDto() { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new ResponseDto() { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new ResponseDto() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto!;
            }
        }
        catch (Exception e)
        {
            var dto = new ResponseDto
            {
                Message = e.Message.ToString(),
                IsSuccess = false
            };
            return dto;
        }
    }
}