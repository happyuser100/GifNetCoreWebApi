using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GiphyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]  
    //[ResponseCache(Duration = 60)]
    [ResponseCache(CacheProfileName = "default")]
    public class GiphyController : ControllerBase
    {
        string _BaseUrl = "";
        string _API_KEY = "";
        IConfiguration configuration;               

        public GiphyController(IConfiguration iConfig)
        {
            configuration = iConfig;
            _BaseUrl = configuration.GetSection("MySettings").GetSection("BaseUrl").Value;
            _API_KEY = configuration.GetSection("MySettings").GetSection("API_KEY").Value;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [HttpGet]
        [Route("SearchGiphy/{search}")]
        public string SearchGiphy(string search)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_BaseUrl + $"search?q={search}&api_key={_API_KEY}&limit=5");
            var data = client.GetAsync("").Result.Content.ReadAsStringAsync().Result;
            return data;
        }

        [HttpGet]
        [Route("SearchTrend")]
        public string SearchTrend()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_BaseUrl + $"trending?api_key={_API_KEY}&limit = 5");
            var data = client.GetAsync("").Result.Content.ReadAsStringAsync().Result;
            return data;
        }

    }
}
