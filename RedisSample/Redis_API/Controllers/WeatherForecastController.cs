using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Redis_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDatabase _database;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IDatabase database)
        {
            _logger = logger;
            this._database = database;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        // GET: api/Cache?key=key
        [HttpGet]
        public string Get([FromQuery]string key)
        {
            var a = _database.SortedSetScan("bbb");
            return _database.StringGet(key);
        }

        // POST: api/Cache
        [HttpPost]
        public void Post([FromBody]Test keyValue)
        {
            //_database.StringSet(keyValue.Key, keyValue.Value);
            //時間到仍可刪除 有變動即重新Run
            //_database.StringSet(keyValue.Key, keyValue.Value, TimeSpan.FromSeconds(20));
            //選擇當不存在時候才可以做增加Key
            //_database.StringSet(keyValue.Key, keyValue.Value, TimeSpan.FromSeconds(20),When.NotExists);
            //當存在時候才可以做修改Key
            //_database.StringSet(keyValue.Key, keyValue.Value, TimeSpan.FromSeconds(20), When.Exists);
            //_database.ListSetByIndex("a", 1, "1");
            //_database.ListSetByIndex("b", 2, "2");
            //_database.ListSetByIndex("c", 3, "3");
            //_database.ListSetByIndex("d", 4, "4");
            //Array.BinarySearch()
            // List 加入期限
            _database.SortedSetAdd("bbb", new List<SortedSetEntry>() {new SortedSetEntry("a01", new Random().Next(0, 100)),new SortedSetEntry("a02", new Random().Next(0, 100)),new SortedSetEntry("a03", new Random().Next(0, 100)) }.ToArray());
            _database.KeyExpire("bbb", TimeSpan.FromSeconds(60));
        }
    }

    public class Test
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
