using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Infrastructure;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        public WeatherForecastController()
        {
        }

        [HttpGet()]
        public IActionResult Get(string d)
        {
            // tên serive name cần truyền tham số
            var res = EngineContext.Current.Resolve<ITTT<WeatherForecastController>>("wh");
            var res1 = EngineContext.Current.Resolve<ITTT<WeatherForecastController>>("Master");
            return Ok(new
            {
                class1 = res.GetString(),
                class2 = res1.GetString(),
                class3 = res.GetString(d)

            });
        }
    }
}