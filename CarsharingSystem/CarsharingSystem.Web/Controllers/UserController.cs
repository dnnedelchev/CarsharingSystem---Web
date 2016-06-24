
using System.Linq;
using CarsharingSystem.Web.ViewModels.User;

namespace CarsharingSystem.Web.Controllers
{
    using System.Web.Mvc;

    using CarsharingSystem.Data;
    using CarsharingSystem.Web.ViewModels.Common;
    using System;
    using System.IO;

    public class UserController : BaseController
    {
        public UserController(ICarsharingData data)
            : base(data)
        {
            
        }

        [Authorize]
        public ActionResult Index()
        {
            // TODO: Func fix see https://github.com/TelerikAcademy/ASP.NET-MVC/tree/661fab78cab7a0f6a3e69a44735a41817a9b4f18/03.%20AJAX%20with%20ASP.NET%20MVC
            // BookViewModel
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
                    ContentType = userDb.Image.ContentType
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
                Gender = userDb.Gender.HasValue ? (Gender)userDb.Gender : (Gender?)null,
                DateOfBirth = userDb.DateOfBirth,
                PhoneNumber = userDb.PhoneNumber,
                AboutMe = userDb.AboutMe,
                UserPhoto = (!userDb.ImageId.HasValue) ? null : new ImageViewModel
                {
                    ImageId = userDb.ImageId.Value,
                    Content = userDb.Image.Content,
                    ContentType = userDb.Image.ContentType
                },
            }; 

            return this.View(userInfo);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserProfileViewModel userInfo)
        {
            var user = this.UserProfile;

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Gender = (CarsharingSystem.Models.Gender)userInfo.Gender;
            user.DateOfBirth = user.DateOfBirth;
            user.PhoneNumber = user.PhoneNumber;
            user.AboutMe = user.AboutMe;

            if (userInfo.NewUserPhoto != null)
            {
                if (user.Image == null)
                {
                    var newImage = new Models.Image();
                    user.Image = newImage;
                    this.Data.Images.Add(newImage);
                    //this.Data.SaveChanges();
                }
                MemoryStream memoryStream = userInfo.NewUserPhoto.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    userInfo.NewUserPhoto.InputStream.CopyTo(memoryStream);
                }
                user.Image.Content = memoryStream.ToArray();
                user.Image.ContentType = userInfo.NewUserPhoto.ContentType;
            }


            this.Data.Users.Update(user);
            this.Data.SaveChanges();
            return this.RedirectToAction("Show", new { id = user.UserName });
        }

    }
}