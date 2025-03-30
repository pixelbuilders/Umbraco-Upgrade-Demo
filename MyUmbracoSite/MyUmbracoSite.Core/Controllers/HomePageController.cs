using MyUmbracoSite.Core.Configuration;
using MyUmbracoSite.Core.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace MyUmbracoSite.Core.Controllers
{
    public class HomePageController : RenderController
    {
        private readonly IWeatherService _weatherService;
        private MyCustomAppSettings myCustomAppSettings;

        public HomePageController(ILogger<RenderController> logger,
                                  ICompositeViewEngine compositeViewEngine,
                                  IUmbracoContextAccessor umbracoContextAccessor,
                                  IWeatherService weatherService, 
                                  IOptions<MyCustomAppSettings> options) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _weatherService = weatherService;
            myCustomAppSettings = options.Value;
        }

        public override IActionResult Index()
        {
            var viewModel = (HomePage)CurrentPage;

            var weatherForecast = _weatherService.GetWeatherForecast("Leeds,UK", 3);

            viewModel.WeatherLocation = myCustomAppSettings.LocationName;
            viewModel.WeatherForecast = weatherForecast;

            return CurrentTemplate(viewModel);
        }
    }
}
