using MyUmbracoSite.Core.Services.Weather;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MyUmbracoSite.Core.Configuration;

namespace MyUmbracoSite.Core.Composers
{
    public class ServicesComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IWeatherService, WeatherService>();
            builder.Services.Configure<MyCustomAppSettings>(builder.Config.GetSection("Tomorrow.io"));
        }
    }
}
