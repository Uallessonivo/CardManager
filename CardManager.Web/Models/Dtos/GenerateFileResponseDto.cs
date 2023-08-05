namespace CardManager.Web.Models.Dtos
{
    public class GenerateFileResponseDto
    {
        public byte[]? Content { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
