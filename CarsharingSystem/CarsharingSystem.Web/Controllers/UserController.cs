
using System.Linq;
using CarsharingSystem.Web.ViewModels.User;

namespace CarsharingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using CarsharingSystem.Data;

    public class UserController : BaseController
    {
        public UserController(ICarsharingData data)
            : base(data)
        {
            
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Show(string id)
        {
            var userName = string.IsNullOrWhiteSpace(id) ? this.UserProfile.UserName : id;
            var userDb = this.Data.Users.All().First(user => user.UserName == userName);

            var userInfo = new UserInfoViewModel
            {
                Username = userName,
                Name = userDb.Name,
                TravelCountAsDriver = userDb.Travels.Count, //TODO: witch travels?
                TravelCountAsPassenger = userDb.Travels.Count, //TODO: witch travels?
                RatingAsDriver = 10,
                RatingAsPassenger = 10,
                CanModify = (userName == this.UserProfile.UserName)
            }; 
            
            return this.View(userInfo);
        }
    }
}