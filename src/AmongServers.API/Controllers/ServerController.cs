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
    /// Defines the server routes.
    /// </summary>
    [ApiController]
    [Route("server")]
    public class ServerController : Controller
    {
        #region Fields
        private readonly IConnectionMultiplexer _cache;
        #endregion

        #region Methods
        /// <summary>
        /// Returns a list of servers to the launcher.
        /// </summary>
        [HttpGet]
        public List<ServerEntity> Index()
        {
            var server = _cache.GetServer("127.0.0.1:6379");
            var database = _cache.GetDatabase(0);
            var keys = server.Keys(0, "*", pageSize: 1000).ToArray();

            List<ServerEntity> response = new List<ServerEntity>();
            foreach(var k in keys)
            {
                try
                {
                    string[] split = k.ToString().Split(":");
                    response.Add(new ServerEntity
                    {
                        IPAddress = split[0],
                        Port = split[1],
                        Name = database.StringGet(k).ToString()
                    });
                }
                catch { };
            }

            return response;
        }
        #endregion

        #region Constructor
        public ServerController(IConnectionMultiplexer cache) =>
            (_cache) = (cache);
        #endregion
    }
}
