
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        private ICollection<Vehicle> vehicles;
        private ICollection<Travel> travelsAsDriver;
        private ICollection<Travel> travelsAsPassenger;
        private ICollection<Comment> comments;

        public User()
        {
            this.vehicles = new HashSet<Vehicle>();
            this.travelsAsDriver = new HashSet<Travel>();
            this.travelsAsPassenger = new HashSet<Travel>();
        }

        public DateTime RegistrationDate { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles
        {
            get
            {
                return this.vehicles;
            }

            set
            {
                this.vehicles = value;
            }
        }


        [InverseProperty("Driver")]
        public virtual ICollection<Travel> TravelsAsDriver { get { return this.travelsAsDriver; } set { this.travelsAsDriver = value; } }

        public virtual ICollection<Travel> TravelsAsPassenger { get { return this.travelsAsPassenger; } set { this.travelsAsPassenger = value; } }

        public virtual ICollection<Comment> Comments { get { return this.comments; } set { this.comments = value; } }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}
