namespace CardManager.Application.Responses
{
    public class FailedCardResponse
    {
        public string CardOwnerName { get; set; } = string.Empty;
        public string CardSerialNumber { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public string CardOwnerCpf { get; set; } = string.Empty;
        public object? ErrorMessage { get; set; }
    }
}
