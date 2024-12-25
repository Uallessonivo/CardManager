using CardManager.Application.DTO;
using CardManager.Application.Validators;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
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

        [Fact]
        public async Task Should_Return_Cards_By_Type_When_Type_Is_Valid()
        {
            // Arrange
            var cardType = CardType.Incentivo;
            var validCardType = CheckCardType.IsCardValid(cardType.ToString());
            var fakeCards = FakeCards.Where(c => c.CardType == validCardType).ToList();

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardsByType(validCardType))
                .ReturnsAsync(fakeCards);

            // Act
            var result = await _fixture.CardService.GetAllByType(cardType);

            // Assert
            var okResult = Assert.IsType<List<Card>>(result);
            var cards = Assert.IsAssignableFrom<List<Card>>(okResult);
            Assert.Equal(fakeCards.Count, cards.Count);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Type_Is_Invalid()
        {
            // Arrange
            var invalidCardType = (CardType)999;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.GetAllByType(invalidCardType));
        }
        
        [Fact]
        public async Task Should_Return_Card_When_Cpf_Is_Valid()
        {
            var cpf = "000000000000";
            var foundCard = FakeCards.FirstOrDefault(c => c.CardOwnerCpf == cpf);

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardByCpfOwner(It.IsAny<string>()))
                .ReturnsAsync(foundCard);

            var result = await _fixture.CardService.GetByOwnerCpfAsync(cpf);

            var okResult = Assert.IsType<Card>(result);
            var card = Assert.IsAssignableFrom<Card>(okResult);

            Assert.Equal(foundCard, card);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Cpf_Is_Invalid()
        {
            var cpf = "invalid_cpf";

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardByCpfOwner(It.IsAny<string>()))
                .ReturnsAsync((Card)null);

            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.GetByOwnerCpfAsync(cpf));
        }
        
        [Fact]
        public async Task Should_Return_Paginated_Cards_When_PageNumber_And_PageSize_Are_Valid()
        {
            var pageNumber = 1;
            var pageSize = 10;
            var fakeCards = FakeCards.Take(pageSize).ToList();

            _fixture.CardRepositoryMock.Setup(repo => repo.GetAllPaginated(pageNumber, pageSize))
                .ReturnsAsync(fakeCards);

            var result = await _fixture.CardService.GetAllPaginatedAsync(pageNumber, pageSize);

            var okResult = Assert.IsType<List<Card>>(result);
            var cards = Assert.IsAssignableFrom<List<Card>>(okResult);
            Assert.Equal(fakeCards.Count, cards.Count);
        }

        [Fact]
        public async Task Should_Return_Empty_List_When_No_Cards_Are_Found()
        {
            var pageNumber = 1;
            var pageSize = 10;

            _fixture.CardRepositoryMock.Setup(repo => repo.GetAllPaginated(pageNumber, pageSize))
                .ReturnsAsync(new List<Card>());

            var result = await _fixture.CardService.GetAllPaginatedAsync(pageNumber, pageSize);

            var okResult = Assert.IsType<List<Card>>(result);
            Assert.Empty(okResult);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_PageNumber_Is_Invalid()
        {
            var pageNumber = -1;
            var pageSize = 10;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _fixture.CardService.GetAllPaginatedAsync(pageNumber, pageSize));
        }

        [Fact]
        public async Task Should_Throw_Exception_When_PageSize_Is_Invalid()
        {
            var pageNumber = 1;
            var pageSize = -10;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _fixture.CardService.GetAllPaginatedAsync(pageNumber, pageSize));
        }
        
        [Fact]
        public async Task Should_Return_Card_When_Id_Is_Valid()
        {
            var cardId = Guid.NewGuid();
            var foundCard = new Card
            {
                CardId = cardId,
                CardSerial = "123456789",
                CardOwnerName = "John Doe",
                CardOwnerCpf = "12345678901",
                CardType = CardType.Incentivo
            };

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardById(cardId))
                .ReturnsAsync(foundCard);

            var result = await _fixture.CardService.GetByIdAsync(cardId);

            var okResult = Assert.IsType<Card>(result);
            var card = Assert.IsAssignableFrom<Card>(okResult);

            Assert.Equal(foundCard, card);
        }
        
        [Fact]
        public async Task Should_Throw_Exception_When_Id_Is_Invalid()
        {
            var cardId = Guid.NewGuid();

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardById(It.IsAny<Guid>()))
                .ReturnsAsync((Card)null);

            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.GetByIdAsync(cardId));
        }
        
        [Fact]
        public async Task Should_Update_Card_When_Id_And_Card_Are_Valid()
        {
            var cardId = Guid.NewGuid();
            var updateCardDto = new UpdateCardDto
            {
                CardOwnerName = "Jane Doe",
                CardOwnerCpf = "98765432100"
            };
            var existingCard = new Card
            {
                CardId = cardId,
                CardSerial = "123456789",
                CardOwnerName = "John Doe",
                CardOwnerCpf = "12345678901",
                CardType = CardType.Incentivo
            };
            var updatedCard = new Card
            {
                CardId = cardId,
                CardSerial = "123456789",
                CardOwnerName = updateCardDto.CardOwnerName,
                CardOwnerCpf = updateCardDto.CardOwnerCpf,
                CardType = CardType.Incentivo
            };

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardById(cardId))
                .ReturnsAsync(existingCard);
            _fixture.CardRepositoryMock.Setup(repo => repo.UpdateCard(It.IsAny<Card>()))
                .Returns(Task.CompletedTask);
            _fixture.UpdateCardDtoValidator.Setup(validator => validator.ValidateAsync(updateCardDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var result = await _fixture.CardService.UpdateCardAsync(cardId, updateCardDto);

            var okResult = Assert.IsType<Card>(result);
            var card = Assert.IsAssignableFrom<Card>(okResult);

            Assert.Equal(updatedCard.CardOwnerName, card.CardOwnerName);
            Assert.Equal(updatedCard.CardOwnerCpf, card.CardOwnerCpf);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Id_Is_Invalid_For_Update()
        {
            var cardId = Guid.NewGuid();
            var updateCardDto = new UpdateCardDto
            {
                CardOwnerName = "Jane Doe",
                CardOwnerCpf = "98765432100"
            };

            _fixture.CardRepositoryMock.Setup(repo => repo.GetCardById(cardId))
                .ReturnsAsync((Card)null);

            _fixture.UpdateCardDtoValidator.Setup(validator => validator.ValidateAsync(updateCardDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.UpdateCardAsync(cardId, updateCardDto));
        }

        [Fact]
        public async Task Should_Throw_Exception_When_UpdateCardDto_Is_Invalid()
        {
            var cardId = Guid.NewGuid();
            var updateCardDto = new UpdateCardDto
            {
                CardOwnerName = "Jane Doe",
                CardOwnerCpf = "98765432100"
            };
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("CardOwnerName", "Card owner name is required.")
            });

            _fixture.UpdateCardDtoValidator.Setup(validator => validator.ValidateAsync(updateCardDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            await Assert.ThrowsAsync<Exception>(async () => await _fixture.CardService.UpdateCardAsync(cardId, updateCardDto));
        }
    }
}