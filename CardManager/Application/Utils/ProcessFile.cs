using System.Globalization;
using CardManager.Presentation.DTO;
using CsvHelper;

namespace CardManager.Application.Utils
{
    public static class ProcessFile
    {
        public static async Task<List<CardDto>> Parse(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var cards = csvReader.GetRecords<CardDto>().ToList();
            return cards;
        }
    }
}
