
namespace CarsharingSystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using CarsharingSystem.Data;
    using CarsharingSystem.Models;

    using Microsoft.AspNet.Identity;

    public abstract class BaseController : Controller
    {
        protected ICarsharingData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected BaseController(ICarsharingData data)
        {
            this.Data = data;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var userId = requestContext.HttpContext.User.Identity.GetUserId();
            if (!string.IsNullOrWhiteSpace(userId))
                this.UserProfile = this.Data.Users.Find(userId);
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}