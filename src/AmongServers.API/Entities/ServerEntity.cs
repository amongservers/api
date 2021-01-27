using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmongServers.API.Entities
{
    /// <summary>
    /// Defines a server response to the client.
    /// </summary>
    public class ServerEntity : HeartbeatEntity
    {
        /// <summary>
        /// Gets or sets the last heartbeat time for the server.
        /// </summary>
        [JsonProperty("lastSeenAt")]
        public DateTimeOffset LastSeenAt { get; set; }
    }
}
