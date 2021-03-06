﻿using System.ComponentModel.DataAnnotations.Schema;

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

        [Column("TravelId")]
        public int Id { get; set; }

        public string Label { get; set; }

        public string Model { get; set; }
        
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int ManufactureYear { get; set; }

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