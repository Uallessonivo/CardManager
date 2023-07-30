﻿using CardManager.Domain.Entities;
using CardManager.Domain.Enums;

namespace CardManager.UnitTests.Fixtures
{
    public static class TestCardFactory
    {
        public static IEnumerable<Card> FakeCards()
        {
            return new List<Card>
            {
                new Card {
                    CardId = Guid.NewGuid(),
                    CardOwnerCpf = "000000000000",
                    CardOwnerName = "THIS IS THE NAME",
                    CardSerial = "000000000000000",
                    CardType = CardType.Despesas
                },
                new Card {
                    CardId = Guid.NewGuid(),
                    CardOwnerCpf = "11111111111",
                    CardOwnerName = "THIS IS THE OTHER NAME",
                    CardSerial = "000000000000000",
                    CardType = CardType.Incentivo
                }
            };
        }

        public static IEnumerable<Card> FakeCardsWithErrors()
        {
            return new List<Card>
            {
                new Card {
                    CardId = Guid.NewGuid(),
                    CardOwnerCpf = "",
                    CardOwnerName = "",
                    CardSerial = "",
                    CardType = CardType.Despesas
                },
                new Card {
                    CardId = Guid.NewGuid(),
                    CardOwnerCpf = "000000000",
                    CardOwnerName = "THIS IS THE OTHER NAME",
                    CardSerial = "000000000000000",
                    CardType = CardType.Incentivo
                },
                new Card {
                    CardId = Guid.NewGuid(),
                    CardOwnerCpf = "000000000",
                    CardOwnerName = "THIS NAME HAS MORE THAN 35 CHARACTERS",
                    CardSerial = "000000000000000",
                    CardType = CardType.Incentivo
                }
            };
        }
    }
}
