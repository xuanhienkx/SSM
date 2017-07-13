using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class ServicesController : BaseController
    {
        private GridNew<ServicesType, ServicesViewModel> gridView;
        private IServicesTypeServices servicesType;
        private const string SERVICE_SEARCH_MODEL = "SERVICE_SEARCH_MODEL";
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            servicesType = new ServicesTypeServices();
        }

        public ActionResult Index()
        {

            gridView = (GridNew<ServicesType, ServicesViewModel>)Session[SERVICE_SEARCH_MODEL];
            if (gridView == null)
            {
                gridView = new GridNew<ServicesType, ServicesViewModel>(
                    new Pager
                    {
                        CurrentPage = 1,
                        PageSize = 10,
                        Sidx = "Name",
                    })
                {
                    SearchCriteria = new ServicesType()
                };
            }
            UpdateGridSupplierData();
            return View(gridView);
        }
        [HttpPost]
        public ActionResult Index(GridNew<ServicesType, ServicesViewModel> grid)
        {
            gridView = grid;
            Session[SERVICE_SEARCH_MODEL] = grid;
            UpdateGridSupplierData();
            return PartialView("_ListData", gridView);
        }
        private void UpdateGridSupplierData()
        {
            int Skip = (gridView.Pager.CurrentPage - 1) * gridView.Pager.PageSize;
            int Take = gridView.Pager.PageSize;
            var services = servicesType.GetQuerys(gridView.SearchCriteria);
            int totalRows = services.Count();
            gridView.Pager.Init(totalRows);
            if (totalRows == 0)
            {
                gridView.Data = new List<ServicesType>();
                return;
            }
            IEnumerable<ServicesType> servicesTypes = services.Skip(Skip).Take(Take).ToList();
            gridView.Data = servicesTypes;
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = servicesType.GetModelById(id);
            model = model ?? new ServicesViewModel();
            return PartialView("_formEditView", model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    model.ModifiedBy = CurrenUser.Id;
                    model.DateModify = DateTime.Now;
                    servicesType.Update(model);
                }
                else
                {
                    model.CreatedBy = CurrenUser.Id;
                    model.DateCreate = DateTime.Now;
                    model.Id = 0;
                    servicesType.Create(model);
                }
                return Json(1);
            }
            return PartialView("_formEditView", model);
        }

        public ActionResult Delete(int id)
        {
            if (servicesType.CheckServiceFree(id))
            {
                servicesType.Delete(id);
            }
            return RedirectToAction("Index", "Services", new { id = 0 });
        }

        public ActionResult CheckDelete(int id)
        {
            ViewBag.checkDelete = servicesType.CheckServiceFree(id);
            return PartialView("_CheckDelete", id);
        }

        public ActionResult SetServiceActive(int id, bool isActive)
        {
            if (CurrenUser.IsAdmin())
            {
                servicesType.SetActive(id, isActive);
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}