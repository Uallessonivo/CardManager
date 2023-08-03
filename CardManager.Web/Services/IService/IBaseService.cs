using CardManager.Web.Models.Dtos;

namespace CardManager.Web.Services.IService;

public interface IBaseService
{
    Task<ResponseDto>? SendAsync(RequestDto requestDto);
}