using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmongServers.API.Entities
{
    /// <summary>
    /// Defines a player input.
    /// </summary>
    public class PlayerEntity
    {
        [Required]
        [MaxLength(128)]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
