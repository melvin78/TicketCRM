using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityLevelController : ControllerBase
    {
        private readonly IPriorityLevelService _priorityLevelService;

        public PriorityLevelController(IPriorityLevelService priorityLevelService)
        {
            _priorityLevelService = priorityLevelService;
        }
        
        [HttpPost]
        [Route("add-new-priority")]
        public ActionResult AddNewPriority(string description,int level)
        {
            var priorityLevel = new PriorityLevelDTO();

            priorityLevel.Level = level;
            priorityLevel.Description = description;

           _priorityLevelService.AddPriorityLevel(priorityLevel);

           return Ok(true);
        }
        
        [HttpGet]
        [Route("get-priority-level")]
        public ActionResult<List<PriorityLevelDTO>> GetPriorityLevel()
        {
            return Ok(_priorityLevelService.FindPrioritiesLevel());

        }

    }
}
