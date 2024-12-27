using CardManager.Application.DTO;

namespace CardManager.Application.Interfaces;

public interface IProcessFile
{
    List<CardDto> Parse(IFormFile file);
}