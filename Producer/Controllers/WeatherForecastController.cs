using Microsoft.AspNetCore.Mvc;
using Producer.Services;
using Shared.Models.Dtos;

namespace Producer.Controllers
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
        private readonly IProducerService _producerService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProducerService producerService)
        {
            _logger = logger;
            _producerService = producerService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("email")]
        public ActionResult<string> SendEmail([FromBody]EmailDto email)
        {
            _producerService.SendEmail(email);
            return Ok(email);
        }

        [HttpPost("noti")]
        public ActionResult<string> SendNoti([FromBody] NotificationDto noti)
        {
            _producerService.SendNoti(noti);
            return Ok(noti);
        }
    }
}
