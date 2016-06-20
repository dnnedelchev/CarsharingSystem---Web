
namespace CarsharingSystem.Web.ViewModels.Vehicle
{
    public class AddVehicleViewModel
    {
        public string Label { get; set; }

        public string Model { get; set; }

        public int ManufactureYear { get; set; }

        public int Seats { get; set; }

        public VehicleTypeViewModel VehicleType { get; set; }

        //TODO: add image of the vehicle.
    }
}