
using System.ComponentModel.DataAnnotations;

namespace CarsharingSystem.Web.ViewModels.Travel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AddTravelViewModel
    {
        [Display(Name = "Дата на пътуване:")]
        public DateTime Date { get; set; }

        [Display(Name = "Пътуване от:")]
        public string DestinationFrom { get; set; }

        [Display(Name = "Пътуване за:")]
        public string DestinationTo { get; set; }

        [Display(Name = "Превозно средство:")]
        public int VehicleId { get; set; }

        public IEnumerable<SelectListItem> Vehicles { get; set; }

        [Display(Name = "Свободни места:")]
        public int FreePlaces { get; set; }

    }
}