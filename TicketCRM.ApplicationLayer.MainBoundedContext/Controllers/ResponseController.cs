using Microsoft.AspNetCore.Mvc;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }
        
        [HttpGet]
        [Route("get-responses-by-ticket-no/{ticketNo}")]
        public async Task<ActionResult<List<ResponseDTO>>> GetEnquiryCategoryByEnquiry(string ticketNo)
        {
            return Ok(await _responseService.GetAllResponsesAsync(ticketNo));
        }
        
        [HttpPost]
        [Route("add-response")]
        public async Task<ActionResult<Guid>> AddNewResponse(ResponseDTO responseDto)
        {
            
            return Ok(await _responseService.AddResponseAsync(responseDto));
        }
        
        [HttpGet]
        [Route("unread-responses/{toId}")]
        public async Task<ActionResult<List<ResponseDTO>>> UnreadResponses(string toId)
        {
            return Ok(await _responseService.UnreadResponses(Guid.Parse(toId)));
        }
        
        [HttpGet]
        [Route("mark-as-read/{responseId}")]
        public async Task<ActionResult<List<ResponseDTO>>> MarkAsRead(string responseId)
        {
            return Ok(await _responseService.MarkAsRead(Guid.Parse(responseId)));
        }
        
        [HttpGet]
        [Route("chat-Room/{userId}")]
        public  ActionResult<List<ResponseDTO>> ChatRoom(string userId)
        {
            return Ok(_responseService.ConfigureRooms(Guid.Parse(userId)));
        }
        
    }
}