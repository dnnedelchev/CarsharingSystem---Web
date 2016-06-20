
namespace CarsharingSystem.Web.ViewModels.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    public enum VehicleTypeViewModel
    {
        [Display(Name = "First Value - desc..")]
        Sedan,
        Crossover,
        SUV,
        Convertible,
        Pickup,
        Coupe,
        Van,
        Hatchback
    }
}