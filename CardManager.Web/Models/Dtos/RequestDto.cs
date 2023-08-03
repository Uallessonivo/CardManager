using CardManager.Web.Utility;

namespace CardManager.Web.Models.Dtos;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string? Url { get; set; }
    public object? Data { get; set; }
}