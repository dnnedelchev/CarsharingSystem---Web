using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Travel
    {
        private ICollection<User> passengers;
        private ICollection<Vote> votes;
        private ICollection<Comment> comments;

        public Travel()
        {
            this.passengers = new HashSet<User>();
            this.votes = new HashSet<Vote>();
            this.comments = new HashSet<Comment>();
        }

        [Column("TravelId")]
        public int Id { get; set; }

        public string DriverId { get; set; }

        [ForeignKey("DriverId")]
        public virtual User Driver { get; set; }

        public virtual ICollection<User> Passengers { get { return this.passengers; } set { this.passengers = value; } }
         
        public virtual ICollection<Vote> Vote
        {
            get
            {
                return this.votes;
            }

            set
            {
                this.votes = value;
            }
        }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public TravelStatusType Status { get; set; }
        
        public DateTime TravelDate { get; set; }

        public int FreeSpaces { get; set; }

        public int? AddressFromId { get; set; }

        [ForeignKey("AddressFromId")]
        public virtual Address AddressFrom { get; set; }

        public int? AddressToId { get; set; }

        [ForeignKey("AddressToId")]
        public virtual Address AddressTo { get; set; }

        //TODO: ADD a set of mid addresses.

        public virtual ICollection<Comment> Comments { get { return this.comments; } set { this.comments = value; } }
    }
}