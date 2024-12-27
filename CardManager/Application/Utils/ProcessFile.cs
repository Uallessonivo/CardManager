using System.Globalization;
using CardManager.Application.DTO;
using CardManager.Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;

namespace CardManager.Application.Utils
{
    public class ProcessFile : IProcessFile
    {
        public List<CardDto> Parse(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            });
            csvReader.Context.RegisterClassMap<CsvCardMapper>();
            var cards = csvReader.GetRecords<CardDto>().ToList();
            return cards;
        }
    }
}
