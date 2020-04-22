using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;


namespace aspnetapp.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private const string cacheKey = "MyKey";
        private readonly IDistributedCache _distributedCache;

        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public string Get()
        {
            var existingValue = _distributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(existingValue))
            {
                return "Fetched from cache : " + existingValue;
            }
            else
            {
                return "Cache is empty.  Add something with a post!";
            }
        }


        [HttpPost]
        public void PostAdd(string value) {            
            if (value != null) {
                _distributedCache.SetString(cacheKey, value);                        
            }
        }

    }
}