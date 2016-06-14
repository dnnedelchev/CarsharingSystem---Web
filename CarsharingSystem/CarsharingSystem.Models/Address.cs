namespace CarsharingSystem.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string AddresLine1 { get; set; }

        public string AddresLine2 { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }
    }
}