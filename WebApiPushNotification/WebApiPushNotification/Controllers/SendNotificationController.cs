using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPushNotification.Class;
using WebPush;

namespace JWTWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendNotificationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly WebPushNotificationConfig webPushNotification = new WebPushNotificationConfig();


        public SendNotificationController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationMessageText oMessage)
        {

            var directory = Environment.CurrentDirectory;
            string fileName = "\\Data\\JsonSubs.json";
            directory += fileName;
            if (System.IO.File.Exists(directory))
            {
                var sss = System.IO.File.ReadAllText(directory);

                List<NotificationSubscription> oTemp = System.Text.Json.JsonSerializer.Deserialize<List<NotificationSubscription>>(sss);
                var subject = "mailto:example@example.com";

                foreach (NotificationSubscription oSubscripcion in oTemp)
                {

                    var webPushClient = new WebPushClient();
                    var pushSubscription = new PushSubscription(oSubscripcion.Url, oSubscripcion.P256dh, oSubscripcion.Auth);
                    var payload = System.Text.Json.JsonSerializer.Serialize(oMessage);
                    var vapidDetails = WebPush.VapidHelper.GenerateVapidKeys();
                    vapidDetails.Subject = subject;

                    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);

                }
            }

            

            return Ok();
        }
    }
}
