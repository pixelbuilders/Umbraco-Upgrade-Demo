using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyUmbracoSite.Core.Configuration;
using MyUmbracoSite.Core.Models.Weather;
using Newtonsoft.Json;

namespace MyUmbracoSite.Core.Services.Weather
{
    public interface IWeatherService
    {
        List<WeatherDay> GetWeatherForecast(string query, int days);
    }

    public class WeatherService : IWeatherService
    {

        private MyCustomAppSettings myCustomAppSettings { get; set; }

        private const string TomorrowIoUrl = "https://api.tomorrow.io/v4/timelines";
        private const string CacheFileName = "~/weatherdata.json";  // File name for caching

        private readonly ILogger<WeatherService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WeatherService(ILogger<WeatherService> logger, IWebHostEnvironment webHostEnvironment, IOptions<MyCustomAppSettings> options)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            myCustomAppSettings = options.Value;
        }

        public List<WeatherDay> GetWeatherForecast(string query, int days)
        {
            List<WeatherDay> weatherDays = new List<WeatherDay>();
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string cacheFilePath = Path.Combine(contentRootPath, CacheFileName); // Path to store cached data

            try
            {
                // Check if the cache file exists and is not older than 3 hours
                if (File.Exists(cacheFilePath) && IsCacheValid(cacheFilePath))
                {
                    // If valid, read the cached data
                    var cachedData = File.ReadAllText(cacheFilePath);
                    weatherDays = JsonConvert.DeserializeObject<List<WeatherDay>>(cachedData);
                }
                else
                {
                    // If not valid, fetch the data from the API

                    _logger.LogInformation("WeatherService cache file '{CacheFileName}' has expired. Requires API refresh.", CacheFileName);

                    using (HttpClient client = new HttpClient())
                    {
                        var startTime = DateTime.UtcNow;
                        var endTime = startTime.AddDays(days - 1);

                        // Format dates as required by the Tomorrow.io API
                        string startTimeFormatted = startTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
                        string endTimeFormatted = endTime.ToString("yyyy-MM-ddTHH:mm:ssZ");

                        // Prepare the request URL
                        string url = $"{TomorrowIoUrl}?location={myCustomAppSettings.LocationLatitude},{myCustomAppSettings.LocationLongitude}&fields=temperature,weatherCode&units=metric&timesteps=1d&startTime={startTimeFormatted}&endTime={endTimeFormatted}&apikey={myCustomAppSettings.ApiKey}";

                        // Fetch the weather data from Tomorrow.io
                        string jsonContent = client.GetStringAsync(url).Result;

                        // Deserialize the JSON content into an object
                        dynamic data = JsonConvert.DeserializeObject(jsonContent);

                        // Extract the forecast data (we will extract the daily forecast)
                        var timeline = data.data.timelines;

                        int dayIndex = 0;
                        foreach (var day in timeline[0].intervals) // Get the weather forecast intervals
                        {
                            if (dayIndex >= 3) break; // We are only interested in the next 3 days

                            var weatherDay = new WeatherDay
                            {
                                Date = DateTime.Parse(day.startTime.ToString()),  // The start time of the interval is the date
                                WeatherDescription = GetWeatherDescription(day.values.weatherCode.ToString()), // Get weather description from code
                                Temperature = day.values.temperature.Value,  // Temperature for the day
                                TemperatureUnit = "°C"
                            };

                            weatherDays.Add(weatherDay);
                            dayIndex++;
                        }

                        // Cache the fetched data in the JSON file
                        File.WriteAllText(cacheFilePath, JsonConvert.SerializeObject(weatherDays));
                        _logger.LogInformation("Updated WeatherService cache file: {CacheFileName}", CacheFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors (e.g., network issues, JSON parsing issues)
                _logger.LogError(ex, "Error fetching weather data: {Message}", ex.Message);
            }

            return weatherDays;
        }
        private string GetWeatherDescription(string weatherCode)
        {
            // Map weather codes to their descriptions
            switch (weatherCode)
            {
                case "0":
                    return "Unknown";
                case "1000":
                    return "Clear, Sunny";
                case "1100":
                    return "Mostly Clear";
                case "1101":
                    return "Partly Cloudy";
                case "1102":
                    return "Mostly Cloudy";
                case "1001":
                    return "Cloudy";
                case "2000":
                    return "Fog";
                case "2100":
                    return "Light Fog";
                case "4000":
                    return "Drizzle";
                case "4001":
                    return "Rain";
                case "4200":
                    return "Light Rain";
                case "4201":
                    return "Heavy Rain";
                case "5000":
                    return "Snow";
                case "5001":
                    return "Flurries";
                case "5100":
                    return "Light Snow";
                case "5101":
                    return "Heavy Snow";
                case "6000":
                    return "Freezing Drizzle";
                case "6001":
                    return "Freezing Rain";
                case "6200":
                    return "Light Freezing Rain";
                case "6201":
                    return "Heavy Freezing Rain";
                case "7000":
                    return "Ice Pellets";
                case "7101":
                    return "Heavy Ice Pellets";
                case "7102":
                    return "Light Ice Pellets";
                case "8000":
                    return "Thunderstorm";
                default:
                    return "Unknown";
            }
        }

        private bool IsCacheValid(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                var age = DateTime.UtcNow - fileInfo.LastWriteTimeUtc;
                return age.TotalHours <= 3; // Cache is valid if it is less than or equal to 3 hours old
            }
            catch
            {
                return false; // If there is any issue with the file, consider the cache invalid
            }
        }
    }
}
