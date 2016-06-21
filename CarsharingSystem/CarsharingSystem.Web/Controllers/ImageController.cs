
namespace CarsharingSystem.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web;
    using System.IO;
    using CarsharingSystem.Data;

    public class ImageController : BaseController
    {
        public ImageController(ICarsharingData data)
            : base(data)
        {

        }

        public ActionResult Get(int id)
        {
            var image = this.Data.Images.Find(id);

            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, image.FileExtension);
        }
    }
}