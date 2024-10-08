using CsvHelper.Configuration.Attributes;

namespace FileProcessorAPI.Models
{
    public class DAILYSALE
    {
        [Index(0)]
        public string Branch { get; set; } = "";

        [Index(1)]
        public string DayOfWeek { get; set; } = "";

        [Index(2)]
        public DateOnly Date { get; set; }

        [Index(3)]
        public double TotalSales { get; set; } = 0.0;
    }
}
