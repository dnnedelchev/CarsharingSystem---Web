
namespace CarsharingSystem.Common.GeocodeAPI
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Globalization;

    public static class GoogleApi
    {
        private const string geoCodeUri = "https://maps.googleapis.com/maps/api/geocode/json?";
        private const string languageAddr = "bg";
        
        static GoogleApi()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        }

        public static IEnumerable<CountryInfo> GetAllCountries()
        {
            var url = "https://restcountries.eu/rest/v1/all";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("").Result;
            var countries = response.Content.ReadAsAsync<IEnumerable<CountryInfo>>().Result;

            return countries;
        }

        public static RootObject GetGeographicDataByAddress(string address)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(geoCodeUri);
            var uriParametersFrom = string.Format("?address={0}&language={1}", address, languageAddr);
            var responseFrom = client.GetAsync(uriParametersFrom).Result;
            var resultAddress = responseFrom.Content.ReadAsAsync<RootObject>().Result;

            return resultAddress;
        }

        public static RootObject GetGeographicDataByLocation(decimal lat, decimal lng)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(geoCodeUri);
            var uriParametersFrom = string.Format("?latlng={0},{1}&language={2}", lat, lng, languageAddr);
            var responseFrom = client.GetAsync(uriParametersFrom).Result;
            var resultAddress = responseFrom.Content.ReadAsAsync<RootObject>().Result;

            return resultAddress;
        }

        public static string GetCityName(Result info)
        {
            string result = null;
            var cityName = info.address_components
                .Where(addr => addr.types.Any(type => type == "administrative_area_level_1"))
                .FirstOrDefault();
            if (cityName != null)
            {
                result = cityName.long_name;
            }

            return result;
        }

        public static string GetCountryName(Result info)
        {
            string result = null;
            var countryName = info.address_components
                .FirstOrDefault(addr => addr.types.Any(type => type == "country"));
            if (countryName != null)
            {
                result = countryName.long_name;
            }

            return result;
        }
    }
}
