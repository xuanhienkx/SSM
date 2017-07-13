using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSM.Models;
using System.Web.Routing;
using System.IO;
using SSM.Services;

namespace SSM.Controllers
{
    public class ListItem
    {
        public String Value { get; set; }
        public String Text { get; set; }
    }
    public class FreightController : Controller
    {
        public static String FREIGHT_PATH = "/FileManager/Freight";
        private User User1;
        private FreightServices _freightService;
        private ShipmentServices _shipmentService;
        private IAgentService agentService;
        private ICarrierService carrierService;
        private IAreaService areaService;
        private IServicesTypeServices servicesType;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _freightService = new FreightServicesImpl();
            _shipmentService = new ShipmentServicesImpl();
            carrierService = new CarrierService();
            agentService = new AgentService();
            areaService = new AreaService();
            servicesType = new ServicesTypeServices();
            ViewData["CountryList"] = new SelectList(agentService.GetAllCountry(), "Id", "CountryName");
            ViewData["AirlineList"] = carrierService.GetAll(x => x.Type == CarrierType.AirLine.ToString());
            ViewData["CarrierList"] = carrierService.GetAll(x => x.Type == CarrierType.ShippingLine.ToString());
            ViewData["AgentList"] = agentService.GetAll();
            List<String> List1 = new List<String>();
            ViewData["AreaListDep"] = new SelectList(List1);
            ViewData["AreaListDes"] = new SelectList(List1);
            ViewData["ServiceList"] = Services;
            User1 = (User)Session[AccountController.USER_SESSION_ID];
        }

