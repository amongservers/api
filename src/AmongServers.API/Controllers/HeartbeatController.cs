using AmongServers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AmongServers.API.Controllers
{
    /// <summary>
    /// Defines the heartbeat routes.
    /// </summary>
    [ApiController]
    [Route("heartbeat")]
    public class HeartbeatController : Controller
    {
        #region Fields
        private readonly IConnectionMultiplexer _cache;
        #endregion

        #region Routes
        /// <summary>
        /// Receives a request from the client to add.
        /// </summary>
        /// <param name="input">A JSON body input.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index([FromBody] HeartbeatEntity input)
        {
            if (!ValdiateClient())
                return BadRequest("Request refused");

            var database = _cache.GetDatabase(0);

            // Parse the endpoint
            if (!IPEndPoint.TryParse(input.Endpoint, out IPEndPoint endpoint))
                return BadRequest("Invalid endpoint");

            // If no IP was provided, use the connection IP
            if (endpoint.Address.Equals(IPAddress.Any) || endpoint.Address.Equals(IPAddress.IPv6Any) || IPAddress.IsLoopback(endpoint.Address))
            {
                if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues values))
                {
                    // Runs behind an NGINX proxy
                    if (values.Count > 1)
                        return BadRequest("Too many forwarded headers!");
                    endpoint.Address = IPAddress.Parse(values[0].Split(", ")[0]);
                } else
                {
                    endpoint.Address = HttpContext.Connection.RemoteIpAddress;
                }
            }

#if DEBUG
            if (endpoint.Address.ToString() == "::1")
                endpoint.Address = IPAddress.Loopback;
#endif

            // Save to redis.
            string key = $"{endpoint.Address}:{endpoint.Port}";
            database.StringSet($"{key}:name", input.Name, TimeSpan.FromMinutes(1));
            database.StringSet($"{key}:games", JsonConvert.SerializeObject(input.Games), TimeSpan.FromMinutes(1));
            database.StringSet($"{key}:lastSeenAt", DateTimeOffset.UtcNow.ToString(), TimeSpan.FromMinutes(1));

            return NoContent();
        }
#endregion

#region Private Methods
        /// <summary>
        /// TODO: Performs a few basic checks on the client.
        /// </summary>
        /// <returns></returns>
        private bool ValdiateClient()
        {
            return true;
        }
#endregion

#region Constructor
        public HeartbeatController(IConnectionMultiplexer cache) =>
            (_cache) = (cache);
#endregion
    }
}
