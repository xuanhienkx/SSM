using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using SSM.Models;
using SSM.Services;

namespace SSM.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public static String FREIGHT_PATH = "/FileManager/Freight";
        private UsersServices UsersServices1 { get; set; }
        private FreightServices _freightService;
        private User User1;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            User1 = (User)Session[AccountController.USER_SESSION_ID];
            UsersServices1 = new UsersServicesImpl();
            _freightService = new FreightServicesImpl();
           
        }
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to SSM System";

            return View();
        }

        public ActionResult About()
        {
            CompanyInfo CompanyInfo1 = new CompanyInfo();
            CompanyInfo1.CompanyLogo = UsersServices1.getComSetting(CompanyInfo.COMPANY_LOGO);
            CompanyInfo1.CompanyHeader = UsersServices1.getComSetting(CompanyInfo.COMPANY_HEADER);
            CompanyInfo1.CompanyFooter = UsersServices1.getComSetting(CompanyInfo.COMPANY_FOOTER);
            CompanyInfo1.AccountInfor = UsersServices1.getComSetting(CompanyInfo.ACCOUNT_INFO);
            return View(CompanyInfo1);
        }
        private ServerFile getServerFiles(long ObjectId, String ObjectType)
        {
            IEnumerable<ServerFile> ServerFiles = _freightService.getServerFile(ObjectId, ObjectType);
            if (ServerFiles != null && ServerFiles.Count() > 0) {
                return ServerFiles.First();
            }
            return null;
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult About(CompanyInfo CompanyInfo1)
        {
            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/" + FREIGHT_PATH)
                            , Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);
                    UsersServices1.UpdateComSetting(CompanyInfo.COMPANY_LOGO, FREIGHT_PATH + "/" + file.FileName);
                    CompanyInfo1.CompanyLogo = FREIGHT_PATH + "/" + file.FileName;
                    Setting Setting1 = UsersServices1.getComLogoSetting(CompanyInfo.COMPANY_LOGO);
                    long ObjectId = Setting1 != null ? Setting1.Id : 0;
                    //save file to db
                    ServerFile ServerFile1 = getServerFiles(ObjectId, "SSM.Models.Setting");
                    
                    if (ServerFile1 != null)
                    {
                        ServerFile1 = _freightService.getServerFile(ServerFile1.Id);
                        ServerFile1.Path = FREIGHT_PATH + "/" + file.FileName;
                        ServerFile1.FileName = file.FileName;
                        ServerFile1.FileSize = file.ContentLength;
                        ServerFile1.FileMimeType = file.ContentType;
                        UsersServices1.updateAny();
                    }
                    else
                    {
                        ServerFile1 = new ServerFile();
                        ServerFile1.ObjectId = ObjectId;
                        ServerFile1.ObjectType = "SSM.Models.Setting";
                        ServerFile1.Path = FREIGHT_PATH + "/" + file.FileName;
                        ServerFile1.FileName = file.FileName;
                        ServerFile1.FileSize = file.ContentLength;
                        ServerFile1.FileMimeType = file.ContentType;
                        _freightService.insertServerFile(ServerFile1);
                    }
                }
            }
            
            if (CompanyInfo1.CompanyHeader != null && !CompanyInfo1.CompanyHeader.Equals(""))
            {
                UsersServices1.UpdateComSetting(CompanyInfo.COMPANY_HEADER, CompanyInfo1.CompanyHeader);
            }
            if (CompanyInfo1.CompanyFooter != null && !CompanyInfo1.CompanyFooter.Equals(""))
            {
                UsersServices1.UpdateComSetting(CompanyInfo.COMPANY_FOOTER, CompanyInfo1.CompanyFooter);
            }
            if (CompanyInfo1.AccountInfor != null && !CompanyInfo1.AccountInfor.Equals(""))
            {
                UsersServices1.UpdateComSetting(CompanyInfo.ACCOUNT_INFO, CompanyInfo1.AccountInfor);
            }
            return View(CompanyInfo1);
        }
        [AllowAnonymous]
        public ActionResult NoPermission()
        {
            return View("ErrorPermistion");
        }
    }
}