        private IEnumerable<ServicesType> servicesList;
        private SelectList Services
        {
            get
            {
                List<ServicesType> list = new List<ServicesType>();
                var servicesTypeItem = new ServicesType { Id = 0, SerivceName = "--All Services--" };
                list.Add(servicesTypeItem);
                list.AddRange(servicesList ?? servicesType.GetAll(x => x.IsActive).OrderBy(x => x.SerivceName).ToList());
                return new SelectList(list, "Id", "SerivceName");
            }

        }
        private SelectList FreightTypes
        {
            get
            {
                var FreightTypes = from FreightModel.FreightTypes Sv in Enum.GetValues(typeof(FreightModel.FreightTypes))
                                   select new { Id = Sv, Name = Sv.GetStringLabel() };
                return new SelectList(FreightTypes, "Id", "Name");
            }

        }
        public JsonResult GetJsonByFreightType(String FreightType)
        {
            IEnumerable<CarrierAirLine> CarrierAirLineList = null;
            List<CarrierModel> Carrires = new List<CarrierModel>();
            if (FreightModel.FreightTypes.AirFreight.ToString().Equals(FreightType))
            {
                CarrierAirLineList = carrierService.GetAllByType(CarrierType.AirLine.ToString());
            }
            else if (FreightModel.FreightTypes.OceanFreight.ToString().Equals(FreightType))
            {
                CarrierAirLineList = carrierService.GetAllByType(CarrierType.ShippingLine.ToString());
            }
            else
            {
                CarrierAirLineList = carrierService.GetQuery();
            }
            foreach (CarrierAirLine CarrierAirLine1 in CarrierAirLineList)
            {
                CarrierModel CarrierModel1 = new CarrierModel();
                CarrierModel1.Id = CarrierAirLine1.Id;
                CarrierModel1.Description = CarrierAirLine1.Description;
                CarrierModel1.CarrierAirLineName = CarrierAirLine1.CarrierAirLineName;
                Carrires.Add(CarrierModel1);

            }
            return this.Json(Carrires, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetJsonByCountry(long CountryId)
        {
            IEnumerable<Area> AreaList = areaService.GetAll(x => x.CountryId.Value == CountryId);
            List<AreaModel> AreaModels = new List<AreaModel>();
            foreach (Area Area1 in AreaList)
            {
                AreaModel AreaModel1 = new AreaModel();
                AreaModel1.Id = Area1.Id;
                AreaModel1.AreaAddress = Area1.AreaAddress;
                AreaModel1.CountryId = CountryId;
                AreaModels.Add(AreaModel1);
            }
            return this.Json(AreaModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListFreight(String FreightType)
        {
            ViewData["FreightType"] = FreightType;
            if (FreightType == null)
            {
                FreightType = FreightModel.FreightTypes.OceanFreight.ToString();
            }
            IEnumerable<Freight> FreightList1 = _freightService.getAllFreightByType(FreightType);
            FreightList1 = FreightList1.OrderByDescending(m => m.UpdateDate);
            ViewData["FreightList1"] = FreightList1;
            return View();
        }

        [HttpPost]
        public ActionResult ListFreight(FreightModel FreightModel1, String FreightType)
        {
            ViewData["FreightType"] = FreightType;
            if (FreightType == null)
            {
                FreightType = FreightModel.FreightTypes.OceanFreight.ToString();
            }
            IEnumerable<Area> AreaListDep = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDep == null ? 0 : FreightModel1.CountryNameDep);
            IEnumerable<Area> AreaListDes = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDes == null ? 0 : FreightModel1.CountryNameDes);
            if (AreaListDep == null || AreaListDep.Count() < 0)
            {
                AreaListDep = new List<Area>();
            }
            if (AreaListDes == null || AreaListDes.Count() < 0)
            {
                AreaListDes = new List<Area>();
            }
            ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
            ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
            IEnumerable<Freight> FreightList1 = _freightService.getAllFreight(FreightModel1, FreightType);
            if ("Carrier".Equals(FreightModel1.SortType))
            {
                if ("asc".Equals(FreightModel1.SortOder))
                {
                    FreightList1 = FreightList1.OrderBy(m => m.CarrierAirLine.AbbName);
                }
                else
                {
                    FreightList1 = FreightList1.OrderByDescending(m => m.CarrierAirLine.AbbName);
                }
            }
            else if ("Agent".Equals(FreightModel1.SortType))
            {
                if ("asc".Equals(FreightModel1.SortOder))
                {
                    FreightList1 = FreightList1.OrderBy(m => m.Agent.AbbName);
                }
                else
                {
                    FreightList1 = FreightList1.OrderByDescending(m => m.Agent.AbbName);
                }
            }
            else if ("ValidDate".Equals(FreightModel1.SortType))
            {
                if ("asc".Equals(FreightModel1.SortOder))
                {
                    FreightList1 = FreightList1.OrderBy(m => m.ValidDate);
                }
                else
                {
                    FreightList1 = FreightList1.OrderByDescending(m => m.ValidDate);
                }
            }
            else
            {
                FreightList1 = FreightList1.OrderByDescending(m => m.UpdateDate);
            }
            ViewData["FreightList1"] = FreightList1;
            return View(FreightModel1);
        }
        //
        // GET: /Freight/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateFreight()
        {
            ViewData["FreightTypes"] = FreightTypes;
            FreightModel model = new FreightModel();
            model.ServiceId = 3;
            return View(model);
        }

        //
        // POST: /Freight/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateFreight(FreightModel FreightModel1)
        {
            if (ModelState.IsValid)
            {
                FreightModel1.UserId = User1.Id;
                FreightModel1.CreateDate = DateTime.Now.ToString("dd/MM/yyyy");
                FreightModel1.UpdateDate = DateTime.Now.ToString("dd/MM/yyyy");

                if (FreightModel.FreightTypes.OceanFreight.ToString().Equals(FreightModel1.Type))
                {
                    FreightModel1.ServiceName = ShipmentModel.Services.SeaInbound.ToString();
                }
                else if (FreightModel.FreightTypes.AirFreight.ToString().Equals(FreightModel1.Type))
                {
                    FreightModel1.ServiceName = ShipmentModel.Services.AirInbound.ToString();
                }
                else if (FreightModel.FreightTypes.InlandRates.ToString().Equals(FreightModel1.Type))
                {
                    FreightModel1.ServiceName = ShipmentModel.Services.InlandService.ToString();
                }
                else
                {
                    FreightModel1.ServiceName = ShipmentModel.Services.Other.ToString();
                }
                FreightModel1.ServiceId = servicesType.GetId(FreightModel1.ServiceName);
                Freight Freight1 = _freightService.CreateFreight(FreightModel1);
                if (Freight1 != null)
                {
                    foreach (string inputTagName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[inputTagName];
                        if (file.ContentLength > 0)
                        {
                            string filePath = Path.Combine(Server.MapPath("~/" + FREIGHT_PATH)
                                    , Path.GetFileName(file.FileName));
                            file.SaveAs(filePath);
                            //save file to db
                            ServerFile ServerFile1 = new ServerFile();
                            ServerFile1.ObjectId = Freight1.Id;
                            ServerFile1.ObjectType = Freight1.GetType().ToString();
                            ServerFile1.Path = FREIGHT_PATH + "/" + file.FileName;
                            ServerFile1.FileName = file.FileName;
                            ServerFile1.FileSize = file.ContentLength;
                            ServerFile1.FileMimeType = file.ContentType;
                            _freightService.insertServerFile(ServerFile1);
                        }
                    }
                }
                return RedirectToAction("ListFreight", new { Id = 0, FreightType = FreightModel1.Type });
            }
            else
            {
                IEnumerable<Area> AreaListDep = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDep);
                IEnumerable<Area> AreaListDes = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDes);
                ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
                ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
                ViewData["FreightTypes"] = FreightTypes;
            }
            return View(FreightModel1);
        }


        private void viewServerFiles(Freight Freight1)
        {
            IEnumerable<ServerFile> ServerFiles = _freightService.getServerFile(Freight1.Id, Freight1.GetType().ToString());
            ViewData["ServerFiles"] = ServerFiles;
        }

