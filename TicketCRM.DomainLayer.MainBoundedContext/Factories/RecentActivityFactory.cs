using TicketCRM.DomainLayer.MainBoundedContext.SupportEntities;

namespace TicketCRM.DomainLayer.MainBoundedContext.Factories
{
    public static class RecentActivityFactory
    {
        public static RecentActivity AddRecentActivity(string color, string icon,
            string task, string ticketNumber,
            string emailAddress,Guid organizationId)
        {
            var recentActivity = new RecentActivity();
            
            recentActivity.GenerateNewIdentity();

            recentActivity.Color = color;

            recentActivity.Icon = icon;

            recentActivity.Task = task;

            recentActivity.TicketNumber = ticketNumber;

            recentActivity.EmailAddress = emailAddress;
            
            recentActivity.CreatedAt= DateTime.Now;

            recentActivity.OrganizationId = organizationId;

            return recentActivity;
        }
    }
}