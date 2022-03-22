using IdentityServerAspNetIdentity.Services;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace TicketCRM.SupportModule
{
    public class PusherService:IPusherService
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IConfiguration _configuration;

        public PusherService(IApplicationUserService applicationUserService,IConfiguration  configuration)
        {
            _applicationUserService = applicationUserService;
            _configuration = configuration;
        }

       
        public async Task<IActionResult> SendPusherNotification(object data, string channelName, string eventName,string socketId)
        {
            var options = new PusherOptions()
            {
                // Cluster = _configuration["PusherSettings:Cluster"],
                Cluster = Environment.GetEnvironmentVariable("Cluster"),
                Encrypted = true,
            };

            var pusher = new Pusher(Environment.GetEnvironmentVariable("appId"), Environment.GetEnvironmentVariable("appKey"), Environment.GetEnvironmentVariable("appSecret"),options);

            var result = await pusher.TriggerAsync(channelName, eventName, data,new TriggerOptions()
            {
                SocketId = socketId
            });

            return new OkObjectResult(result);
        }

        public async Task<IActionResult> SendPusherNotificationAsync(object data, string channelName, string eventName)
        {
            var options = new PusherOptions()
            {
                // Cluster = _configuration["PusherSettings:Cluster"],
                Cluster = Environment.GetEnvironmentVariable("Cluster"),

                Encrypted = true,
            };

            var pusher = new Pusher(Environment.GetEnvironmentVariable("appId"),Environment.GetEnvironmentVariable("appKey"),Environment.GetEnvironmentVariable("appSecret"),options);

            var result = await pusher.TriggerAsync(channelName, eventName, data);

            return new OkObjectResult(result);
        }

        public IActionResult SendPusherNotification(object data, string channelName, string eventName)
        {
            var options = new PusherOptions()
            {
                Cluster = Environment.GetEnvironmentVariable("Cluster"),
                Encrypted = true,
            };

            var pusher = new Pusher(Environment.GetEnvironmentVariable("appId"),Environment.GetEnvironmentVariable("appKey"), Environment.GetEnvironmentVariable("appSecret"),options);

            var result = pusher.TriggerAsync(channelName, eventName, data).Result;

            return new OkObjectResult(result);
        }

        public ActionResult Auth(string channelName, string socketId,string userId)
        {
            var options = new PusherOptions()
            {
                Cluster = _configuration["PusherSettings:Cluster"],
                Encrypted = true,
            };
            var res = _applicationUserService.GetUserDetails(userId);
            
            var channelData = new PresenceChannelData() {
                
               user_id = res.Id,
               
               user_info = new
               {
                   name = res.FirstName,
                   email = res.Email
               }
            };
            
            var pusher = new Pusher(_configuration["PusherSettings:AppId"], _configuration["PusherSettings:AppKey"],_configuration["PusherSettings:AppSecret"],options);

            var auth = pusher.Authenticate(channelName, socketId,channelData);

            var json = auth.ToJson();

            return new ContentResult
            {
                Content = json,
                ContentType = "application/json"

            };

        }
    }
}