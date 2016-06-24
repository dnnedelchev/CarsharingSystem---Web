﻿

namespace CarsharingSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Web.ViewModels.Travel;
    using CarsharingSystem.Models;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using CarsharingSystem.Web.ViewModels.Vehicle;
    using CarsharingSystem.Common.GeocodeAPI;

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

            return View(addTravelViewModel);
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
                AddressToId = addressTo.Id,
                Description = travel.Description
            };

            this.Data.Travels.Add(travelToBeAdded);
            this.Data.SaveChanges();

            return RedirectToAction("Show", new {id = travelToBeAdded.Id});
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
            var city = this.Data.Cities.All().FirstOrDefault(c => c.Name.ToLower() == cityName.ToLower());
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
                    this.Data.Countries
                        .All()
                        .First(country => (country.Name == countryName) || (country.NativeName == countryName))
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

            return View(travelInfo);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult TakePlace(int id)
        {
            var travel = this.Data.Travels.Find(id);
            if (travel == null)
            {
                throw new HttpException(404, "Travel not found");
            }

            if (travel.FreeSpaces < 0)
            {
                throw new InvalidOperationException();
            }

            if (this.UserProfile.Id == travel.DriverId ||
                travel.Passengers.Any(passenger => passenger.Id == this.UserProfile.Id))
            {
                throw new InvalidOperationException();
            }

            travel.FreeSpaces--;
            travel.Passengers.Add(this.UserProfile);
            this.Data.SaveChanges();

            return RedirectToAction("Show", new {id = travel.Id});
        }
    }
}