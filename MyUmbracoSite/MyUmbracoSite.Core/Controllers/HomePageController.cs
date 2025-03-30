using MyUmbracoSite.Core.Configuration;
using MyUmbracoSite.Core.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace MyUmbracoSite.Core.Controllers
{
    public class HomePageController : RenderMvcController
    {
        private readonly IWeatherService _weatherService;
        private MyCustomAppSettings myCustomAppSettings = new MyCustomAppSettings();

        public HomePageController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public override ActionResult Index(ContentModel model)
        {
            var viewModel = (HomePage)model.Content;

            var weatherForecast = _weatherService.GetWeatherForecast("Leeds,UK", 3);

            viewModel.WeatherLocation = myCustomAppSettings.TomorrowIoLocationName;
            viewModel.WeatherForecast = weatherForecast;

            return CurrentTemplate(viewModel);
        }
    }
}
