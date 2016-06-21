
namespace CarsharingSystem.Web.ViewModels.Vehicle
{
    using CarsharingSystem.Web.ViewModels.Common;
    using System.Collections.Generic;

    public class ShowVehicleViewModel
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Model { get; set; }

        public IList<ImageViewModel> ImageGallery { get; set; }

        public string OwnerUsername { get; set; }

        public int Seats { get; set; }

        public int TravelCounts { get; set; }

        public int ManufactureYear { get; set; }
    }
}