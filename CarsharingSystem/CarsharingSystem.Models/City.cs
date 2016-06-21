using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    public class City
    {
        [Column("CityId")]
        public int Id { get; set; }

        public string Name { get; set; }
        
    }
}