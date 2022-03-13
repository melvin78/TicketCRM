using Microsoft.AspNetCore.Mvc;

namespace TicketCRM.SupportModule
{
    public interface IPusherService
    {
        Task<IActionResult> SendPusherNotification(object data, string channelName, string eventName,string socketId);
        
        
        Task<IActionResult> SendPusherNotificationAsync(object data, string channelName, string eventName);
        
        IActionResult SendPusherNotification(object data, string channelName, string eventName);

        ActionResult Auth(string channelName, string socketId,string userId);


    }
}