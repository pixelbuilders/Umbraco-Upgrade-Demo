using MyUmbracoSite.Core.Services.Weather;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace MyUmbracoSite.Core.Composers
{
    public class ServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IWeatherService, WeatherService>();
        }
    }
}
