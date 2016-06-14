namespace CarsharingSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Vehicle
    {
        private ICollection<Image> images;
        private ICollection<Travel> travels;

        public Vehicle()
        {
            this.images = new HashSet<Image>();
            this.travels = new HashSet<Travel>();
        }

        public int Id { get; set; }

        public string DriverId { get; set; }

        public virtual User Owner { get; set; }

        public DateTime ManufactureDate { get; set; }

        public VehicleType VehicleType { get; set; }

        public int Seats { get; set; }

        public virtual ICollection<Image> Images
        {
            get
            {
                return this.images;
            }

            set
            {
                this.images = value;
            }
        }

        public virtual ICollection<Travel> Travels
        {
            get
            {
                return this.travels;
            }

            set
            {
                this.travels = value;
            }
        }
    }
}