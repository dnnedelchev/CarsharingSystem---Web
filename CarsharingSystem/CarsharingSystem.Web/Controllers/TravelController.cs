
using CarsharingSystem.Web.ViewModels.Vehicle;

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
                //Date = DateTime.Now,
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
            var resultAddressFrom = GoogleApi.GetGeographicDataByAddress(travel.DestinationFrom);
            var resultAddressTo = GoogleApi.GetGeographicDataByAddress(travel.DestinationTo);

            if (resultAddressFrom.status != HttpStatusCode.OK.ToString())
            {
                throw new InvalidOperationException();
            }
            var addresFrom = CreateNewAddress(resultAddressFrom);
            this.Data.Addresses.Add(addresFrom);
            this.Data.SaveChanges();

            if (resultAddressTo.status != HttpStatusCode.OK.ToString())
            {
                throw new InvalidOperationException();
            }
            var addressTo = CreateNewAddress(resultAddressTo);
            this.Data.Addresses.Add(addressTo);
            this.Data.SaveChanges();

            var travelToBeAdded = new Travel
            {
                DriverId = this.UserProfile.Id,
                VehicleId = travel.VehicleId,
                Status = TravelStatusType.Active,
                TravelDate = travel.Date,
                FreeSpaces = travel.FreePlaces,
                AddressFromId = addresFrom.Id,
                AddressToId = addressTo.Id
            };

            this.Data.Travels.Add(travelToBeAdded);
            this.Data.SaveChanges();

            return this.RedirectToAction("Show", new {id = travelToBeAdded.Id});
        }

        [HttpGet]
        public JsonResult ParseAddress(string address)
        {
            var result = GoogleApi.GetGeographicDataByAddress(address);
            var fullAddress = result.results.First();

            var json = new
            {
                Address = fullAddress.formatted_address
            };

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private Address CreateNewAddress(RootObject resultAddress)
        {
            var addressFromJson = resultAddress.results.First();
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

            var addresFrom = new Address
            {
                FullAddress = addressFromJson.formatted_address,
                CountryId =
                    this.Data.Countries.All()
                        .Where(country => (country.Name == countryName) || (country.NativeName == countryName))
                        .First()
                        .Id,
                CityId = city.Id,
                Latitude = addressFromJson.geometry.location.lat,
                Longitude = addressFromJson.geometry.location.lng
            };
            return addresFrom;
        }

        public ActionResult Show(int id)
        {
            var travelInfo = this.Data.Travels.All()
                .Where(travel => travel.Id == id)
                .Select(travel => new TravelInfoViewModel
                {
                    TravelId = travel.Id,
                    DriverUserName = travel.Driver.UserName,
                    TravelDate = travel.TravelDate,
                    LocationFrom = new LocationInfoViewModel
                    {
                        Latitude = travel.AddressFrom.Latitude,
                        Longitude = travel.AddressFrom.Longitude
                    },
                    LocationTo = new LocationInfoViewModel
                    {
                        Latitude = travel.AddressTo.Latitude,
                        Longitude = travel.AddressTo.Longitude
                    },
                    FreeSpaces = travel.FreeSpaces,
                    Vehicle = new ShowVehicleViewModel
                    {
                        Id = travel.VehicleId,
                        Label = travel.Vehicle.Label,
                        Model = travel.Vehicle.Model
                    }
                })
                .FirstOrDefault();

            if (travelInfo == null)
            {
                throw new InvalidOperationException();
            }

            return this.View(travelInfo);
        }
    }
}