using Microsoft.AspNetCore.Mvc;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PusherController : ControllerBase
    {
        private readonly IPusherService _pusherService;

        public PusherController(IPusherService pusherService)
        {
            _pusherService = pusherService;
        }
        
        [HttpPost]
        [Route("auth")]
        [Consumes("application/x-www-form-urlencoded")]
        public  ActionResult<object> AuthenticateChannel([FromForm] IFormCollection value)
        {
        
            string channelName = value["channel_name"];
            string socketId = value["socket_id"];
            string userId = value["user_id"];
            return Ok(_pusherService.Auth(channelName,socketId,userId));
        }
      
    }
}