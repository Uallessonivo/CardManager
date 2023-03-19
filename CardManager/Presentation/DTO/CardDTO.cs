using CardManager.Domain.Enums;

namespace CardManager.Presentation.DTO
{
    public class CardDTO
    {
        public string CardSerial { get; set; }
        public string CardOwnerName { get; set; }
        public string CardOwnerCpf { get; set; }
        public CardType CardType { get; set; }
    }
}
