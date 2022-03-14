using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketStatusController : ControllerBase
    {
        private readonly ITicketStatusService _ticketStatusService;

        public TicketStatusController(ITicketStatusService ticketStatusService)
        {
            _ticketStatusService = ticketStatusService;
        }
        

        [HttpGet]
        [Route("add-new-ticket-status")]
        public async Task<ActionResult<bool>> AddNewTicketStatus(string values)
        {
            var ticketStatusDto = new TicketStatusDTO()
            {
                TicketStatusVal = values
            };

        
            await _ticketStatusService.AddNewTicketStatus(ticketStatusDto);

            return Ok(true);
        }

        [HttpGet]
        [Route("get-ticket-statuses")]
        public async Task<ActionResult<IEnumerable<TicketStatusDTO>>> GetTicketStatus()
        {

            return Ok(await _ticketStatusService.GetTicketStatus());
        }
    }
}