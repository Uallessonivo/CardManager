using System;

namespace CardManager.Domain.Errors
{
    public static class Errors
    {
        public static string CardNotFound(Guid id)
        {
            return $"Não localizamos o cartão com ID: {id}";
        }

        public static string InvalidCardType(string type)
        {
            return $"Não temos esse tipo de cartão em nossa base: {type}";
        }
    }
}
