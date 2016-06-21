
using System.ComponentModel.DataAnnotations;

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

        //TODO: add image of the vehicle.
    }
}