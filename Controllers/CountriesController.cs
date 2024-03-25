using KAHA.TravelBot.NETCoreReactApp.Models;
using KAHA.TravelBot.NETCoreReactApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KAHA.TravelBot.NETCoreReactApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class CountriesController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherService;
        private readonly TravelBotService _travelBotService;
        private readonly ILogger<CountriesController> _logger;

        // GET: api/<CountriesController>
        [HttpGet("top5")]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetTopFive()
        {
            try
            {
                var topFiveCountries = await _travelBotService.GetTopFiveCountries();
                return Ok(topFiveCountries);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Error getting top 5 countries: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/<CountriesController>/Zimbabwe
        [HttpGet("{countryName}")]
        public string GetSummary(string countryName)
        {
            return "value";
        }

        // POST api/<CountriesController>
        [HttpGet("random")]
        public void GetRandomCountry()
        {
        }
    }
}
