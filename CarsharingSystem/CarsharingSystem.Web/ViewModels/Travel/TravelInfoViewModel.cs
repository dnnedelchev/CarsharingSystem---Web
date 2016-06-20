
namespace CarsharingSystem.Web.ViewModels.Travel
{
    using System;
    using CarsharingSystem.Web.ViewModels.Vehicle;

    public class TravelInfoViewModel
    {
        public string DriverUserName { get; set; }

        public string DriverName { get; set; }

        public DateTime TravelDate { get; set; }

        public LocationInfoViewModel LocationFrom { get; set; }

        public LocationInfoViewModel LocationTo { get; set; }

        public int FreeSpaces { get; set; }

        public ShowVehicleViewModel Vehicle { get; set; }
    }
}