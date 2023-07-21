using CardManager.Application.DTO;
using CsvHelper.Configuration;

namespace CardManager.Application.Utils;

public sealed class CsvCardMapper : ClassMap<CardDto>
{
    public CsvCardMapper()
    {
        Map(c => c.CardSerial).Index(1);
        Map(c => c.CardOwnerCpf).Index(2);
        Map(c => c.CardOwnerName).Index(3);
        Map(c => c.CardType).Index(4);
    }
}