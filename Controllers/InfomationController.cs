using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Common;

using SSM.Models;
using SSM.Services;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class InfomationController : BaseController
    {
        #region Definetion
        public static String DOCUMENT_PATH = "/FileManager/Document";
        private UsersServices usersServices;
        private IGroupService groupService;
        private INewsServices newsServices;
        private FreightServices freightServices;
        private Grid<NewsModel> _gridInfo;
        private const string INFOMATION_MODEL = "INFOMATION_MODEL";
        private const string INFOMATION_SEARCH_MODEL = "INFOMATION_SEARCH_MODEL";
        private NewSearchModel filter;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            usersServices = new UsersServicesImpl();
            newsServices = new NewsServices();
            freightServices = new FreightServicesImpl();
            groupService = new GroupService();
        }

        #endregion
        #region Information

        public ActionResult Index()
        {
            ViewBag.Title = "List Company Regulation";
            _gridInfo = (Grid<NewsModel>)Session[INFOMATION_MODEL];
            if (_gridInfo == null)
            {
                _gridInfo = new Grid<NewsModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 50,
                                    Sord = "asc",
                                    Sidx = "Header"
                                }
                            );
                _gridInfo.SearchCriteria = new NewsModel();
            }

            filter = filter ?? new NewSearchModel();
            UpdateGridInfomation();
            ViewBag.Categories = newsServices.ListCatelories(NewType.Infomation);
            return View(_gridInfo);
        }
        [HttpPost]
        public ActionResult Index(NewSearchModel model, Grid<NewsModel> gridview)
        {
            _gridInfo = gridview;
            filter = model;
            Session[INFOMATION_MODEL] = _gridInfo;
            Session[INFOMATION_SEARCH_MODEL] = filter;
            _gridInfo.ProcessAction();
            UpdateGridInfomation();
            return PartialView("_ListData", _gridInfo);
        }
        public void UpdateGridInfomation()
        {
            var orderField = new SSM.Services.SortField(_gridInfo.Pager.Sidx, _gridInfo.Pager.Sord == "asc");
            filter.SortField = orderField;
            var page = _gridInfo.Pager;
            //bool checkEdit = CurrenUser.IsEditNew(null) || CurrenUser.AllowRegulationApproval;
            //var grpermission = CurrenUser.UserGroups.Select(x => x.GroupId).ToList();
            var newlist = newsServices.GetScfNewsByUser(CurrenUser);
            newlist = newlist.Where(x => x.Type == (byte) NewType.Infomation
                                         && (string.IsNullOrEmpty(filter.Keyworks) || x.Header.Contains(filter.Keyworks))
                                         && (filter.CategoryId == 0 || x.CateloryId == filter.CategoryId)
                                         && (x.IsApproved != filter.IsPending)
                                         && (filter.GroupId == 0 || x.GroupAccessPermissions.Any(g => g.GroupId == filter.GroupId))
                                    );
            newlist = newlist.OrderBy(orderField);
            var totalRows = (int)newlist.Count();
            ViewBag.SearchingMode = filter ?? new NewSearchModel();
            ViewBag.AllGroup = groupService.GetAll(x => x.IsActive);
            _gridInfo.Pager.Init(totalRows);
            if (totalRows == 0)
            {
                _gridInfo.Data = new List<NewsModel>();
                return;
            }

            var list = newsServices.GetListPager(newlist, page.CurrentPage, page.PageSize);
            var listview = list.Select(x => NewsServices.ToModel(x));
            var viewData = new List<NewsModel>();
            foreach (var it in listview)
            {
                IEnumerable<ServerFile> files = freightServices.getServerFile(it.Id, new SSM.Models.NewsModel().GetType().ToString());
                it.FilesList = files != null ? files.ToList() : new List<ServerFile>();
                viewData.Add(it);
            }
            _gridInfo.Data = viewData;

        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Create Company Regulation";
            var listUser = groupService.GetQuery(x => x.IsActive).ToList();
            ViewBag.AllUSer = listUser;
            var model = new NewsModel();
            model.Type = NewType.Infomation;
            ViewBag.Categories = newsServices.ListCatelories(NewType.Infomation);
            model.NewAccessPermissions = new List<GroupAccessPermission>();
            model.FilesList = new List<ServerFile>();
            ViewBag.AllUSerFee = usersServices.GetAll(x => x.IsActive);
            ViewBag.UserCanupdate = new List<User>();
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            ViewBag.Title = "Create Company Regulation";
            model.CreaterBy = CurrenUser;
            model.DateCreate = DateTime.Now;
            model.DatePromulgate = model.DateCreate;
            model.Type = NewType.Infomation;
            var idNew = newsServices.Created(model);
            model.Id = idNew;
            UploadFile(model, filesUpdoad);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "UPDATE YOUR NEW INFORMATION";
            var model = newsServices.GetNewsModel(id);
            var listUser = groupService.GetQuery(x => x.IsActive).ToList();
            var listUserAccess = model.NewAccessPermissions.Select(x => x.Group).OrderBy(x => x.Name).ToList();
            var userNewList = listUser.Where(x => !listUserAccess.Select(u => u.Id).Contains(x.Id));
            var alluser = usersServices.GetAll(x => x.IsActive);
            ViewBag.AllUSer = userNewList.ToList();
            var userCanupdate = alluser.Where(x => model.ListUserUpdate.Contains(x.Id)).ToList();
            ViewBag.AllUSerFee = alluser.Where(x => !model.ListUserUpdate.Contains(x.Id)).ToList();
            ViewBag.UserCanupdate = userCanupdate;
            ViewBag.Categories = newsServices.ListCatelories(NewType.Infomation);
            ViewBag.UserCanupdate = userCanupdate;
            IEnumerable<ServerFile> files = freightServices.getServerFile(id, new SSM.Models.NewsModel().GetType().ToString());

            model.FilesList = files != null ? files.ToList() : new List<ServerFile>();
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            try
            {
                ViewBag.Title = "UPDATE YOUR NEW INFORMATION";
                model.ModifiedBy = CurrenUser;
                model.DateModify = DateTime.Now;
                model.DatePromulgate = model.DateCreate;
                model.Type = NewType.Infomation;
                newsServices.SaveUpdate(model);
                UploadFile(model, filesUpdoad);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        #region File Action
        private void UploadFile(NewsModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            if (filesUpdoad != null && filesUpdoad.Any())
                foreach (HttpPostedFileBase file in filesUpdoad)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        try
                        {
                            var pathfoder = Path.Combine(Server.MapPath(@"~/" + DOCUMENT_PATH), model.Type.ToString());
                            if (!Directory.Exists(pathfoder))
                            {
                                Directory.CreateDirectory(pathfoder);
                            }
                            string filePath = Path.Combine(pathfoder, Path.GetFileName(file.FileName));
                            file.SaveAs(filePath);
                            //save file to db
                            var fileSave = new ServerFile
                            {
                                ObjectId = model.Id,
                                ObjectType = model.GetType().ToString(),
                                Path = string.Format("{0}/{1}/{2}", DOCUMENT_PATH, model.Type.ToString(), file.FileName),
                                FileName = file.FileName,
                                FileSize = file.ContentLength,
                                FileMimeType = file.ContentType
                            };
                            freightServices.insertServerFile(fileSave);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex);
                        }
                    }

                }
        }
        public ActionResult Download(long id)
        {
            var document = freightServices.getServerFile(id);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = document.FileName,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = true,
            };
            // Response.AppendHeader("Content-Disposition", cd.ToString());
            string filepath = AppDomain.CurrentDomain.BaseDirectory + document.Path;//.Replace("/","\\");
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, document.FileMimeType);
        }
        public JsonResult DeleteFile(long id)
        {
            try
            {
                var file = freightServices.getServerFile(id);
                if (file != null)
                    if (System.IO.File.Exists(Server.MapPath(file.Path)))
                        System.IO.File.Delete(Server.MapPath(file.Path));
                freightServices.deleteServerFile(id);
                return Json(new { isFalse = false, messageErro = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { isFalse = true, messageErro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Approval(int id)
        {
            var dbnew = newsServices.GetNew(id);
            if (!dbnew.IsApproved)
            {
                dbnew.Appvovedby = CurrenUser.Id;
                dbnew.DateApproved = DateTime.Now;
                dbnew.IsApproved = true;
                newsServices.Commited();
                return
                    Json(
                        new
                        {
                            IsApproved = true,
                            messageErro = "Approved successfully",
                            ApproveMess = string.Format("Approved by:{0} at {1}", CurrenUser.FullName, dbnew.DateApproved.Value.ToString("dd/MM/yyyy"))
                        },
                        JsonRequestBehavior.AllowGet);
            }
            else
            {
                dbnew.Appvovedby = (long?)null;
                dbnew.DateApproved = null;
                dbnew.IsApproved = false;
                newsServices.Commited();
                return Json(new { IsApproved = false, messageErro = "DisApproved successfully" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        public ActionResult Detail(int id)
        {
            ViewBag.Title = "DETAIL  Company Regulation";
            var model = newsServices.GetNewsModel(id);
            //var listUser = groupService.GetQuery(x=>x.IsActive).ToList();
            //var listUserAccess = model.NewAccessPermissions.Select(x => x.Group).ToList();
            //var userNewList = listUser.Where(x => !listUserAccess.Select(u => u.Id).Contains(x.Id));
            //ViewBag.AllUSer = userNewList.ToList();
            IEnumerable<ServerFile> files = freightServices.getServerFile(id, new SSM.Models.NewsModel().GetType().ToString());

            model.FilesList = files != null ? files.ToList() : new List<ServerFile>();
            return View(model);
        }
    }
}