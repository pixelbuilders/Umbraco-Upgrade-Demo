using MyUmbracoSite.Core.Models.Weather;
using System.Collections.Generic;

namespace Umbraco.Web.PublishedModels
{
    public partial class HomePage
    {
        public string WeatherLocation { get; set; }
        public List<WeatherDay> WeatherForecast { get; set; }
    }
}
