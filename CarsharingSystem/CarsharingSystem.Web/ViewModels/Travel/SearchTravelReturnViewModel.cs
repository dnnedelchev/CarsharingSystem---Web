
namespace CarsharingSystem.Web.ViewModels.Travel
{
    using System;

    public class SearchTravelReturnViewModel
    {
        public int TravelId { get; set; }

        public string AddressFrom { get; set; }

        public string AddressTo { get; set; }

        public string DriverUsername { get; set; }

        public DateTime TravelDate { get; set; }

        public int DistanceFrom { get; set; }

        public int DistanceTo { get; set; }
    }
}