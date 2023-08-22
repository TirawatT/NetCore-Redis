using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisExample.Models;
using RedisExample.Utilities;
using System.Text;

namespace RedisExample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RedisController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly string _cacheUser = "my_user";
        public RedisController(IDistributedCache cache)
        {
            _cache = cache;
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            object user = null;
            var cacheUser = _cache.Get(_cacheUser);

            if (cacheUser == null)
            {
                user = new UserDto
                {
                    Name = "user_name",
                    Date = DateTime.Now.ToString(),
                };
                byte[] bytes = user.ObjectToByteArray();
                _cache.Set(_cacheUser, bytes, new DistributedCacheEntryOptions());
            }
            else
            {
                user = cacheUser.ByteArrayToObject<UserDto>();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult SetUser()
        {
            var user = new UserDto
            {
                Name = "user_name",
                Date = DateTime.Now.ToString(),
            };
            byte[] bytes = user.ObjectToByteArray();
            _cache.Set(_cacheUser, bytes, new DistributedCacheEntryOptions());

            return Ok(user);

        }
        [HttpGet]
        public IActionResult GetDateTime()
        {
            var CachedTimeUTC = "Cached Time Expired";
            var encodedCachedTimeUTC = _cache.Get("cachedTimeUTC");

            if (encodedCachedTimeUTC != null)
            {
                CachedTimeUTC = Encoding.UTF8.GetString(encodedCachedTimeUTC);
            }

            var ASP_Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (String.IsNullOrEmpty(ASP_Environment))
            {
                ASP_Environment = "Null, so Production";
            }
            return Ok(CachedTimeUTC);
        }
        [HttpPost]
        public IActionResult SetDateTime()
        {
            var currentTimeUTC = DateTime.UtcNow.ToString();
            byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(20))
                ;
            _cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);

            return Ok(currentTimeUTC);

        }

    }
}