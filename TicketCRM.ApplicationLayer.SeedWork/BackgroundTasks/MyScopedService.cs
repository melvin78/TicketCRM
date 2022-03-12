using Microsoft.Extensions.Logging;

namespace TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks
{
 
    public class MyScopedService<T> : IMyScopedService<T>
    {
        private readonly ILogger<MyScopedService<T>> _logger;

        public MyScopedService(ILogger<MyScopedService<T>> logger)
        {
            _logger = logger;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} MyScopedService is working.");
            await Task.Delay(1000 * 20, cancellationToken);
        }
    }
}
