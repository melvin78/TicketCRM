using TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks;
using TicketCRM.SupportModule;

namespace TicketCRM.CronJobs
{
    public class AssignTicketCronJob : CronJobService
    {
        private readonly ILogger<AssignTicketCronJob> _logger;
        private readonly IServiceProvider _serviceProvider;

        public AssignTicketCronJob(IScheduleConfig<AssignTicketCronJob> config,
            ILogger<AssignTicketCronJob> logger,
            IServiceProvider serviceProvider)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override  async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Assing Ticket Cron Job is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var assignTicketService = scope.ServiceProvider.GetRequiredService<IAssignTicketService>();
                assignTicketService.AssignRoundRobin(cancellationToken);
            }
        }
    }
}