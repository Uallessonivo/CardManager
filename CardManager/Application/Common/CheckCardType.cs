using CardManager.Domain.Enums;
using CardManager.Domain.Errors;

namespace CardManager.Application.Common
{
    public static class CheckCardType
    {
        public static CardType IsCardValid(string cardTypeString)
        {
            if (Enum.TryParse(cardTypeString, out CardType cardType))
            {
                return cardType;
            }

            throw new Exception(Errors.InvalidCardType(cardTypeString));
        }
    }
}
