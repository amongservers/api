using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmongServers.API.Entities
{
    /// <summary>
    /// Defines a game input for the heartbeat.
    /// </summary>
    public class GameEntity
    {
        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        [Required]
        [JsonProperty("players")]
        public PlayerEntity[] Players { get; set; }

        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        [JsonProperty("hostPlayer")]
        public PlayerEntity HostPlayer { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [Required]
        [MaxLength(16)]
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        [Required]
        [MaxLength(128)]
        [JsonProperty("map")]
        public string Map { get; set; }

        /// <summary>
        /// Gets or sets the max players.
        /// </summary>
        [Required]
        [Range(4, 10)]
        [JsonProperty("maxPlayers")]
        public int MaxPlayers { get; set; }

        /// <summary>
        /// Gets or sets the number of impostors.
        /// </summary>
        [Required]
        [Range(1, 3)]
        [JsonProperty("numImpostors")]
        public int NumImposters { get; set; }

        /// <summary>
        /// Gets or sets if the game is public.
        /// </summary>
        [Required]
        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }
    }
}
