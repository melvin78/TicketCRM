using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;

namespace TicketCRM.SupportModule
{
    public interface IRecentActivityService
    {

        int AddRecentActivity(RecentActivityDTO recentActivityDto);

        Task<int> AddRecentActivityAsync(RecentActivityDTO recentActivityDto);

        List<RecentActivityDTO> FindRecentActivitiesAsync();
        
        List<RecentActivityDTO> FindRecentActivitiesAsync(string organizationId);
    }
}