using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPushNotification.Class;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Authorization;

namespace JWTWebApi.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Notification/[controller]")]
    public class NotificationSubscribeController : ControllerBase
    {
        public IConfiguration _configuration;

        public NotificationSubscribeController(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>  
        /// Subcripcion para los PushNotification Requiere JWT  
        /// </summary>
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> NotificationSubscribe(NotificationSubscription oSubscripcion)
        {
            try
            {
                var directory = Environment.CurrentDirectory;

                string fileName = "\\Data\\JsonSubs.json";
                directory += fileName;
                List<NotificationSubscription> oListaSubs;

                if (System.IO.File.Exists(directory))
                {
                    var sss = System.IO.File.ReadAllText(directory);

                    oListaSubs = System.Text.Json.JsonSerializer.Deserialize<List<NotificationSubscription>>(sss);

                }
                else
                {
                    oListaSubs = new List<NotificationSubscription>();
                }


                oListaSubs.Add(oSubscripcion);

                var payload = System.Text.Json.JsonSerializer.Serialize(oListaSubs);

                

                // simplest way to write to file
                System.IO.File.WriteAllText(directory, payload);

            }
            catch (Exception ex)
            { }

            return Ok();
        }
    }
}
