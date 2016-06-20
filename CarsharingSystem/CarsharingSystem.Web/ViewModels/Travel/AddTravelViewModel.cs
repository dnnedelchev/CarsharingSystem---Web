
namespace CarsharingSystem.Web.ViewModels.Travel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AddTravelViewModel
    {
        public DateTime Date { get; set; }

        public string DestinationFrom { get; set; }

        public string DestinationTo { get; set; }

        public int VehicleId { get; set; }

        public IEnumerable<SelectListItem> Vehicles { get; set; }

        public int FreePlaces { get; set; }
    }
}