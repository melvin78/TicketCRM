using TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks;
using TicketCRM.SupportModule;

namespace TicketCRM.CronJobs
{
    public class ReminderCronJob:CronJobService
    {
        private readonly ILogger<ReminderCronJob> _logger;
        private readonly IServiceProvider _serviceProvider;
        
        public ReminderCronJob(IScheduleConfig<ReminderCronJob> config,
            ILogger<ReminderCronJob> logger,
            IServiceProvider serviceProvider) : base(config.CronExpression, config.TimeZoneInfo)
        {
            
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        
        public override  async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Reminder Cron Job is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();
                ticketService.SendReminderNotification(cancellationToken);
            }
        }

    }
}