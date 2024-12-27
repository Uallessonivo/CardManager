using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CardManager.Presentation.Controllers;
using Moq;

namespace CardManager.UnitTests.Fixtures;

public class CardControllerFixture
{
    public Mock<ICardService> CardServiceMock { get; private set; }
    public Mock<ResponseDto> ResponseDtoMock { get; private set; }
    public CardController CardController { get; private set; }

    public CardControllerFixture()
    {
        CardServiceMock = new Mock<ICardService>();
        ResponseDtoMock = new Mock<ResponseDto>();
        CardController = new CardController(CardServiceMock.Object);
    }
}