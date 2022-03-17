using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecentActivityController : ControllerBase
    {
        private readonly IRecentActivityService _recentActivityService;

        public RecentActivityController(IRecentActivityService recentActivityService)
        {
            _recentActivityService = recentActivityService;
        }

        
        [HttpGet]
        [Route("recent-activity")]
        public ActionResult<List<RecentActivityDTO>> FindRecentActivities()
        {
            return Ok(_recentActivityService.FindRecentActivitiesAsync());
        }
        
        [HttpGet]
        [Route("sacco-recent-activity/{organizationId}")]
        public ActionResult<List<RecentActivityDTO>> FindSaccoRecentActivities(string organizationId)
        {
            return Ok(_recentActivityService.FindRecentActivitiesAsync(organizationId));
        }
    }
    
}