        public ActionResult DeleteServerFile(long id)
        {

            ServerFile file = _freightService.getServerFile(id);
            long freightId = file.ObjectId.Value;
            _freightService.deleteServerFile(id);

            return RedirectToAction("EditFreight", new { id = freightId });
        }

        public ActionResult EditFreight(int id)
        {
            Freight Freight1 = _freightService.getFreightById(id);
            FreightModel FreightModel1 = null;
            if (Freight1 == null)
            {
                return RedirectToAction("ListFreight", new { Id = 0, FreightType = FreightModel.FreightTypes.OceanFreight.ToString() });
            }
            else
            {
                FreightModel1 = FreightModel.ConvertFreight(Freight1);
                FreightModel1.CountryNameDep = Freight1.Area.CountryId == null ? 0 : Freight1.Area.CountryId.Value;
                FreightModel1.CountryNameDes = Freight1.Area1.CountryId == null ? 0 : Freight1.Area1.CountryId.Value;
                IEnumerable<Area> AreaListDep = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDep);
                IEnumerable<Area> AreaListDes = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDes);
                ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
                ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
                viewServerFiles(Freight1);
            }

            if (FreightModel1.Type.Equals(CarrierType.AirLine.ToString()))
            {
                ViewData["ALLCarrierList"] = carrierService.GetAllByType(CarrierType.AirLine.ToString());
            }
            else if (FreightModel1.Type.Equals(CarrierType.ShippingLine.ToString()))
            {
                ViewData["ALLCarrierList"] = carrierService.GetAllByType(CarrierType.ShippingLine.ToString());
            }
            else
            {
                ViewData["ALLCarrierList"] = carrierService.GetAll();
            }
            ViewData["FreightTypes"] = FreightTypes;
            return View(FreightModel1);
        }

        //
        // POST: /Freight/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFreight(int id, FreightModel FreightModel1)
        {
            Freight Freight1 = _freightService.getFreightById(id);
            if (ModelState.IsValid)
            {
                try
                {

                    if (Freight1 != null)
                    {
                        FreightModel1.Id = id;
                        FreightModel1.CreateDate = Freight1.CreateDate.Value.ToString("dd/MM/yyyy");
                        FreightModel1.UpdateDate = DateTime.Now.ToString("dd/MM/yyyy");
                        FreightModel1.UserId = User1.Id;

                        if (FreightModel.FreightTypes.OceanFreight.ToString().Equals(FreightModel1.Type))
                        {
                            FreightModel1.ServiceName = ShipmentModel.Services.SeaInbound.ToString();
                        }
                        else if (FreightModel.FreightTypes.AirFreight.ToString().Equals(FreightModel1.Type))
                        {
                            FreightModel1.ServiceName = ShipmentModel.Services.AirInbound.ToString();
                        }
                        else if (FreightModel.FreightTypes.InlandRates.ToString().Equals(FreightModel1.Type))
                        {
                            FreightModel1.ServiceName = ShipmentModel.Services.InlandService.ToString();
                        }
                        else
                        {
                            FreightModel1.ServiceName = ShipmentModel.Services.Other.ToString();
                        }

                        _freightService.UpdateFreight(FreightModel1);
                        foreach (string inputTagName in Request.Files)
                        {
                            HttpPostedFileBase file = Request.Files[inputTagName];
                            if (file.ContentLength > 0)
                            {
                                string filePath = Path.Combine(HttpContext.Server.MapPath("~/" + FREIGHT_PATH)
                                        , Path.GetFileName(file.FileName));
                                file.SaveAs(filePath);
                                //save file to db
                                ServerFile ServerFile1 = new ServerFile();
                                ServerFile1.ObjectId = Freight1.Id;
                                ServerFile1.ObjectType = Freight1.GetType().ToString();
                                ServerFile1.Path = FREIGHT_PATH + "/" + file.FileName;
                                ServerFile1.FileName = file.FileName;
                                ServerFile1.FileMimeType = file.ContentType;
                                ServerFile1.FileSize = file.ContentLength;
                                _freightService.insertServerFile(ServerFile1);
                            }
                        }
                    }
                    return RedirectToAction("ListFreight", new { Id = 0, FreightType = FreightModel1.Type });
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                IEnumerable<Area> AreaListDep = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDep);
                IEnumerable<Area> AreaListDes = _shipmentService.getAllAreaByCountry(FreightModel1.CountryNameDes);
                ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
                ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
                ViewData["FreightTypes"] = FreightTypes;
                viewServerFiles(Freight1);
            }
            return View(FreightModel1);
        }
        //
        // GET: /Freight/Delete/5

        public ActionResult Delete(int id)
        {
            String FreightType = FreightModel.FreightTypes.OceanFreight.ToString();
            try
            {
                Freight Freight1 = _freightService.getFreightById(id);
                FreightType = Freight1.Type;
                _freightService.DeleteFreight(id);
                return RedirectToAction("ListFreight", new { Id = 0, FreightType = FreightType });
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }
            return RedirectToAction("ListFreight", new { Id = 0, FreightType = FreightType });
        }

    }
}
