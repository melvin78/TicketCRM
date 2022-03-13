using TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks;
using TicketCRM.SupportModule;

namespace TicketCRM.CronJobs
{
    public class ActivityCronJob:CronJobService
    {
        private readonly ILogger<ActivityCronJob> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ActivityCronJob(IScheduleConfig<ActivityCronJob> config,
            ILogger<ActivityCronJob> logger,
            IServiceProvider serviceProvider)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public override  async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} ActivityCronJob is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var assignTicketService = scope.ServiceProvider.GetRequiredService<IAssignTicketService>();
                assignTicketService.SaveActivity(cancellationToken);
            }
        }
    }
    
}