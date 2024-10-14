using FileProcessorAPI.Models;

namespace FileProcessorAPI.DataProcess
{
    public class ReportService : BackgroundService
    {
        public ReportService() { }

        private readonly PeriodicTimer _periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _periodicTimer.WaitForNextTickAsync(stoppingToken))
            {
                await ProcessReportAsync(1);
            }
        }


        public async Task  ProcessReportAsync(int reportType)
        {
            // Start the Processing the report here...
            var report = new ReportData();
        }
    }
}
