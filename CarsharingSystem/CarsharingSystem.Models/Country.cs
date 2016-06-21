using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    public class Country
    {
        [Column("CountryId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string NativeName { get; set; }
    }
}