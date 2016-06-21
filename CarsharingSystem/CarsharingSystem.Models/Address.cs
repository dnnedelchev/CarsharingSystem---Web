using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace CarsharingSystem.Models
{
    public class Address
    {
        [Column("AddressId")]
        public int Id { get; set; }

        public string FullAddress { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }
        
        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}