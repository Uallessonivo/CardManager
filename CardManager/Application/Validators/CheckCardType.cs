using CardManager.Domain.Enums;
using CardManager.Domain.Errors;

namespace CardManager.Application.Validators
{
    public static class CheckCardType
    {
        public static CardType IsCardValid(string cardTypeString)
        {
            if (Enum.TryParse(cardTypeString, true, out CardType cardType))
            {
                return cardType;
            }

            throw new Exception(Errors.InvalidCardType(cardTypeString));
        }
    }
}
