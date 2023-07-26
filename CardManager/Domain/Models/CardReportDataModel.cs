namespace CardManager.Domain.Models
{
    public class CardReportDataModel
    {
        public string CardSerial { get; set; } = string.Empty;
        public string CardOwnerCpf { get; set; } = string.Empty;
        public string CardValue { get; set; } = string.Empty;
        public string CardOwnerName { get; set; } = string.Empty;
    }
}
