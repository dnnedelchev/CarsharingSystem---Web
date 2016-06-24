
using System;
using CarsharingSystem.Data;

namespace CarsharingSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class PassengerController : BaseController
    {
        public PassengerController(ICarsharingData data) 
            : base(data)
        {       
        }

        // GET: Passenger
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll(int id)
        {
            throw  new NotImplementedException();
        }
    }
}