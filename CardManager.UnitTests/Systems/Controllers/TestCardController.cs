using System.Collections;
using CardManager.Application.DTO;
using CardManager.Domain.Entities;
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
        public async Task GetCards_Should_Thrown_Exception_When_Error_Occurs()
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
    }
}