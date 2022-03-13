using TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks;
using TicketCRM.SupportModule;

namespace TicketCRM.CronJobs
{
    public class OverdueCronJob:CronJobService
    {
        private readonly ILogger<OverdueCronJob> _logger;
        private readonly IServiceProvider _serviceProvider;
        
        public OverdueCronJob(IScheduleConfig<OverdueCronJob> config,
            ILogger<OverdueCronJob> logger,
            IServiceProvider serviceProvider) : base(config.CronExpression, config.TimeZoneInfo)
        {
            
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        
        public override  async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Overdue Cron Job is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var assignTicketService = scope.ServiceProvider.GetRequiredService<IAssignTicketService>();
                assignTicketService.MarkTicketsAsOverdue(cancellationToken);
            }
        }
        
        
    }
}