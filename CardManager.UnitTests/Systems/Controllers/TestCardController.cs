using CardManager.Application.DTO;
using CardManager.Domain.Entities;
using CardManager.Domain.Enums;
using CardManager.Domain.Errors;
using CardManager.UnitTests.Fixtures;
using Moq;

namespace CardManager.UnitTests.Systems.Controllers
{
    public class TestCardController : IClassFixture<TestCardController>
    {
        private readonly CardControllerFixture _fixture;
        private static readonly IEnumerable<CardDto> FakeCardsDto = TestCardFactory.FakeCardsDto();

        public TestCardController()
        {
            _fixture = new CardControllerFixture();
        }

        [Fact]
        public async Task GetCards_Should_Return_Cards()
        {
            // Arrange
            var cards = TestCardFactory.FakeCards().ToList();
            _fixture.CardServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(cards);
            
            // Act
            var result = await _fixture.CardController.GetCards();
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cards, result.Result);
        }
        
        [Fact]
        public async Task GetCards_Should_Return_Empty_List_When_No_Cards_Found()
        {
            // Arrange
            var cards = new List<Card>();
            _fixture.CardServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(cards);
            
            // Act
            var result = await _fixture.CardController.GetCards();
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(cards, result.Result);
        }
        
        [Fact]
        public async Task GetCards_Should_Throw_Exception_When_Error_Occurs()
        {
            // Arrange
            var exception = new Exception();
            _fixture.CardServiceMock.Setup(service => service.GetAllAsync()).ThrowsAsync(exception);
            
            // Act
            var result = await _fixture.CardController.GetCards();
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(exception.Message, result.Message);
        }
        
        [Fact]
        public async Task GetCardByType_Should_Return_Card()
        {
            // Arrange
            var cardType = CardType.Incentivo;
            _fixture.CardServiceMock.Setup(service => service.GetAllByType(cardType)).ReturnsAsync(It.IsAny<List<Card>>());
            
            // Act
            var result = await _fixture.CardController.GetCardByType(cardType);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(It.IsAny<Card>(), result.Result);
        }
        
        [Fact]
        public async Task GetCardByType_Should_Throw_Exception_When_Card_Is_Not_Found()
        {
            // Arrange
            var cardType = CardType.Incentivo;
            var exception = new Exception();
            _fixture.CardServiceMock.Setup(service => service.GetAllByType(cardType)).ThrowsAsync(exception);
            
            // Act
            var result = await _fixture.CardController.GetCardByType(cardType);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.Equal(exception.Message, result.Message);
        }
        
        [Fact]
        public async Task GetCardByType_Should_Show_Error_Message_When_Card_Is_Not_Found()
        {
            // Arrange
            var cardType = CardType.Incentivo;
            var cardId = Guid.NewGuid();
            _fixture.CardServiceMock.Setup(service => service.GetAllByType(cardType))
                .ThrowsAsync(new Exception(Errors.CardNotFound(cardId)));
            
            // Act
            var result = await _fixture.CardController.GetCardByType(cardType);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.Equal(Errors.CardNotFound(cardId), result.Message);
        }
        
        [Fact]
        public async Task GetById_Should_Return_Card()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            _fixture.CardServiceMock.Setup(service => service.GetByIdAsync(cardId)).ReturnsAsync(It.IsAny<Card>());
            
            // Act
            var result = await _fixture.CardController.GetByIdAsync(cardId);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(It.IsAny<Card>(), result.Result);
        }
        
        [Fact]
        public async Task GetById_Should_Throw_Exception_When_Card_Is_Not_Found()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var exception = new Exception();
            _fixture.CardServiceMock.Setup(service => service.GetByIdAsync(cardId)).ThrowsAsync(exception);
            
            // Act
            var result = await _fixture.CardController.GetByIdAsync(cardId);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.Equal(exception.Message, result.Message);
        }
        
        [Fact]
        public async Task GetById_Should_Show_Error_Message_When_Card_Is_Not_Found()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            _fixture.CardServiceMock.Setup(service => service.GetByIdAsync(cardId))
                .ThrowsAsync(new Exception(Errors.CardNotFound(cardId)));
            
            // Act
            var result = await _fixture.CardController.GetByIdAsync(cardId);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.Equal(Errors.CardNotFound(cardId), result.Message);
        }
        
        [Fact]
        public async Task GetCardByOwnerCpf_Should_Return_Card()
        {
            // Arrange
            var cards = TestCardFactory.FakeCards().ToList();
            _fixture.CardServiceMock.Setup(service => service.GetByOwnerCpfAsync(cards.First().CardOwnerCpf!))
                .ReturnsAsync(It.IsAny<Card>());
            
            // Act
            var result = await _fixture.CardController.GetCardByOwnerCpf(cards.First().CardOwnerCpf!);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(It.IsAny<Card>(), result.Result);
        }
        
        [Fact]
        public async Task GetCardByOwnerCpf_Should_Throw_Exception_When_Card_Is_Not_Found()
        {
            // Arrange
            var cards = TestCardFactory.FakeCards().ToList();
            var exception = new Exception();
            _fixture.CardServiceMock.Setup(service => service.GetByOwnerCpfAsync(cards.First().CardOwnerCpf!))
                .ThrowsAsync(exception);
            
            // Act
            var result = await _fixture.CardController.GetCardByOwnerCpf(cards.First().CardOwnerCpf!);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.Equal(exception.Message, result.Message);
        }
        
        [Fact]
        public async Task GetCardByOwnerCpf_Should_Show_Error_Message_When_Card_Is_Not_Found()
        {
            // Arrange
            var cards = TestCardFactory.FakeCards().ToList();
            var cardId = Guid.NewGuid();
            _fixture.CardServiceMock.Setup(service => service.GetByOwnerCpfAsync(cards.First().CardOwnerCpf!))
                .ThrowsAsync(new Exception(Errors.CardNotFound(cardId)));
            
            // Act
            var result = await _fixture.CardController.GetCardByOwnerCpf(cards.First().CardOwnerCpf!);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Result);
            Assert.Equal(Errors.CardNotFound(cardId), result.Message);
        }
    }
}