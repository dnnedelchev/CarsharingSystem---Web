
namespace CarsharingSystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Models;
    using CarsharingSystem.Web.ViewModels.Vehicle;

    public class VehicleController : BaseController
    {
        public VehicleController(ICarsharingData data)
            : base(data)
        {   
        }

        [Authorize]
        public ActionResult All(string id)
        { 
            if (string.IsNullOrWhiteSpace(id) || (this.UserProfile == null))
            {
                throw new UnauthorizedAccessException();
            }
            var userName = string.IsNullOrWhiteSpace(id) ? this.UserProfile.UserName : id;

            return this.View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddVehicleViewModel vehicle)
        {
            if ((vehicle != null) && this.ModelState.IsValid)
            {
                var newVehicle = new Vehicle
                {
                    Label =  vehicle.Label,
                    Model = vehicle.Model,
                    OwnerId = this.UserProfile.Id,
                    ManufactureYear = vehicle.ManufactureYear,
                    VehicleType = (VehicleType)vehicle.VehicleType,
                    Seats = vehicle.Seats
                };
                this.Data.Vehicles.Add(newVehicle);
                this.Data.SaveChanges();

                var action = "All/" + Uri.EscapeDataString(this.UserProfile.UserName);
                return this.RedirectToAction(action, "Vehicle");
            }

            return this.View();
        }
    }
}