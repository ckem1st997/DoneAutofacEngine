using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication3.Infrastructure;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly IHttpClientFactory _clientFactory;

        public WeatherForecastController(IHttpClientFactory clientFactory, IWebHostEnvironment hostingEnvironment)
        {
            _clientFactory = clientFactory;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("image/{name}")]
        [Authorize]
        public IActionResult GetFile(string name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", ""+ name + ".PNG");
            var fs = System.IO.File.OpenRead(filePath);
            return File(fs, "image/png");
        }


        [HttpPost]
        public async Task<IActionResult> GetAsync(DiscordMessageModel mes)
        {
            try
            {
                var avatar = "https://scontent.fhan12-1.fna.fbcdn.net/v/t1.6435-9/119562747_180284166996373_6804447371466693981_n.png?_nc_cat=109&ccb=1-7&_nc_sid=09cbfe&_nc_ohc=yS0FdZnAl-8AX-CHyRF&_nc_ht=scontent.fhan12-1.fna&oh=00_AT9QdTKkspuRzZIiOo8qbmV_CQeZZuSzv_xDUGeu6S8wtw&oe=62C532A5";
                var formdata = new MultipartFormDataContent();
                formdata.Add(new StringContent(mes.UserName), "username");
                formdata.Add(new StringContent(mes.Content), "content");
                formdata.Add(new StringContent(avatar), "avatar_url");
                formdata.Add(new StringContent(DateTime.Now.ToString()), "embeds.timestamp");
                var itemSend = new StringContent(JsonSerializer.Serialize(mes), Encoding.UTF8, Application.Json);
                mes.Webhook = "https://discord.com/api/webhooks/984016785886040074/zNrFc30q4Qk_WkhtIt7UQVjPQeG95Tr4tQRO-yBCszZ6aPrwpwrYddWYITftFE1p0IAu";
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsync(mes.Webhook, formdata);
                if (response.IsSuccessStatusCode)
                    return Ok(await response.Content.ReadAsStringAsync());
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }

            // tên serive name cần truyền tham số
            //var res = EngineContext.Current.Resolve<ITTT<WeatherForecastController>>("wh");
            //var res1 = EngineContext.Current.Resolve<ITTT<WeatherForecastController>>("Master");
            //return Ok(new
            //{
            //    class1 = res.GetString(),
            //    class2 = res1.GetString(),
            //    class3 = res.GetString(d)

            //});
        }

        public class DiscordMessageModel
        {
            public string Webhook { get; set; }

            public string UserName { get; set; }

            public string Avatar { get; set; }

            public string Content { get; set; }
        }
    }
}