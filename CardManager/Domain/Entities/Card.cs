using CardManager.Domain.Enums;

namespace CardManager.Domain.Entities
{
    public class Card
    {
        public Guid CardId { get; set; }
        public string CardSerial { get; set; }
        public string CardOwnerName { get; set; }
        public string CardOwnerCpf { get; set; }
        public CardTypes CardType { get; set; }
    }
}
