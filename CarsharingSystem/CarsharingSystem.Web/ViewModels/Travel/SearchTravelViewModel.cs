
namespace CarsharingSystem.Web.ViewModels.Travel
{
    using System;

    using CarsharingSystem.Web.ViewModels.Common;

    public class SearchTravelViewModel
    {
        public string AddressFrom { get; set; }

        public DistanceEnum MaxPerimeterFrom { get; set; }

        public string AddressTo { get; set; }

        public DistanceEnum MaxPerimeterTo { get; set; }

        public DateTime TravelDateTime { get; set; }

        public DateTime? EndLimitTravelDate { get; set; }
    }
}