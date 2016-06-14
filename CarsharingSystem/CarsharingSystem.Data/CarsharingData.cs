namespace CarsharingSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using CarsharingSystem.Data.Repositories;

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

        //public TravelRepository Travels
        //{
        //    get { return (TravelRepository)this.GetRepository<Travel>(); }
        //}

        //public VehicleRepository Vehicles
        //{
        //    get { return (VehicleRepository)this.GetRepository<Vehicle>(); }
        //}

        //public DrivingLicenseRepository DrivingLicenses
        //{
        //    get { return (DrivingLicenseRepository)this.GetRepository<DrivingLicense>(); }
        //}

        //public IRepository<TravelPassenger> TravelPassengers
        //{
        //    get { return this.GetRepository<TravelPassenger>(); }
        //}

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