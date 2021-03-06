﻿
namespace CarsharingSystem.Web.ViewModels.User
{
    using System;

    using CarsharingSystem.Web.Infrastructure.Mapping;
    using CarsharingSystem.Web.ViewModels.Common;
    using AutoMapper;

    public class UserInfoViewModel : IHaveCustomMappings
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string AboutMe { get; set; }

        public int TravelCountAsDriver { get; set; }

        public int TravelCountAsPassenger { get; set; }

        public decimal RatingAsDriver { get; set; }

        public decimal RatingAsPassenger { get; set; }

        public ImageViewModel UserPhoto { get; set; }

        public bool CanModify { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            
        }

    }
}