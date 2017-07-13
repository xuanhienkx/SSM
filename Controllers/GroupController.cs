using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SSM.Common;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class GroupController : BaseController
    {
        private IGroupService groupService;
        private UsersServices usersServices;
        private GridNew<Group, GroupModel> gridView;
        private const string GROUP_SEARCH_MODEL = "GROUP_SEARCH_MODEL";
        public GroupController()
        {
            this.groupService = new GroupService();
            usersServices = new UsersServicesImpl();
        }

        public ActionResult Index()
        {
            gridView = (GridNew<Group, GroupModel>)Session[GROUP_SEARCH_MODEL];
            if (gridView == null)
            {
                gridView = new GridNew<Group, GroupModel>(
                    new Pager
                    {
                        CurrentPage = 1,
                        PageSize = 10,
                        Sidx = "Name",
                    })
                {
                    SearchCriteria = new Group()
                };
            }
            UpdateGridData();
            return View(gridView);
        }
        [HttpPost]
        public ActionResult Index(GridNew<Group, GroupModel> grid)
        {
            gridView = grid;
            Session[GROUP_SEARCH_MODEL] = grid;
            UpdateGridData();
            return PartialView("_ListData", gridView);
        }

        private void UpdateGridData()
        {

            // int skip = (gridView.Pager.CurrentPage - 1) * gridView.Pager.PageSize;
            var page = gridView.Pager;
            int take = gridView.Pager.PageSize;
            var groups = groupService.GetQuerys(gridView.SearchCriteria);
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "Name" : page.Sidx, page.Sord == "asc");

            int totalRows = groups.Count();
            gridView.Pager.Init(totalRows);
            if (totalRows == 0)
            {
                gridView.Data = new List<Group>();
                return;
            }
            groups = groups.OrderBy(sort);
            IEnumerable<Group> list = groupService.GetListPager(groups, gridView.Pager.CurrentPage, take);
            gridView.Data = list;
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = id == 0 ? new GroupModel() { UserGroups = new List<UserGroup>(), ListUserAccesses = new long[10], IsActive = true } : groupService.GetGroupModel(id);
            var listUser = usersServices.GetQuery(x => x.IsActive).OrderBy(x => x.FullName).ThenBy(x => x.Department.DeptName).ToList();
            ViewBag.AllUSer = listUser;
            if (id != 0)
            {
                var listUserAccess = model.UserGroups.Select(x => x.User).OrderBy(x => x.FullName).ThenBy(x => x.Department.DeptName).ToList();
                var userNewList = listUser.Where(x => !listUserAccess.Select(u => u.Id).Contains(x.Id));
                ViewBag.AllUSer = userNewList.ToList();
            }

            model = model ?? new GroupModel();
            return PartialView("_formEditView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupModel model)
        {
            string message;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id > 0)
                    {
                        groupService.UpdateGroup(model, out message);
                    }
                    else
                    {
                        model.Id = 0;
                        groupService.CreateGroup(model, out message);
                    }

                    if (string.IsNullOrEmpty(message))
                    {
                        return Json(1);
                    }
                    else
                    {
                        throw new Exception(message);
                    }
                }
                else
                {
                    var errm = ViewData.ModelState.Values.Aggregate(string.Empty, (current1, modelState) => modelState.Errors.Aggregate(current1, (current, error) => current + error.ErrorMessage+"\n"));
                    throw new Exception(errm);
                }


            }
            catch (Exception ex)
            {
                var listUser = usersServices.GetQuery(x => x.IsActive).OrderBy(x => x.FullName).ThenBy(x => x.Department.DeptName).ToList();
                ViewBag.AllUSer = listUser;
                ViewBag.ErrorMessage = ex.Message;
                return PartialView("_formEditView", model);
            }

        }

        public ActionResult SetGroupActive(int id, bool isActive)
        {
            groupService.SetActive(id, isActive);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckDelete(int id)
        {
            ViewBag.checkDelete = groupService.CheckGroupFree(id);
            return PartialView("_CheckDelete", id);
        }
        public ActionResult Delete(int id)
        {
            string message;
            if (groupService.CheckGroupFree(id))
            {
                groupService.DeleteGroup(id, out message);
            }
            return RedirectToAction("Index", "Group", new { id = 0 });
        }
    }
}