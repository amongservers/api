using AmongServers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
        public ServerEntity[] Index()
        {
            var server = _cache.GetServer("127.0.0.1:6379");
            var database = _cache.GetDatabase(0);
            var keys = server.Keys(0, "*", pageSize: 1000).ToArray();
            
            // Build the response from the keys
            Dictionary<string, ServerEntity> servers = new Dictionary<string, ServerEntity>();
            foreach(var k in keys)
            {
                try
                {
                    string[] split = k.ToString().Split(":");
                    if (split.Length != 3)
                        continue;

                    string key = string.Join(':', split[0], split[1]);
                    if (!servers.ContainsKey(key))
                        servers[key] = new ServerEntity
                        {
                            Endpoint = IPEndPoint.Parse(key).ToString()
                        };

                    if (split.Length > 2)
                    {
                        switch (split[2])
                        {
                            case "games":

                                servers[key].Games = JsonConvert.DeserializeObject<GameEntity[]>(database.StringGet(k));
                                break;
                            case "name":
                                servers[key].Name = database.StringGet(k);
                                break;
                            case "lastSeenAt":
                                servers[key].LastSeenAt = DateTimeOffset.Parse(database.StringGet(k));
                                break;
                        }
                    }
                }
                catch { };
            }

            return servers.Select(x => x.Value).ToArray();
        }
        #endregion

        #region Constructor
        public ServerController(IConnectionMultiplexer cache) =>
            (_cache) = (cache);
        #endregion
    }
}
