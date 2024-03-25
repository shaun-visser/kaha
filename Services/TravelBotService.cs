using KAHA.TravelBot.NETCoreReactApp.Models;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;

namespace KAHA.TravelBot.NETCoreReactApp.Services
{
    public class TravelBotService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TravelBotService> _logger;

        public TravelBotService(ILogger<TravelBotService> logger)
        {
            _httpClient = new HttpClient();
            _logger = logger;
        }

        public async Task<List<CountryModel>> GetAllCountries()
        {
            var apiUrl = "https://restcountries.com/v3.1/all";

            try
            {
                _logger.LogInformation("Fetching countries...");
                var response = await _httpClient.GetStringAsync(apiUrl);
                _logger.LogError($"respone: {response}");
                var countries = ParseCountries(response);
                return countries;
            } catch (HttpRequestException ex)
            {
                // Logging the exception
                _logger.LogError($"Error fetching countries: {ex.Message}");
                throw;
            }
        }
        private List<CountryModel> ParseCountries(string response)
        {
            var countries = new List<CountryModel>();

            var parsedResponse = JArray.Parse(response);

            foreach (var countryJson in parsedResponse)
            {
                try
                {
                    var nameToken = countryJson["name"];
                    var capitalToken = countryJson["capital"];
                    var latlngToken = countryJson["latlng"];

                    // Check if all required properties are present
                    if (nameToken != null && capitalToken != null && latlngToken != null)
                    {
                        var name = nameToken.Value<string>("common");
                        var capital = capitalToken?.ToString();

                        // Extract latitude and longitude (handle null values)
                        var latitude = latlngToken[0]?.Value<double?>() ?? 0.0;
                        var longitude = latlngToken[1]?.Value<double?>() ?? 0.0;

                        // Create a CountryModel instance
                        var country = new CountryModel
                        {
                            Name = name ?? "Unknown",
                            Capital = capital ?? "Unknown",
                            Latitude = latitude,
                            Longitude = longitude
                        };

                        countries.Add(country);
                    }
                }
                catch (Exception ex)
                {
                    // Log parsing errors with context
                    _logger.LogError($"Error parsing country data: {ex.Message}. Country: {countryJson}");
                }
            }

            return countries;
        }

        // Top 5 Countries by population size
        public async Task<List<CountryModel>> GetTopFiveCountries()
        {
            try
            {
                var countries = await GetAllCountries();
                var southernHemisphereCountries = countries.Where(c => c.Latitude < 0).OrderByDescending(c => c.Population).Take(5).ToList();
                return southernHemisphereCountries;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching countries: {ex.Message}");
                throw;
            }
        }

        public CountrySummaryModel GetCountrySummary(string countryName)
        {
            var sunriseSunsetTimes = GetSunriseSunsetTimes(countryName);
            throw new NotImplementedException();
        } 

        public CountryModel RandomCountryInSouthernHemisphere(List<CountryModel> countries)
        {
            var countriesInSouthernHemisphere = countries.Where(x => x.Latitude < 0);
            var random = new Random();
            var randomIndex = random.Next(0, countriesInSouthernHemisphere.Count());
            return countriesInSouthernHemisphere.ElementAt(randomIndex);
        }

        public async Task<(string, string)> GetSunriseSunsetTimes(string countryName)
        {
            // Implement logic to get sunrise and sunset times for tomorrow
            // using https://sunrise-sunset.org/api
            throw new NotImplementedException();
        }
    }
}
