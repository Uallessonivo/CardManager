using CardManager.Application.DTO;
using CardManager.Application.Services;
using CardManager.Infrastructure.Interfaces;
using FluentValidation;
using Moq;

namespace CardManager.UnitTests.Fixtures
{
    public class CardServiceFixture
    {
        public Mock<ICardRepository> CardRepositoryMock { get; private set; }
        public Mock<IValidator<CardDto>> CardDtoValidator { get; private set; }
        public Mock<IValidator<UpdateCardDto>> UpdateCardDtoValidator { get; private set; }
        public CardService CardService { get; private set; }

        public CardServiceFixture()
        {
            CardRepositoryMock = new Mock<ICardRepository>();
            CardDtoValidator = new Mock<IValidator<CardDto>>();
            UpdateCardDtoValidator = new Mock<IValidator<UpdateCardDto>>();
            CardService = new CardService(CardRepositoryMock.Object, CardDtoValidator.Object, UpdateCardDtoValidator.Object);
        }
    }
}
