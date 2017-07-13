using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class OutNewController : BaseController
    {
        #region Definetion
        public static String DOCUMENT_PATH = "/FileManager/Document";
        private UsersServices usersServices;
        private IGroupService groupService;
        private INewsServices newsServices;
        private FreightServices freightServices;
        private Grid<NewsModel> _grid;
        private const string NEW_SEARCH_MODEL = "NEW_SEARCH_MODEL";
        private const string INFOMATION_SEARCH_MODEL = "INFOMATION_SEARCH_MODEL";
        private NewSearchModel filterInformationModel;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            usersServices = new UsersServicesImpl();
            newsServices = new NewsServices();
            freightServices = new FreightServicesImpl();
            groupService = new GroupService();
            Session[AccountController.USER_HASH_NEWINFO] = false;

        }
        #endregion
        #region Company Regulation
        public ActionResult Index()
        {
            _grid = (Grid<NewsModel>)Session[NEW_SEARCH_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<NewsModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 50,
                                    Sord = "asc",
                                    Sidx = "Header"
                                }
                            );
                _grid.SearchCriteria = new NewsModel();
            }

            UpdateGrid(string.Empty);
            return View(_grid);

        }
        public ActionResult List(string newType)
        {
            LoadTitle(newType);
            _grid = (Grid<NewsModel>)Session[NEW_SEARCH_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<NewsModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 50,
                                    Sord = "asc",
                                    Sidx = "Header"
                                }
                            );
                _grid.SearchCriteria = new NewsModel();
            }

            UpdateGrid(newType);
            return PartialView("_ListNewData", _grid);

        }

        public void LoadTitle(string newType)
        {
            var title = "List INFORMATION";
            if (newType == "NEW")
            {
                title += " - New";
            }
            else if (newType == "Info")
            {
                title += " - Info";
            }
            ViewBag.Title = title;
        }
        [HttpPost]
        public ActionResult List(string newType, Grid<NewsModel> grid)
        {
            LoadTitle(newType);
            _grid = grid;
            Session[NEW_SEARCH_MODEL] = _grid;
            UpdateGrid(newType);
            return PartialView("_ListNewData", _grid);

        }

        public void UpdateGrid(string newType)
        {
            var page = _grid.Pager;
            var newlist = newsServices.GetScfNewsByUser(CurrenUser)
                .Where(x => x.Type == (byte) NewType.News
                            && (string.IsNullOrEmpty(newType) || x.Catelory.NameType == newType));
            newlist =
                newlist.Where(
                    x =>
                        string.IsNullOrEmpty(_grid.SearchCriteria.Header) ||
                        x.Header.Contains(_grid.SearchCriteria.Header));

            var totalRows = (int)newlist.Count();
            _grid.Pager.Init(totalRows);
            if (totalRows == 0)
            {
                _grid.Data = new List<NewsModel>();
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
            _grid.Data = viewData;
        }
        [HttpGet]
        public ActionResult GetTopNew(string newType, int? top = 10)
        {
            LoadTitle(newType);
            ViewBag.ViewType = newType;
            int getTop = top ?? 10;
            var newlist = newsServices.GetScfNewsByUser(CurrenUser)
                .Where(x => x.Type == (byte) NewType.News && x.Catelory.NameType == newType)
                .OrderByDescending(x => x.DateCreate)
                .Take(getTop).ToList();
            return PartialView("_NewTopView", newlist);
        }
        [HttpGet]
        public ActionResult CreateNews()
        {
            ViewBag.Title = "Create INFORMATION";
            var listUser = groupService.GetQuery(x => x.IsActive).ToList();
            ViewBag.AllUSer = listUser;
            var model = new NewsModel();
            ViewBag.Categories = newsServices.ListCatelories(NewType.News);
            model.NewAccessPermissions = new List<GroupAccessPermission>();
            model.FilesList = new List<ServerFile>();
            model.Type = NewType.News;
            ViewBag.AllUSerFee = usersServices.GetAll(x => x.IsActive);
            ViewBag.UserCanupdate = new List<User>();
            return PartialView("_CreateNewInfo", model);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNews(NewsModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            ViewBag.Title = "Ceate INFORMATION";
            model.CreaterBy = CurrenUser;
            model.DateCreate = DateTime.Now;
            model.Type = NewType.News;
            var idNew = newsServices.Created(model);
            model.Id = idNew;
            UploadFile(model, filesUpdoad);
            return List(string.Empty);
        }

        public ActionResult ViewDetail(int id)
        {
            var data = newsServices.GetNewsModel(id);
            var check = data.UserView != null &&
                       data.UserView.Contains(string.Format(";{0};", CurrenUser.Id));
            if (!check)
            {
                data.UserView = newsServices.SetIsViewed(CurrenUser, id);
            }
            IEnumerable<ServerFile> files = freightServices.getServerFile(id, new SSM.Models.NewsModel().GetType().ToString());
            data.FilesList = files != null ? files.ToList() : new List<ServerFile>();
            return PartialView("_DetailView", data);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "UPDATE YOUR NEW INFORMATION";
            var model = newsServices.GetNewsModel(id);
            var check = model.UserView != null &&
                        model.UserView.Contains(string.Format(";{0};", CurrenUser.Id));
            if (!check)
            {
                model.UserView = newsServices.SetIsViewed(CurrenUser, id);
            }
            var listUser = groupService.GetQuery(x => x.IsActive).ToList();
            var listUserAccess = model.NewAccessPermissions.Select(x => x.Group).ToList();
            var userNewList = listUser.Where(x => !listUserAccess.Select(u => u.Id).Contains(x.Id));
            var alluser = usersServices.GetAll(x => x.IsActive);
            ViewBag.AllUSer = userNewList.ToList();
            var userCanupdate = alluser.Where(x => model.ListUserUpdate.Contains(x.Id)).ToList();
            ViewBag.AllUSerFee = alluser.Where(x => !model.ListUserUpdate.Contains(x.Id)).ToList();
            ViewBag.UserCanupdate = userCanupdate;
            ViewBag.Categories = newsServices.ListCatelories(NewType.News);
            IEnumerable<ServerFile> files = freightServices.getServerFile(id, new SSM.Models.NewsModel().GetType().ToString());

            model.FilesList = files != null ? files.ToList() : new List<ServerFile>();
            return PartialView("_EditView", model);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            ViewBag.Title = "UPDATE YOUR NEW INFORMATION";
            model.ModifiedBy = CurrenUser;
            model.DateModify = DateTime.Now;
            model.Type = NewType.News;
            newsServices.SaveUpdate(model);
            UploadFile(model, filesUpdoad);
            return List(string.Empty);
        }
        public ActionResult Delete(int id)
        {
            var files = freightServices.getServerFile(id, new NewsModel().GetType().ToString());
            foreach (var file in files.Where(file => file != null).Where(file => System.IO.File.Exists(Server.MapPath(file.Path))))
            {
                System.IO.File.Delete(Server.MapPath(file.Path));
            }
            newsServices.DeleteNew(id);
            return List(string.Empty);
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
            string filepath = AppDomain.CurrentDomain.BaseDirectory + document.Path;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, document.FileMimeType);
        }
        [HttpPost]
        public ActionResult DeleteFile(long id)
        {
            try
            {
                var idFile = (long)id;
                var file = freightServices.getServerFile(idFile);
                if (file != null)
                    if (System.IO.File.Exists(Server.MapPath(file.Path)))
                        System.IO.File.Delete(Server.MapPath(file.Path));
                freightServices.deleteServerFile(idFile);
                return Json(new { isFalse = false, messageErro = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { isFalse = true, messageErro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}