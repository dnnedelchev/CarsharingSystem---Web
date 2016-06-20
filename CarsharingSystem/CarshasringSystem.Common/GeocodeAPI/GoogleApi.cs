
namespace CarsharingSystem.Common.GeocodeAPI
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    public static class GoogleApi
    {
        private const string geoCodeUri = "https://maps.googleapis.com/maps/api/geocode/json?";

        public static IEnumerable<CountryInfo> GetAllCountries()
        {
            var url = "https://restcountries.eu/rest/v1/all";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("").Result;
            var countries = response.Content.ReadAsAsync<IEnumerable<CountryInfo>>().Result;

            return countries;
        }

        public static RootObject GetGeographicData(string address)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(geoCodeUri);
            var uriParametersFrom = string.Format("?address={0}", address);
            var responseFrom = client.GetAsync(uriParametersFrom).Result;
            var resultAddress = responseFrom.Content.ReadAsAsync<RootObject>().Result;

            return resultAddress;
        }

    }
}
