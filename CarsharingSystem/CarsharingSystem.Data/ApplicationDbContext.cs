
namespace CarsharingSystem.Data
{
    using System.Data.Entity;

    using CarsharingSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Image> Images{ get; set; }

        public IDbSet<Travel> Travels { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Vehicle> Vehicles { get; set; }

        public IDbSet<Vote> Votes { get; set; }
    }
}
