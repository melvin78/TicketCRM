using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : ControllerBase
    {
        private readonly IInboxService _inboxService;

        public InboxController(IInboxService inboxService)
        {
            _inboxService = inboxService;
        }
        
        
        [HttpGet]
        [Route("get-inbox/{userId}")]
        public ActionResult<List<InboxDTO>> GetInbox(string userId)
        {
            return Ok(_inboxService.GetInboxes(Guid.Parse(userId)));

        }
        
        [HttpGet]
        [Route("get-single-inbox/{ticketNo}")]
        public ActionResult<SingleRoomDTO> GetSingleInbox(string ticketNo)
        {
            return Ok(_inboxService.GetInbox(ticketNo));

        }
        
        [HttpGet]
        [Route("get-inbox-id/{ticketNo}")]
        public ActionResult<SingleRoomDTO> GetInboxId(string ticketNo)
        {
            return Ok(_inboxService.GetInboxId(ticketNo));

        }

        
    }
}