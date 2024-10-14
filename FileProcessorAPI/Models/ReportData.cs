namespace FileProcessorAPI.Models
{
    public class ReportData
    {
        public ReportData()
        {
            ReportType = 0;
        }

        public int ReportType { get; set; }
        public string ReportName { get; set; }
        public double DurationSeconds { get; set; } = 0.00;

    }
}
