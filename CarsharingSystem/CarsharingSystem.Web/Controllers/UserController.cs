
using System.Linq;
using CarsharingSystem.Web.ViewModels.User;

namespace CarsharingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Web.ViewModels.Common;
    using System;

    public class UserController : BaseController
    {
        public UserController(ICarsharingData data)
            : base(data)
        {
            
        }

        [Authorize]
        public ActionResult Index()
        {
            var userInfo = new UserInfoViewModel
            {
                Username = this.UserProfile.UserName,
                FirstName = this.UserProfile.FirstName,
                TravelCountAsDriver = this.UserProfile.TravelsAsDriver.Count,
                TravelCountAsPassenger = this.UserProfile.TravelsAsPassenger.Count,
                RatingAsDriver = 10,
                RatingAsPassenger = 10,
                CanModify = true
            };

            return this.View("Show", userInfo);
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
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Gender = userDb.Gender.HasValue ? (Gender)userDb.Gender : (Gender?)null,
                DateOfBirth = userDb.DateOfBirth,
                AboutMe = userDb.AboutMe,
                TravelCountAsDriver = userDb.TravelsAsDriver.Count,
                TravelCountAsPassenger = userDb.TravelsAsPassenger.Count,
                RatingAsDriver = 10,
                RatingAsPassenger = 10,
                UserPhoto = (userDb.Image == null) ? null : new ImageViewModel 
                {
                    ImageId = userDb.ImageId.Value,
                    Content = userDb.Image.Content,
                    ContentType = userDb.Image.FileExtension
                },
                CanModify = (userName == this.UserProfile.UserName)
            }; 
            
            return this.View(userInfo);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            var userDb = this.UserProfile;
            var userInfo = new EditUserProfileViewModel
            {
                Username = userDb.UserName,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Gender = (Gender)userDb.Gender,
                DateOfBirth = userDb.DateOfBirth,
                PhoneNumber = userDb.PhoneNumber,
                AboutMe = userDb.AboutMe,
                UserPhoto = (!userDb.ImageId.HasValue) ? null : new ImageViewModel
                {
                    ImageId = userDb.ImageId.Value,
                    Content = userDb.Image.Content,
                    ContentType = userDb.Image.FileExtension
                },
            }; 

            return this.View(userInfo);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserProfileViewModel userInfo)
        {
            throw new NotImplementedException();
        }

    }
}