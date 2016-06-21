
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CarsharingSystem.Web.ViewModels.Vehicle
{
    public class AddVehicleViewModel
    {
        [Display(Name = "Марка:")]
        public string Label { get; set; }

        [Display(Name = "Модел:")]
        public string Model { get; set; }

        [Display(Name = "Година на производство:")]
        public int ManufactureYear { get; set; }

        [Display(Name = "Места:")]
        public int Seats { get; set; }

        [Display(Name = "Тип:")]
        public VehicleTypeViewModel VehicleType { get; set; }

        [Display(Name = "Снимки:")]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }

        //TODO: add image of the vehicle.
    }
}