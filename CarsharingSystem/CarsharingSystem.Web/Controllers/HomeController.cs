
using CarsharingSystem.Web.ViewModels.Vehicle;

namespace CarsharingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using Models;
    using ViewModels.Travel;
    public class HomeController : BaseController
    {
        public HomeController(ICarsharingData data)
            : base(data)
        {
            
                
        }

        public ActionResult Index()
        {
            var travels = this.Data.Travels.All()
                .Where(travel => travel.Status == TravelStatusType.Active)
                .Take(10)
                .OrderBy(travel => travel.TravelDate)
                .Select(travel => new TravelInfoViewModel
                {
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
                .ToList();

            return View(travels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}