using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketCRM.DomainLayer.MainBoundedContextDTO.SupportEntities;
using TicketCRM.SupportModule;

namespace TicketCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatsController> _logger;

        public ChatsController(IChatService chatService,ILogger<ChatsController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [HttpPost]
        [Route("send-message")]
        public async Task<ActionResult<Guid>> AddMessage(ChatDTO chatDto)
        {
            return Ok(await _chatService.AddChatAsync(chatDto));
        }

        [HttpGet]
        [Route("retrieve-messages/{inboxId}")]
        public ActionResult GetMessage(string inboxId)
        {
            return Ok(_chatService.GetChatsAsync(inboxId));
        }
        
        [HttpPost]
        [Route("mailgun-message")]
        public ActionResult MailgunMessage([FromForm]string subject)
        {
            _logger.LogInformation(subject);
            return Ok();
        }
    }
}