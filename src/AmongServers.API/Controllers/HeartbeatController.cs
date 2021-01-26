using AmongServers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Add([FromBody] HeartbeatEntity input)
        {
            if (!ValdiateClient())
                return BadRequest("Request refused");

            // Add the server to the cache and only last for one minute.
            var database = _cache.GetDatabase(0);
            database.StringSet($"{input.IPAddress}:{input.Port}", input.Name, TimeSpan.FromMinutes(1));

            // 204 response.
            return NoContent();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Performs a few basic checks on the client.
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
