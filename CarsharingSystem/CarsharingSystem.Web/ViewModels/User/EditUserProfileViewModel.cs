
namespace CarsharingSystem.Web.ViewModels.User
{

    using System;
    using System.Web;
    using System.Web.Mvc;

    using CarsharingSystem.Web.ViewModels.Common;

    public class EditUserProfileViewModel
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string AboutMe { get; set; }

        public ImageViewModel UserPhoto { get; set; }

        public HttpPostedFileBase NewUserPhoto { get; set; }
    }
}