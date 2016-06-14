namespace CarsharingSystem.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string FileExtension { get; set; }
    }
}