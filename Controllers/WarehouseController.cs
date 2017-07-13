using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Routing;
using SSM.Common;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class WarehouseController : Controller
    {
        private User currentUser;
        private IWarehouseSevices warehouseSevices;
        private GridNew<Warehouse, WareHouseModel> warehouseGird;
        private IAreaService areaService;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            currentUser = (User)Session[AccountController.USER_SESSION_ID];
            warehouseSevices = new WareHouseServices();
            areaService = new AreaService();
            ViewData["location"] = new SelectList(areaService.GetAll(x => x.trading_yn.Value == true), "Id", "AreaAddress");
        }

        public ActionResult Index()
        {

            warehouseGird = new GridNew<Warehouse, WareHouseModel>(
                     new Pager
                     {
                         CurrentPage = 1,
                         PageSize = 10,
                     });
            UpdateGrid(); 
            return View(warehouseGird);
        }

        [HttpPost]
        public ActionResult Index( GridNew<Warehouse, WareHouseModel> grid)
        {
            warehouseGird = grid;
            warehouseGird.ProcessAction();
            warehouseGird.Model = new WareHouseModel();
            UpdateGrid();
            return PartialView("_ListData",warehouseGird);
        }
        private void UpdateGrid()
        {
            int Skip = (warehouseGird.Pager.CurrentPage - 1) * warehouseGird.Pager.PageSize;
            int Take = warehouseGird.Pager.PageSize;
            var warehouseList = warehouseSevices.GetAll();
            int totalRows = warehouseList.Count();
            warehouseGird.Pager.Init(totalRows);
            if (totalRows == 0)
            {
                warehouseGird.Data = new List<Warehouse>();
                return;
            }
            warehouseList = warehouseList.AsQueryable().Skip(Skip).Take(Take).ToList();
            warehouseGird.Data = warehouseList;
        }

        public ActionResult Edit(int id)
        {
            var model = warehouseSevices.GetWareHouseModel(id);
            model = model ?? new WareHouseModel();
            return PartialView("_formEditView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WareHouseModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (model.Id > 0)
                    {
                        model.ModifiedBy = currentUser.Id;
                        model.DateModify = DateTime.Now;
                        warehouseSevices.UpdateWareHouse(model);
                    }
                    else
                    {
                        model.CreatedBy = currentUser.Id;
                        model.DateCreate = DateTime.Now;
                        warehouseSevices.InsertWareHouse(model);
                    }
                }
                return Json(1);
            }
            return PartialView("_formEditView", model);
        }
        public ActionResult Delete(long id)
        {
            warehouseSevices.DeleteWareHouse(id);
            return RedirectToAction("Index", new { id = 0 });
        }
    }
}