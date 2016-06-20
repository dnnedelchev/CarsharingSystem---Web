
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
            
            var resultAddressFrom = GoogleApi.GetGeographicData(travel.DestinationFrom);
            var resultAddressTo = GoogleApi.GetGeographicData(travel.DestinationTo);

            if (resultAddressFrom.status == HttpStatusCode.OK.ToString())
            {
                var addressFromJson = resultAddressFrom.results.First();
                var countryName = addressFromJson.address_components.Where(addr => addr.types.Any(type => type == "country")).FirstOrDefault().long_name;
                var addresFrom = new Address
                {
                    FullAddress = addressFromJson.formatted_address,
                    CountryId = this.Data.Countries.All().Where(country => country.Name == countryName).FirstOrDefault().Id
                };

            }
           
            var travelToBeAdded = new Travel
            {
                DriverId = this.UserProfile.Id,
                VehicleId = travel.VehicleId,
                Status = TravelStatusType.Active,
                TravelDate = travel.Date

            };


            return View();
        }
    }
}