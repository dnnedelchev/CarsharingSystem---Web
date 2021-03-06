﻿
using CarshasringSystem.Common.GeocodeAPI;

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
        private const string GeoCodeUri = "https://maps.googleapis.com/maps/api/geocode/json?";
        private const string distanceMetrix = "https://maps.googleapis.com/maps/api/distancematrix/json?";
        private const string languageAddr = "bg";
        
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
            client.BaseAddress = new Uri(GeoCodeUri);
            var uriParametersFrom = string.Format("?address={0}&language={1}", address, languageAddr);
            var responseFrom = client.GetAsync(uriParametersFrom).Result;
            var resultAddress = responseFrom.Content.ReadAsAsync<RootObject>().Result;

            return resultAddress;
        }

        public static RootObject GetGeographicDataByLocation(decimal lat, decimal lng)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(GeoCodeUri);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var uriParametersFrom = string.Format("?latlng={0},{1}&language={2}", lat, lng, languageAddr);
            var responseFrom = client.GetAsync(uriParametersFrom).Result;
            var resultAddress = responseFrom.Content.ReadAsAsync<RootObject>().Result;

            return resultAddress;
        }

        public static DistanceMatrixResult CalculateDistance(decimal latFrom, decimal lngFrom, decimal latTo, decimal lngTo)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(distanceMetrix);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var uriParameters = string.Format("?units={0}&language={1}&origins={2},{3}&destinations={4},{5}", "metrix", languageAddr, latFrom, lngFrom, latTo, lngTo);
            var response = client.GetAsync(uriParameters).Result;
            var result = response.Content.ReadAsAsync<DistanceMatrixResult>().Result;

            return result;
        }

        private static string GetAddressValue(Result info, string typeParam)
        {
            string result = null;
            var cityName = info.address_components
                .Where(addr => addr.types.Any(type => type == typeParam))
                .FirstOrDefault();
            if (cityName != null)
            {
                result = cityName.long_name;
            }

            return result;
        }

        public static string GetCityName(Result info)
        {
            var type = "administrative_area_level_1";
            return GetAddressValue(info, type);
        }

        public static string GetCountryName(Result info)
        {
            var type = "country";
            return GetAddressValue(info, type);
        }

        public static object GetStreetName(Result info)
        {
            var type = "route";
            return GetAddressValue(info, type);
        }

        public static object GetNeighborhoodName(Result info)
        {
            var type = "neighborhood";
            return GetAddressValue(info, type);
        }

        public static object GetStreetNumber(Result info)
        {
            var type = "street_number";
            return GetAddressValue(info, type);
        }

        public static object GetPremiseNumber(Result info)
        {
            var type = "premise";
            return GetAddressValue(info, type);
        }
    }
}
