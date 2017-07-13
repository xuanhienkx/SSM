using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class SupplierController : Controller
    {
        private GridNew<Supplier, SupplierModels> _supplierGrid;
        private const string SUPPLIER_SEARCH_MODEL = "SUPPLIER_SEARCH_MODEL";
        private User currentUser;
        private ShipmentServices _shipmentServices;
        private ISupplierServices _supplierServices;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            currentUser = (User)Session[AccountController.USER_SESSION_ID];
            _shipmentServices = new ShipmentServicesImpl();
            _supplierServices = new SupplierSerivecs();
            ViewData["CountryList"] = new SelectList(_supplierServices.GetAllCountry(), "Id", "CountryName");

        }

        public ActionResult Index()
        {
            _supplierGrid = (GridNew<Supplier, SupplierModels>)Session[SUPPLIER_SEARCH_MODEL];
            if (_supplierGrid == null)
            {
                _supplierGrid = new GridNew<Supplier, SupplierModels>(
                    new Pager
                    {
                        CurrentPage = 1,
                        PageSize = 10,
                        Sord = "asc",
                        Sidx = "FullName"
                    })
                {
                    SearchCriteria = new Supplier()
                };
            }

            _supplierGrid.Model = new SupplierModels();
            UpdateGridSupplierData();
            return View(_supplierGrid);
        }
        [HttpPost]
        public ActionResult Index( GridNew<Supplier, SupplierModels> grid)
        {
            _supplierGrid = grid;
            Session[SUPPLIER_SEARCH_MODEL] = grid;
            _supplierGrid.ProcessAction();
            UpdateGridSupplierData();
            return PartialView("_ListData",_supplierGrid);
        }
        private void UpdateGridSupplierData()
        {
            var page = _supplierGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "FullName" : page.Sidx, page.Sord == "asc");
            var rows = 0;
            //var suppliers = _supplierServices.GetAll(_supplierGrid.SearchCriteria, sort, out rows, page.CurrentPage, page.PageSize);
            var suppliers = _supplierServices.GetAll(_supplierGrid.SearchCriteria );
            var q = suppliers.OrderBy(sort);
            rows = q.Count();
            _supplierGrid.Pager.Init(rows);
            if (rows == 0)
            {
                _supplierGrid.Data = new List<Supplier>();
                return;
            }
            _supplierGrid.Data = _supplierServices.GetListPager(q,page.CurrentPage,page.PageSize);
            //_supplierGrid.Data = suppliers;
        }

        public ActionResult SupplierDelete(long id)
        {
            _supplierServices.DeleteSupplier(id);
            return RedirectToAction("Index", new { id = 0 });
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ViewData["CountryList"] = new SelectList(_supplierServices.GetAllCountry(), "Id", "CountryName");
            var model = _supplierServices.GetSupplierModel(id);
            model = model ?? new SupplierModels();
            return PartialView("_formEditView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierModels model)
        {
            ViewData["CountryList"] = new SelectList(_supplierServices.GetAllCountry(), "Id", "CountryName");
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    model.ModifiedBy = currentUser.Id;
                    model.DateModify = DateTime.Now;
                    _supplierServices.UpdateSupplier(model);
                }
                else
                {
                    model.CreatedBy = currentUser.Id;
                    model.DateCreate = DateTime.Now;
                    model.Id = 0;
                    _supplierServices.InsertSupplier(model);
                }
                return Json(1);
            }
            return PartialView("_formEditView", model);
        }

        public JsonResult SupplierSuggest(string term)
        {
            var result = _supplierServices.GetAll()
                .Where(x => x.FullName.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.FullName).Take(20)
                .ToList()
                .Select(x => new { id = x.Id, Display = x.FullName });
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}