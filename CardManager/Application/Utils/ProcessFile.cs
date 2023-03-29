using System.Globalization;
using CardManager.Application.DTO;
using CsvHelper;

namespace CardManager.Application.Utils
{
    public static class ProcessFile
    {
        public static async Task<List<CardDto>> Parse(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<CsvCardMapper>();
            var cards = csvReader.GetRecords<CardDto>().ToList();
            return cards;
        }
    }
}
