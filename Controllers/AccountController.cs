using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SSM.Models;
using SSM.Services;

namespace SSM.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        public static String USER_SESSION_ID = "USER_SESSION_ID";
        public static String USER_HASH_NEWINFO = "USER_HASH_NEWINFO";
        private UsersServices UsersService1 { get; set; }
        private INewsServices newsServices;
        private User UserProfile
        {
            get
            {
                if (UserProfile == null)
                {
                    User User1 = (User)Session[USER_SESSION_ID];
                    return User1;
                }
                else
                {
                    return UserProfile;
                }
            }
        }
        public bool isAdminOrDirector
        {
            get
            {
                if (UserProfile.RoleName.Equals(UsersModel.Positions.Administrator.ToString())
                    || UserProfile.RoleName.Equals(UsersModel.Positions.Director.ToString()))
                {
                    return true;
                }
                return false;
            }
        }
        public bool isSetPassword
        {
            get
            {
                if (isAdminOrDirector)
                {
                    return true;
                }
                if (UserProfile.SetPass == true)
                {
                    return true;
                }
                return false;
            }
        }
        protected override void Initialize(RequestContext requestContext)
        {
            UsersService1 = new UsersServicesImpl();
            newsServices = new NewsServices();
            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user1 = UsersService1.getUserLogIn(model.UserName, model.Password);
                if (user1 != null)
                {
                    var check = newsServices.CheckAboutNewByUser(user1); 
                    Session[USER_HASH_NEWINFO] = check;
                    if (Request.QueryString["ReturnUrl"] != null)
                    {
                        Session[USER_SESSION_ID] = user1;
                        FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Users", new { id = 0 });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Shipment", new { id = 0 });
        }
    }
}
