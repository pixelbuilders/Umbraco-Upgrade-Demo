using System.Configuration;

namespace MyUmbracoSite.Core.Configuration
{
    public class MyCustomAppSettings
    {
        public MyCustomAppSettings()
        {
            TomorrowIoApiKey = ConfigurationManager.AppSettings["Tomorrow.io.ApiKey"];
            TomorrowIoLocationName = ConfigurationManager.AppSettings["Tomorrow.io.Location.Name"];
            TomorrowIoLocationLatitude = ConfigurationManager.AppSettings["Tomorrow.io.Location.Latitude"];
            TomorrowIoLocationLongitude = ConfigurationManager.AppSettings["Tomorrow.io.Location.Longitude"];
        }

        public string TomorrowIoApiKey { get; set; }
        public string TomorrowIoLocationName { get; set; }
        public string TomorrowIoLocationLatitude { get; set; }
        public string TomorrowIoLocationLongitude { get; set; }
    }
}
