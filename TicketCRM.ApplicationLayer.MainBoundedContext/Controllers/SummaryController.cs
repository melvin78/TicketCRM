using Microsoft.AspNetCore.Mvc;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public SummaryController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        
        [HttpGet]
        [Route("pending-tickets")]
        public async Task<ActionResult<string>> TicketsOpenedByUser()
        {
            return Ok(await _ticketService.GetAllTicketsOpenedByUser(Guid.Parse("userId")));
        }
    }
}