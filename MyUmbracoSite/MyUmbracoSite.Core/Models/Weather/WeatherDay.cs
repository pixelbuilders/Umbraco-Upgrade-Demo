using System;

namespace MyUmbracoSite.Core.Models.Weather
{
    public class WeatherDay
    {
        public DateTime Date { get; set; }
        public string WeatherDescription { get; set; }
        public double Temperature { get; set; }
        public string TemperatureUnit { get; set; }
    }

}
