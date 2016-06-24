using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    public class Image
    {
        [Column("ImageId")]
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }
    }
}