namespace CarsharingSystem.Data
{
    public interface ICarsharingData
    {
        //IRepository Drivers { get; }
        //DrivingLicenseRepository DrivingLicenses { get; }
        //PassengerRepository Passengers { get; }
        //IRepository<TravelPassenger> TravelPassengers { get; }
        //TravelRepository Travels { get; }
        //VehicleRepository Vehicles { get; }

        int SaveChanges();
    }
}