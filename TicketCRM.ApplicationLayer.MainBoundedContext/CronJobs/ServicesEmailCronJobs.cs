using TicketCRM.ApplicationLayer.SeedWork.BackgroundTasks;

namespace TicketCRM.CronJobs
{
    public class ServicesEmailCronJobs:CronJobService
    {
        public ServicesEmailCronJobs(string cronExpression, TimeZoneInfo timeZoneInfo) : base(cronExpression, timeZoneInfo)
        {
            
        }
    }
}