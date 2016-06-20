
namespace CarsharingSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Web.ViewModels.Travel;

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
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddTravelViewModel travel)
        {
            throw new NotImplementedException();
        }
    }
}