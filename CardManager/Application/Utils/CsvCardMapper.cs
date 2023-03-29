using CardManager.Application.DTO;
using CardManager.Domain.Enums;
using CsvHelper.Configuration;

namespace CardManager.Application.Utils;

public sealed class CsvCardMapper : ClassMap<CardDto>
{
    public CsvCardMapper()
    {
        Map(c => c.CardSerial).Name("N de Série Cartão");
        Map(c => c.CardOwnerCpf).Name("CPF");
        Map(c => c.CardOwnerName).Name("Colaborador");
        Map(c => c.CardType).Name("Centro de Custo");
    }
}