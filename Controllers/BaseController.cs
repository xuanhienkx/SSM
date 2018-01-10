using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Common;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels;

namespace SSM.Controllers
{

    public abstract class BaseController : Controller
    {
        private User currentUser;
        private HttpCookie cookie;
        private const string PAGE_SIZE_APP = "PAGE_SIZE_APP";

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            currentUser = (User)Session[AccountController.USER_SESSION_ID];
        }

        protected User CurrenUser => currentUser ?? (User)Session[AccountController.USER_SESSION_ID];

        protected ActionResult NotPermistion()
        {
            return View("ErrorPermistion");
        }

        protected SelectList CarrierTypes
        {
            get
            {
                var carrierType = from CarrierType ct in Enum.GetValues(typeof(CarrierType))
                                  select new { Id = ct, Name = ct.ToString() };
                return new SelectList(carrierType, "Id", "Name");
            }

        }

        protected SelectList ServiceTypes
        {
            get
            {
                var Services = from ShipmentModel.ServicesType Sv in Enum.GetValues(typeof(ShipmentModel.ServicesType))
                               select new { Id = Sv, Name = Sv.ToString() };
                return new SelectList(Services, "Id", "Name");
            }

        }

        protected HttpCookie Cookie
        {
            get
            {
                return cookie = Request.Cookies[PAGE_SIZE_APP];
            }
        }

        protected List<long> UListAvilibelUserId()
        {
            var listIdUser = new List<long> {CurrenUser.Id};
            if (UsersModel.isAdminNComDirctor(CurrenUser))
            {
                listIdUser = new List<long>();
            }
            else if (UsersModel.isDeptManager(CurrenUser))
            {
                listIdUser.AddRange(CurrenUser.Department.Users.Select(_user => _user.Id));
            }
            return listIdUser;
        }

        protected void SetCookiePager(int pageSize, string actionPage)
        {
            HttpCookie nCookie = Cookie ?? new HttpCookie(PAGE_SIZE_APP);
            nCookie[actionPage] = $"{pageSize}";
            nCookie["LastVisit"] = DateTime.Now.ToString("F");
            nCookie.Expires = DateTime.Now.AddYears(1);
            if (Cookie == null)
                Response.SetCookie(nCookie);
            else
                Response.Cookies.Add(nCookie);
        }
     /*   public ActionResult SimpleView(IResult result, Func<ActionResult> viewRender)
        {
            if (result != null && result.IsSuccess == false)
            {
                return Json(new
                {
                    success = result.IsSuccess,
                    value = result.ErrorResults
                });
            }
            return viewRender();

        }
*/
       
        public ActionResult JsonView(Func<object> viewRender, Func<object> otherData = null)
        {
            return Json(new
            {
                success = true,
                value = viewRender(),
                other = otherData != null ? otherData() : null
            });
        }

         public JsonResult JsonResult(object dataView = null, bool dialog = false)
         {
             return new JsonResult()
             {
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 Data = new
                 {
                     success = true,
                     value = dataView,
                     dialog = dialog
                 },
             };
         }
         public JsonResult JsonResult(IResult result, object dataView = null, bool dialog = false)
         {
             if (result != null && result.IsSuccess == false)
             {
                 return Json(new
                 {
                     success = result.IsSuccess,
                     value = result.ErrorResults,
                     dialog = dialog
                 }, JsonRequestBehavior.AllowGet);
             }
             return new JsonResult()
             {
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 Data = new
                 {
                     success = true,
                     value = dataView,
                     dialog = dialog
                 },
             };
         }
        public void SetNotification(string message, NotificationType notifyType, bool autoHideNotification = true)
        {
            this.TempData["Notification"] = message;
            this.TempData["NotificationAutoHide"] = (autoHideNotification) ? "true" : "false";

            switch (notifyType)
            {
                case NotificationType.Success:
                    this.TempData["NotificationCSS"] = "notificationbox nb-success";
                    break;
                case NotificationType.Error:
                    this.TempData["NotificationCSS"] = "notificationbox nb-error";
                    break;
                case NotificationType.Warning:
                    this.TempData["NotificationCSS"] = "notificationbox nb-warning";
                    break;
            }
        }
    }
}