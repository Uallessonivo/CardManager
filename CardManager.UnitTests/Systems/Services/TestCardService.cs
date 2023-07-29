using CardManager.Application.DTO;
using CardManager.Domain.Entities;
using CardManager.UnitTests.Fixtures;
using CardManager.UnitTests.Helpers.Mappers;
using FluentValidation.Results;
using Moq;

namespace CardManager.UnitTests.Systems.Services
{
    public class TestCardService : IClassFixture<TestCardService>
    {
        private static readonly IEnumerable<Card> FakeCards = TestCardFactory.FakeCards();
        private static readonly IEnumerable<Card> FakeInvalidCards = TestCardFactory.FakeCardsWithErrors();
        private readonly CardServiceFixture _fixture;

        public TestCardService()
        {
            _fixture = new CardServiceFixture();
        }

        [Fact]
        public async Task Should_Thrown_Exception_When_Card_Exists()
        {
            // Arrange
            var card = FakeCards.First();
            var dto = CardMapper.MapToDto(card);
            
            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardBySerialNumber(It.IsAny<string>()))
                .ReturnsAsync(card);
            
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.CardExists(dto));
        }
        
        [Fact]
        public async Task Should_Return_False_When_Card_Does_Not_Exists()
        {
            // Arrange
            var card = FakeCards.First();
            var dto = CardMapper.MapToDto(card);

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardBySerialNumber(It.IsAny<string>()))
                .ReturnsAsync((Card)null);

            // Act
            var result = await _fixture.CardService.CardExists(dto);
            
            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task Should_Return_Validation_Error_When_Card_Data_Is_Invalid()
        {
            // Arrange
            var card = FakeInvalidCards.First();
            var dto = CardMapper.MapToDto(card);

            var validationErrors = new List<ValidationFailure>();

            if (string.IsNullOrEmpty(card.CardOwnerName))
            {
                validationErrors.Add(new ValidationFailure("CardOwnerName", "Invalid name"));
            }

            if (string.IsNullOrEmpty(card.CardOwnerCpf))
            {
                validationErrors.Add(new ValidationFailure("CardOwnerCpf", "Invalid CPF"));
            }

            if (string.IsNullOrEmpty(card.CardSerial))
            {
                validationErrors.Add(new ValidationFailure("CardSerial", "Invalid serial"));
            }

            var validationResult = new ValidationResult(validationErrors);

            _fixture.CardDtoValidator.Setup(validator => validator.ValidateAsync(It.IsAny<CardDto>(), default))
                .ReturnsAsync(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.CreateCardAsync(dto));

            foreach (var validationError in validationErrors)
            {
                Assert.Contains(validationError.ErrorMessage, exception.Message);
            }
        }

        [Fact]
        public async Task Should_Save_Card_When_Data_Is_Valid()
        {
            // Arrange
            var card = FakeCards.First();
            var dto = CardMapper.MapToDto(card);

            _fixture.CardDtoValidator.Setup(validator => validator.ValidateAsync(It.IsAny<CardDto>(), default))
                .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _fixture.CardService.CreateCardAsync(dto);

            // Assert
            var okResult = Assert.IsType<Card>(result);
            var createdCard = Assert.IsAssignableFrom<Card>(okResult);

            Assert.Equal(card.CardOwnerCpf, createdCard.CardOwnerCpf);
            Assert.Equal(card.CardSerial, createdCard.CardSerial);
            Assert.Equal(card.CardType, createdCard.CardType);
        }

        [Fact]
        public async Task Should_Return_All_Cards_When_Success()
        {
            // Arrange
            var fakeCards = FakeCards.ToList();

            _fixture.CardRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(fakeCards);

            // Act
            var result = await _fixture.CardService.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<List<Card>>(result);
            var cards = Assert.IsAssignableFrom<List<Card>>(okResult);
            Assert.Equal(fakeCards.Count, cards.Count);
        }

        [Fact]
        public async Task Should_Return_Card_When_Success()
        {
            // Arrange
            var ownerCpf = "000000000000";

            var foundCard = FakeCards.FirstOrDefault(c => c.CardOwnerCpf == ownerCpf);

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardById(It.IsAny<Guid>()))
                .ReturnsAsync(foundCard);

            // Act
            var result = await _fixture.CardService.GetByIdAsync(foundCard.CardId);

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

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardByCpfOwner(It.IsAny<string>()))
                .ReturnsAsync((Card)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.GetByOwnerCpfAsync(ownerCpf));
        }
    }
}