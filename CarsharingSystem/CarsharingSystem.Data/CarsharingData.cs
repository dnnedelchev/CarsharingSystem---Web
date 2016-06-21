namespace CarsharingSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using CarsharingSystem.Data.Repositories;
    using CarsharingSystem.Models;

    public class CarsharingData : ICarsharingData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public CarsharingData()
            : this(new ApplicationDbContext())
        {
        }

        public CarsharingData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        //public IRepository Drivers
        //{
        //    get { return (DriverRepository)this.GetRepository<Driver>(); }
        //}

        //public PassengerRepository Passengers
        //{
        //    get { return (PassengerRepository)this.GetRepository<Passenger>(); }
        //}

        public IRepository<Address> Addresses
        {
            get { return this.GetRepository<Address>(); }
        }

        public IRepository<City> Cities
        {
            get { return this.GetRepository<City>(); }
        }

        public IRepository<Country> Countries
        {
            get { return this.GetRepository<Country>(); }
        }

        public IRepository<Image> Images
        {
            get { return this.GetRepository<Image>(); }
        }

        public IRepository<Travel> Travels
        {
            get { return this.GetRepository<Travel>(); }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Vehicle> Vehicles
        {
            get { return this.GetRepository<Vehicle>(); }
        }

        public IRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                //if (type.IsAssignableFrom(typeof(Vehicle)))
                //{
                //    typeOfRepository = typeof(VehicleRepository);
                //}
                //else if (type.IsAssignableFrom(typeof(Travel)))
                //{
                //    typeOfRepository = typeof(TravelRepository);
                //}
                //else if (type.IsAssignableFrom(typeof(Driver)))
                //{
                //    typeOfRepository = typeof(DriverRepository);
                //}
                //else if (type.IsAssignableFrom(typeof(Passenger)))
                //{
                //    typeOfRepository = typeof(PassengerRepository);
                //}
                //else if (type.IsAssignableFrom(typeof(DrivingLicense)))
                //{
                //    typeOfRepository = typeof(DrivingLicenseRepository);
                //}

                var repository = Activator.CreateInstance(typeOfRepository, this.context);
                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}