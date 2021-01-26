using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmongServers.API.Entities
{
    /// <summary>
    /// Defines a heartbeat request from the client.
    /// </summary>
    public class HeartbeatEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(128)]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        [Required]
        [JsonProperty("ipAddress")]
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        [Required]
        [Range(0, 65535)]
        [JsonProperty("port")]
        public string Port { get; set; }
    }
}
