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
    public class ServerEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        [JsonProperty("ipAddress")]
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        [JsonProperty("port")]
        public string Port { get; set; }
    }
}
