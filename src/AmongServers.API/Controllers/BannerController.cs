using AmongServers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmongServers.API.Controllers
{
    /// <summary>
    /// Defines the banner routes.
    /// </summary>
    [ApiController]
    [Route("banner")]
    public class BannerController : Controller
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        #endregion

        #region Routes
        /// <summary>
        /// Returns the banners to show in the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BannerEntity[] Index([FromQuery]string version)
        {
            //TODO: if version is older (current is 0.1.0) send a banner with an update image and URL

            if (_cache.TryGetValue("banners", out BannerEntity[] banners))
            {
                return banners;
            }
            else
            {
                banners = _configuration.GetSection("Banners").Get<BannerEntity[]>();
                _cache.Set("banners", banners);
                return banners;
            }
        }
        #endregion

        #region Constructor
        public BannerController(IConfiguration configuration, IMemoryCache cache) =>
            (_configuration, _cache) = (configuration, cache);
        #endregion
    }
}
