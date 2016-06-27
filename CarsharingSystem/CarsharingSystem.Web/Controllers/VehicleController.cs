
namespace CarsharingSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Models;
    using CarsharingSystem.Web.ViewModels.Vehicle;
    using CarsharingSystem.Web.ViewModels.Travel;
    using System.Collections.Generic;
    using System.Web;
    using CarsharingSystem.Web.ViewModels.Common;

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

        public ActionResult Show(int id)
        {
            var vehicle = this.Data.Vehicles.Find(id);
            if (vehicle == null)
            {
                throw new HttpException(404, "Vehicle was not found");
            }

            var vehicleInfo = new ShowVehicleViewModel
            {
                Label = vehicle.Label,
                Model = vehicle.Model,
                ImageGallery = vehicle.Images.Select(image => new ImageViewModel 
                { 
                    ImageId = image.Id,
                    Content = image.Content,
                    ContentType = image.ContentType
                }).ToList(),
                OwnerUsername = vehicle.Owner.UserName,
                Seats = vehicle.Seats,
                TravelCounts = vehicle.Travels.Count(),
                ManufactureYear = vehicle.ManufactureYear
            };

            return this.View(vehicleInfo);
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
            var images = new List<Image>(); 

            foreach (var image in vehicle.Images)
            {
                if (image == null) continue;

                MemoryStream memoryStream = image.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    image.InputStream.CopyTo(memoryStream);
                }
                var newImage = new Image
                {
                    Content = memoryStream.ToArray(),
                    ContentType = image.ContentType
                };
                this.Data.Images.Add(newImage);
                images.Add(newImage);
            }
            if ((vehicle != null) && this.ModelState.IsValid)
            {
                var newVehicle = new Vehicle
                {
                    Label =  vehicle.Label,
                    Model = vehicle.Model,
                    OwnerId = this.UserProfile.Id,
                    ManufactureYear = vehicle.ManufactureYear,
                    VehicleType = (VehicleType)vehicle.VehicleType,
                    Seats = vehicle.Seats,
                    Images = images
                };
                this.Data.Vehicles.Add(newVehicle);
                this.Data.SaveChanges();

                var action = string.Format(@"All/{0}/", Uri.EscapeDataString(this.UserProfile.UserName));
                return this.RedirectToAction(action, "Vehicle");
            }

            return this.View();
        }

        public ActionResult GetVehiclesByUser(string userName)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == userName);
            if (user == null) 
            {
                throw new InvalidOperationException();
            }

            var vehicles = user.Vehicles.Select(vehicle => new ShowVehicleViewModel
                {
                    Id = vehicle.Id,
                    Label = vehicle.Label,
                    Model = vehicle.Model,
                    Seats = vehicle.Seats,
                    TravelCounts = vehicle.Travels.Count,
                    ManufactureYear = vehicle.ManufactureYear
                })
                .ToList();

            return this.View("_VehiclesInfoPartial", vehicles);
        }
    }
}