namespace CarsharingSystem.Data
{
    using CarsharingSystem.Data.Repositories;
    using CarsharingSystem.Models;

    public interface ICarsharingData
    {
        IRepository<Address> Addresses { get; }

        IRepository<City> Cities { get; }

        IRepository<Country> Countries { get; }

        IRepository<Image> Images { get; }

        IRepository<Travel> Travels { get; }

        IRepository<User> Users { get; }

        IRepository<Vehicle> Vehicles { get; }

        IRepository<Vote> Votes { get; }

        int SaveChanges();
    }
}