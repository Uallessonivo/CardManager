using System;

namespace CardManager.Domain.Errors
{
    public static class Errors
    {
        public static string CardNotFound(Guid id)
        {
            return $"Não localizamos o cartão com ID: {id}";
        }

        public static string CardNotFound(string cpf)
        {
            return $"Não localizamos o cartão com CPF: {cpf}";
        }

        public static string CardAlreadyExists()
        {
            return $"O cartão já existe na base!";
        }

        public static string InvalidCardType(string type)
        {
            return $"Não temos esse tipo de cartão em nossa base: {type}";
        }
    }
}
