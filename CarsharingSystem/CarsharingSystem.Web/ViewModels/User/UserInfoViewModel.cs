
namespace CarsharingSystem.Web.ViewModels.User
{
    public class UserInfoViewModel
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public int TravelCountAsDriver { get; set; }

        public int TravelCountAsPassenger { get; set; }

        public decimal RatingAsDriver { get; set; }

        public decimal RatingAsPassenger { get; set; }

        public ImageViewModel UserPhoto { get; set; }

        public bool CanModify { get; set; }
    }
}