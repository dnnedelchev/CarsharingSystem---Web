
namespace CarsharingSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Web.ViewModels.Travel;
    using CarsharingSystem.Models;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using CarsharingSystem.Common.GeocodeAPI;
    using System.Net;

    public class TravelController : BaseController
    {
        public TravelController(ICarsharingData data)
            : base(data)
        {       
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            var addTravelViewModel = new AddTravelViewModel
            {
                Date = DateTime.Now,
                Vehicles = this.Data.Vehicles.All()
                    .Where(vehicle => vehicle.Owner.Id == this.UserProfile.Id)
                    .Select(
                        vehicle =>
                        new SelectListItem()
                            {
                                Value = vehicle.Id.ToString(),
                                Text = vehicle.Label + " " + vehicle.Model
                            })
            };

            return this.View(addTravelViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddTravelViewModel travel)
        {
            Address addresFrom = new Address();
            Address addressTo = new Address();

            var resultAddressFrom = GoogleApi.GetGeographicDataByAddress(travel.DestinationFrom);
            var resultAddressTo = GoogleApi.GetGeographicDataByAddress(travel.DestinationTo);

            if (resultAddressFrom.status == HttpStatusCode.OK.ToString())
            {
                var addressFromJson = resultAddressFrom.results.First();
                var countryName = GoogleApi.GetCountryName(addressFromJson);
                var cityName = GoogleApi.GetCityName(addressFromJson);
                var city = this.Data.Cities.All().Where(c => c.Name.ToLower() == cityName.ToLower()).FirstOrDefault();
                if (city == null)
                {
                    city = new City
                    {
                        Name = cityName
                    };
                    this.Data.Cities.Add(city);
                    this.Data.SaveChanges();
                }

                addresFrom = new Address
                {
                    FullAddress = addressFromJson.formatted_address,
                    CountryId = this.Data.Countries.All().Where(country => country.Name == countryName).FirstOrDefault().Id,
                    CityId = city.Id,
                    Latitude = addressFromJson.geometry.location.lat,
                    Longitude = addressFromJson.geometry.location.lng
                };

            }

            if (resultAddressTo.status == HttpStatusCode.OK.ToString())
            {
                var addressToJson = resultAddressTo.results.First();
                var countryName = GoogleApi.GetCountryName(addressToJson);
                var cityName = GoogleApi.GetCityName(addressToJson);
                var city = this.Data.Cities.All().Where(c => c.Name.ToLower() == cityName.ToLower()).FirstOrDefault();
                if (city == null)
                {
                    city = new City
                    {
                        Name = cityName
                    };
                    this.Data.Cities.Add(city);
                    this.Data.SaveChanges();
                }

                addressTo = new Address
                {
                    FullAddress = addressToJson.formatted_address,
                    CountryId = this.Data.Countries.All().Where(country => country.Name == countryName).FirstOrDefault().Id,
                    CityId = city.Id,
                    Latitude = addressToJson.geometry.location.lat,
                    Longitude = addressToJson.geometry.location.lng
                };

            }

            var travelToBeAdded = new Travel
            {
                DriverId = this.UserProfile.Id,
                VehicleId = travel.VehicleId,
                Status = TravelStatusType.Active,
                TravelDate = travel.Date,
                AddressFrom = addresFrom,
                AddressTo = addressTo
            };

            this.Data.SaveChanges();

            return View();
        }
    }
}