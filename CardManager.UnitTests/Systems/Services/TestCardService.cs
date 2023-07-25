using CardManager.Application.DTO;
using CardManager.Application.Services;
using CardManager.Domain.Entities;
using CardManager.Infrastructure.Interfaces;
using CardManager.UnitTests.Fixtures;
using FluentValidation;
using Moq;

namespace CardManager.UnitTests.Systems.Services
{
    public class TestCardService
    {
        private static readonly IEnumerable<Card> FakeCards = TestCardFactory.FakeCards();

        [Fact]
        public async Task Should_Return_All_Cards_When_Successfull()
        {
            // Arrange
            var fakeCards = FakeCards.ToList();

            var mockCardRepository = new Mock<ICardRepository>();

            mockCardRepository.Setup(repo => repo.GetAll()).ReturnsAsync(fakeCards);

            var mockCardValidator = new Mock<IValidator<CardDto>>();
            var mockUpdatedCardValidator = new Mock<IValidator<UpdateCardDto>>();

            var cardService = new CardService(mockCardRepository.Object, mockCardValidator.Object, mockUpdatedCardValidator.Object);

            // Act
            var result = await cardService.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<List<Card>>(result);
            var cards = Assert.IsAssignableFrom<List<Card>>(okResult);
            Assert.Equal(fakeCards.Count, cards.Count);
        }

        [Fact]
        public async Task Should_Return_Card_When_Successfull()
        {
            // Arrange
            var ownerCpf = "000000000000";

            var foundCard = FakeCards.FirstOrDefault(c => c.CardOwnerCpf == ownerCpf);

            var mockCardRepository = new Mock<ICardRepository>();

            mockCardRepository.Setup(repo => repo.GetCardById(It.IsAny<Guid>()))
                .ReturnsAsync(foundCard);

            var mockCardValidator = new Mock<IValidator<CardDto>>();
            var mockUpdatedCardValidator = new Mock<IValidator<UpdateCardDto>>();

            var cardService = new CardService(mockCardRepository.Object, mockCardValidator.Object, mockUpdatedCardValidator.Object);

            // Act
            var result = await cardService.GetByIdAsync(foundCard.CardId);

            // Assert
            var okResult = Assert.IsType<Card>(result);
            var card = Assert.IsAssignableFrom<Card>(okResult);

            Assert.Equal(foundCard, card);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Not_Found()
        {
            // Arrange
            var ownerCpf = "11111111111";

            var mockCardRepository = new Mock<ICardRepository>();
            mockCardRepository.Setup(repo => repo.GetCardByCpfOwner(It.IsAny<string>()))
                .ReturnsAsync((Card)null);

            var mockCardValidator = new Mock<IValidator<CardDto>>();
            var mockUpdatedCardValidator = new Mock<IValidator<UpdateCardDto>>();

            var cardService = new CardService(mockCardRepository.Object, mockCardValidator.Object, mockUpdatedCardValidator.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await cardService.GetByOwnerCpfAsync(ownerCpf));
        }
    }
}
