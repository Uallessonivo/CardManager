using CardManager.Application.DTO;
using CardManager.Application.Validators;
using CardManager.Domain.Entities;

namespace CardManager.UnitTests.Helpers.Mappers
{
    public static class CardMapper
    {
        public static CardDto MapToDto(Card card)
        {
            return new CardDto
            {
                CardOwnerCpf = card.CardOwnerCpf,
                CardOwnerName = card.CardOwnerName,
                CardSerial = card.CardSerial,
                CardStatus = "Ativo",
                CardType = card.CardType.ToString(),
            };
        }

        public static Card MapToEntity(CardDto card)
        {
            return new Card
            {
                CardId = Guid.NewGuid(),
                CardOwnerCpf = card.CardOwnerCpf,
                CardOwnerName = card.CardOwnerName,
                CardSerial = card.CardSerial,
                CardType = CheckCardType.IsCardValid(card.CardType),
            };
        }
    }
}
