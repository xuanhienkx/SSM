using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SSM.Common;

using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class CRMEventController : BaseController
    {
        #region Define
        private ICRMEventService eventService;
        private ICRMScheduleServiec scheduleServiec;
        private ICRMEvetypeService evetypeService;
        private static String DOCUMENT_PATH = "/FileManager/CRMDocument";
        private IServerFileService fileService;
        private ICRMCustomerService crmCustomerService;
        private UsersServices usersServices;
        private ICRMUserFollowEventService followEventService;

        private const string CRMVISITED_SEARCH_MODEL = "CRMVISITED_SEARCH_MODEL";
        private const string CRMEVENT_SEARCH_MODEL = "CRMEVENT_SEARCH_MODEL";
        private const string CALENDAR_SEARCH_MODEL = "CALENDAR_SEARCH_MODEL";
        private Grid<CRMEventModel> _grid;
        public CRMEventController()
        {
            eventService = new CRMEventService();
            scheduleServiec = new CRMScheduleServiec();
            evetypeService = new CRMEvetypeService();
            fileService = new ServerFileService();
            crmCustomerService = new CRMCustomerService();
            usersServices = new UsersServicesImpl();
            followEventService = new CRMUserFollowEventService();
        }

        #endregion
        #region List
        public ActionResult Index()
        {
            _grid = (Grid<CRMEventModel>)Session[CRMVISITED_SEARCH_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<CRMEventModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "DateBegin"
                                }
                            );
                _grid.SearchCriteria = new CRMEventModel();
            }
            UpdateGrid();
            ViewData["UserSalesList"] = usersServices.GetAllSales(CurrenUser, false);
            return View(_grid);
        }
        [HttpPost]
        public ActionResult Index(Grid<CRMEventModel> grid, ListEventFilter filter)
        {
            _grid = grid;
            _grid.ProcessAction();
            Session[CRMVISITED_SEARCH_MODEL] = grid;
            UpdateGrid(filter);
            ViewData["UserSalesList"] = usersServices.GetAllSales(CurrenUser, false);
            ViewBag.SearchingMode = filter;
            return PartialView("_List", _grid);
        }

        private void UpdateGrid(ListEventFilter filter = null)
        {
            filter = filter ?? new ListEventFilter();
            filter.Sales = filter.Sales ?? 0;
            bool isEventAction = filter.OfEvent == TypeOfEvent.Events;
            ViewBag.IsEventAction = isEventAction;
            var page = _grid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "DateEvent" : page.Sidx, page.Sord == "asc");
            var totalRow = 0;
            var qr = eventService.GetQuery(x => (filter.Sales == 0 || filter.Sales == x.CreatedById ||
                                                (x.CRMCustomer.CRMFollowCusUsers != null &&
                                                 x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == filter.Sales)) ||
                                                (x.CRMCustomer.CreatedById == CurrenUser.Id)
                                                || (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == filter.Sales))
                                                )
                                                && (filter.Status == CRMEventStatus.All || x.Status == (byte)filter.Status)
                                                && (string.IsNullOrEmpty(filter.CustomerName) ||
                                                 x.CRMCustomer.CompanyShortName.Contains(filter.CustomerName) ||
                                                 x.CRMCustomer.CompanyName.Contains(filter.CustomerName)));
            if (filter.OfEvent.HasValue)
                qr = filter.OfEvent == TypeOfEvent.Events ? qr.Where(x => x.IsEventAction == true) : qr.Where(x => x.IsEventAction == false);

            if (filter.BeginDate.HasValue)
            {
                qr = qr.Where(x => x.DateEvent >= filter.BeginDate);
            }
            if (filter.EndDate.HasValue)
            {
                qr = qr.Where(x => x.DateEvent.Date <= filter.EndDate);
            }

            if (!CurrenUser.IsDirecter() && filter.Sales == 0)
            {

                if (CurrenUser.IsDepManager())
                {
                    qr = qr.Where(x => x.User.DeptId == CurrenUser.DeptId || (x.CRMCustomer.CRMFollowCusUsers != null &&
                                            x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == CurrenUser.Id)) ||
                                           (x.CRMCustomer.CreatedById == CurrenUser.Id)
                                            || (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == CurrenUser.Id)));
                }
                else
                {
                    qr = qr.Where(x => x.CreatedById == CurrenUser.Id ||
                                       (x.CRMCustomer.CRMFollowCusUsers != null &&
                                        x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == CurrenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == CurrenUser.Id)
                                       || (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == CurrenUser.Id)));
                }
            }
            qr = qr.OrderByDescending(x => x.DateEvent).ThenBy(sort);
            totalRow = qr.Count();
            var list = eventService.GetListPager(qr, page.CurrentPage, page.PageSize);
            var listView = list.Select(x => eventService.ToModel(x)).ToList();
            _grid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _grid.Data = new List<CRMEventModel>();
                ViewBag.TotalDisplay = string.Empty;
                return;
            }
            var typeOfEvent = filter.OfEvent == TypeOfEvent.Visited ? "viếng thăm" : "sự kiện";
            var finished = list.Count(x => x.Status == (byte)CRMEventStatus.Finished);
            var follow = list.Count(x => x.Status == (byte)CRMEventStatus.Follow);
            string display = string.Format(Resources.Resource.CRM_EVENT_LIST_TOTAL, totalRow, typeOfEvent, finished, follow);
            ViewBag.TotalDisplay = display;
            _grid.Data = listView;
        }
        public JsonResult ListByCus(long refId, bool isEventAction = false)
        {
            var sort = new SSM.Services.SortField("Subject", true);
            ViewBag.IsEventAction = isEventAction;
            var totalRow = 0;
            var page = new Pager() { CurrentPage = 1, PageSize = 100 };
            IEnumerable<CRMEventModel> listEvents = eventService.GetAll(sort, out totalRow, page, refId, isEventAction, CurrenUser);
            crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(refId);
            var value = new
            {
                Views = this.RenderPartialView("_ListForCus", listEvents),
                CloseOther = true,
                Title = string.Format(@"Danh sách {0} của khách hàng {1}", isEventAction ? "sự kiện" : "viếng thăm", crmCustomer.CompanyShortName),
            };
            return JsonResult(value, true);
        }
        #endregion
        #region Created and edit

        private CRMCustomerModel crmCustomer;
        public ActionResult Create(long refId = 0, bool isEventAction = false)
        {
            crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(refId);
            var model = new CRMEventModel()
            {
                DateBegin = DateTime.Now,
                DateEnd = DateTime.Now,
                DateEvent = DateTime.Now,
                Status = CRMEventStatus.Follow,
                CrmCusId = refId,
                IsEventAction = isEventAction,
                TimeOfRemider = "9:00",
                FilesList = new List<ServerFile>()
            };
            model.CheckModels = LoadCheckModel(model.DayOfWeek);
            ViewData["CRMEventType"] = evetypeService.GetAll();
            if (crmCustomer != null)
            {
                model.CusName = crmCustomer.CompanyShortName;
            }
            var value = new
            {
                Views = this.RenderPartialView("_EditTemplate", model),
                Title = string.Format(@"Tạo {0} {1} ", isEventAction ? "sự kiện" : "viếng thăm", crmCustomer != null ? "cho khách hàng " + crmCustomer.CompanyShortName : ""),
            };
            return JsonResult(value, true);
        }

        [HttpPost]
        public ActionResult Create(CRMEventModel model)
        {
            ViewData["CRMEventType"] = evetypeService.GetAll();
            model.FilesList = new List<ServerFile>();
            model.CheckModels = LoadCheckModel(model.DayOfWeek);  
            if (!CheckModelValid(model))
            {
                GetRefEvent(model);
                return Json(new
                {
                    Message = Resources.Resource.CRM_EDIT_ERROR_MESSAGE_BLANK,
                    Success = false,
                    View = this.RenderPartialView("_EditTemplate", model)
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (string.IsNullOrEmpty(model.CusName) && model.CrmCusId == 0)
                {
                    ModelState.AddModelError("CusName", @"Tên khách hàng không tồn tại trong hệ thống");
                    return Json(new
                    {
                        Message = Resources.Resource.CRM_EDIT_ERROR_MESSAGE_BLANK,
                        Success = false,
                        View = this.RenderPartialView("_EditTemplate", model)
                    }, JsonRequestBehavior.AllowGet);
                }
                model.TypeOfEvent = model.IsEventAction ? TypeOfEvent.Events : TypeOfEvent.Visited;
                if (!string.IsNullOrEmpty(model.TimeBegin))
                {
                    int[] timeBegin = model.TimeBegin.Split(':').Select(x => int.Parse(x)).ToArray();
                    model.DateBegin = new DateTime(model.DateBegin.Year, model.DateBegin.Month, model.DateBegin.Day, timeBegin[0], timeBegin[1], 0);
                }
                if (!string.IsNullOrEmpty(model.TimeEnd) && model.DateEnd != null)
                {
                    int[] timeEnd = model.TimeEnd.Split(':').Select(x => int.Parse(x)).ToArray();
                    model.DateBegin = new DateTime(model.DateBegin.Year, model.DateBegin.Month, model.DateBegin.Day, timeEnd[0], timeEnd[1], 0);
                }

                if (model.Id > 0)
                {
                    model.ModifiedBy = CurrenUser;
                    model.ModifiedDate = DateTime.Now;
                    eventService.UpdateToDb(model);
                }
                else
                {
                    model.CreatedBy = CurrenUser;

                    var id = eventService.InsertToDb(model).Id;
                    model.Id = id;

                }

                UploadFile(model, model.Uploads);
                return Json(new
                {
                    Message = @"Thành công",
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                GetRefEvent(model);
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new
                {
                    Message = Resources.Resource.CRM_EDIT_ERROR_MESSAGE,
                    Success = false,
                    View = this.RenderPartialView("_EditTemplate", model)
                }, JsonRequestBehavior.AllowGet);
            }

        }

        private void GetRefEvent(CRMEventModel model)
        {
            var db = eventService.GetById(model.Id);
            if (db != null)
            {
                db.FilesList = fileService.GetServerFile(model.Id, new CRMEventModel().GetType().ToString());
                db.FilesList = db.FilesList ?? new List<ServerFile>();
                model.CreatedBy = db.CreatedBy;
                model.FilesList = db.FilesList;
                model.UsersFollow = db.UsersFollow;
                model.UserFollowNames = db.UserFollowNames;
            }
        }
        private bool CheckModelValid(CRMEventModel model)
        {
            bool isValid = ModelState.IsValid;

            if (model.DateBegin > model.DateEnd)
            {
                isValid = false;
                ModelState.AddModelError(string.Empty, @"Date End must be  more than or equals Date Begin");
            }
            if (model.IsEventAction && (model.EventTypeId == null || model.EventTypeId == 0))
            {
                isValid = false;
                ModelState.AddModelError("EventTypeId", @"Type is required");
            }
            if (model.CrmCusId == 0 || string.IsNullOrEmpty(model.CusName))
            {
                isValid = false;
                ModelState.AddModelError("CusName", @"Customer is required");
            }
            if (model.CrmCusId == 0 && !string.IsNullOrEmpty(model.CusName))
            {
                isValid = false;
                ModelState.AddModelError("CusName", @"Customer is not right");
            }
            return isValid;
        }

        public ActionResult Detail(long id)
        {
            var model = eventService.GetById(id);
            model.CheckModels = LoadCheckModel(model.DayOfWeek);
            model.FilesList = fileService.GetServerFile(id, new CRMEventModel().GetType().ToString());
            model.FilesList = model.FilesList ?? new List<ServerFile>();
            model.CusName = model.CRMCustomer.CompanyShortName;
            model.TimeBegin = model.DateBegin.ToString("HH:mm");
            ViewData["CRMEventType"] = evetypeService.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {

            var model = eventService.GetById(id);
            model.CheckModels = LoadCheckModel(model.DayOfWeek);
            model.FilesList = fileService.GetServerFile(id, new CRMEventModel().GetType().ToString());
            model.FilesList = model.FilesList ?? new List<ServerFile>();
            model.CusName = model.CRMCustomer.CompanyShortName;
            model.TimeBegin = model.DateBegin.ToString("HH:mm");
            ViewData["CRMEventType"] = evetypeService.GetAll();
            var value = new
            {
                Views = this.RenderPartialView("_EditTemplate", model),
                Title = string.Format(@"Sửa {0} cho khách hàng {1} ", model.IsEventAction ? "sự kiện" : "viếng thăm", model.CRMCustomer.CompanyShortName),
            };
            return JsonResult(value, true);
        }
        private void UploadFile(CRMEventModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            if (filesUpdoad == null || !filesUpdoad.Any()) return;
            var type = model.GetType().ToString();
            foreach (var file in filesUpdoad.Where(file => file != null && file.ContentLength > 0))
            {
                try
                {
                    var pathfoder = Path.Combine(Server.MapPath(@"~/" + DOCUMENT_PATH), model.CrmCusId.ToString("D6"), type, model.IsEventAction ? "Event" : "Visited", model.Id.ToString("D4"));
                    if (!Directory.Exists(pathfoder))
                    {
                        Directory.CreateDirectory(pathfoder);
                    }
                    var filePath = Path.Combine(pathfoder, Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);
                    //save file to db
                    var fileSave = new ServerFile
                    {
                        ObjectId = model.Id,
                        ObjectType = type,
                        Path = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", DOCUMENT_PATH, model.CrmCusId.ToString("D6"), type, model.IsEventAction ? "Event" : "Visited", model.Id.ToString("D4"), file.FileName),
                        FileName = file.FileName,
                        FileSize = file.ContentLength,
                        FileMimeType = file.ContentType
                    };
                    fileService.Insert(fileSave);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            }
        }
        #endregion
        #region  Schedule load and setting
        public ActionResult LoadSchedule(int idSchedule, bool isEventAction, bool isList = false)
        {
            ViewBag.IsEventAction = isEventAction;
            var item = scheduleServiec.GetById(idSchedule) ?? new CRMScheduleModel() { DateBegin = DateTime.Now, DateEnd = DateTime.Now, TimeOfSchedule = "12:00pm" };
            item.CheckModels = LoadCheckModel(item.DayOfWeek);
            if (isList)
            {
                var value = new
                {
                    Views = this.RenderPartialView("_ScheduleTemplate", item),
                    Title = "Cài đặt lịch nhắc nhở",
                    ColumnClass = "col-md-5 col-md-offset-2"
                };
                return JsonResult(value, true);
            }
            return PartialView("_ScheduleTemplate", item);
        }
        [HttpPost]
        public ActionResult EditSchedule(CRMScheduleModel model)
        {
            //var isvalid = CheckModelValid(model);
            //if (!isvalid)
            //{
            //    return PartialView("_ScheduleTemplate", model);
            //}
            var listDay = model.CheckModels.Where(x => x.Checked).Select(x => x.Id).ToArray();
            model.DayOfWeek = listDay;
            if (model.Id == 0)
            {
                var data = scheduleServiec.InsertToDb(model);
                model = Mapper.Map<CRMScheduleModel>(data);
            }
            else
            {
                scheduleServiec.UpdateToDb(model);
            }

            return Json(new
            {
                Success = true,
                Message = "Tạo mới thành công",
                Model = model
            }, JsonRequestBehavior.AllowGet);
        }
        List<CheckModel> LoadCheckModel(IEnumerable<int> listDay)
        {
            if (listDay == null || !listDay.Any())
            {
                var list = (from DayOfWeek day in Enum.GetValues(typeof(DayOfWeek))
                            select new CheckModel() { Id = (int)day, Name = day, Checked = false }).ToList();
                return list;
            }
            else
            {
                var list = (from DayOfWeek day in Enum.GetValues(typeof(DayOfWeek))
                            select new CheckModel() { Id = (int)day, Name = day, Checked = listDay.Contains((int)day) }).ToList();
                return list;
            }
        }
        #endregion
        public ActionResult Delete(int id)
        {
            try
            {
                var eventItem = eventService.GetDbById(id);
                var filelist = fileService.GetServerFile(id, new CRMEventModel().GetType().ToString());
                if (filelist.Any())
                {

                    foreach (var serverFile in filelist)
                    {
                        if (serverFile != null)
                            if (System.IO.File.Exists(Server.MapPath(serverFile.Path)))
                                System.IO.File.Delete(Server.MapPath(serverFile.Path));
                        fileService.Delete(serverFile);
                    }
                }
                var follows = followEventService.GetAll(x => x.VisitId == id);
                if (follows.Any())
                {
                    followEventService.DeleteAll(follows);
                }
                eventService.Delete(eventItem);
                var value = new
                {
                    Views = "Bạn đã xoá thành công",
                    Title = "Success!",
                    IsRemve = true,
                    TdId = "del_" + id
                };
                return JsonResult(value, true);
            }
            catch (Exception ex)
            {
                var result = new CommandResult(false)
                {
                    ErrorResults = new[] { ex.Message }
                };
                return JsonResult(result, null, true);
            }
        }

        public ActionResult CalendarView()
        {
            ViewData["UserSalesList"] = usersServices.GetAllSales(CurrenUser, false);
            var current = DateTime.Now;
            var filter = new EventFilter()
            {
                Year = current.Year,
                Month = current.Month,
                Sales = 0,
                OfEvent = null
            };
            ViewBag.filter = filter;
            var qrEvents =
                eventService.GetQuery(x => x.DateBegin.Month == current.Month
                                           && x.DateBegin.Year == current.Year);
            if (!CurrenUser.IsDirecter())
            {

                if (CurrenUser.IsDepManager())
                {
                    qrEvents = qrEvents.Where(x => x.User.DeptId == CurrenUser.DeptId || (x.CRMCustomer.CRMFollowCusUsers != null &&
                                            x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == CurrenUser.Id)) ||
                                           (x.CRMCustomer.CreatedById == CurrenUser.Id)
                                            || (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == CurrenUser.Id)));
                }
                else
                {
                    qrEvents = qrEvents.Where(x => x.CreatedById == CurrenUser.Id ||
                                       (x.CRMCustomer.CRMFollowCusUsers != null &&
                                        x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == CurrenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == CurrenUser.Id) ||
                                       (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == CurrenUser.Id)));
                }
            }
            var list = qrEvents.ToList();
            Session[CALENDAR_SEARCH_MODEL] = filter;
            return View(list);
        }
        [HttpPost]
        public ActionResult CalendarView(string type, EventFilter filter)
        {
             ViewData["UserSalesList"] = usersServices.GetAllSales(CurrenUser, false);
            var currentFilter = (EventFilter)Session[CALENDAR_SEARCH_MODEL];
            if (filter.Month == 0) filter = currentFilter;
            var dateOfMonth = new DateTime(filter.Year, filter.Month, 1);
            if (!filter.Sales.HasValue)
            {
                filter.Sales = 0;
            }

            switch (type)
            {
                case "prev":
                    dateOfMonth = dateOfMonth.AddMonths(-1);
                    break;
                case "current":
                    dateOfMonth = DateTime.Now;
                    break;
                case "next":
                    dateOfMonth = dateOfMonth.AddMonths(1);
                    break;
            }
            filter.Month = dateOfMonth.Month;
            filter.Year = dateOfMonth.Year;
            var qrEvents =
                eventService.GetQuery(x => (x.DateBegin.Month == filter.Month && x.DateBegin.Year == filter.Year)
                                           && (filter.Sales == 0 || (filter.Sales == x.CreatedById ||
                                           (x.CRMCustomer.CreatedById == filter.Sales) ||
                                           (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == filter.Sales))))
                                           && (filter.Status == CRMEventStatus.All || x.Status == (byte)filter.Status)
                                           && (string.IsNullOrEmpty(filter.CustomerName) || x.CRMCustomer.CompanyShortName.Contains(filter.CustomerName) || x.CRMCustomer.CompanyName.Contains(filter.CustomerName))
                    );
            if (filter.OfEvent.HasValue)
                qrEvents = filter.OfEvent == TypeOfEvent.Events ? qrEvents.Where(x => x.IsEventAction == true) : qrEvents.Where(x => x.IsEventAction == false);
            if (!CurrenUser.IsDirecter() && filter.Sales == 0)
            {
                if (CurrenUser.IsDepManager())
                {
                    qrEvents = qrEvents.Where(x => x.User.DeptId == CurrenUser.DeptId || (x.CRMCustomer.CRMFollowCusUsers != null &&
                                            x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == CurrenUser.Id)) ||
                                           (x.CRMCustomer.CreatedById == CurrenUser.Id) ||
                                           (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == CurrenUser.Id)));
                }
                else
                {
                    qrEvents = qrEvents.Where(x => x.CreatedById == CurrenUser.Id ||
                                       (x.CRMCustomer.CRMFollowCusUsers != null &&
                                        x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == CurrenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == CurrenUser.Id) ||
                                       (x.CRMFollowEventUsers != null && x.CRMFollowEventUsers.Any(ef => ef.UserId == CurrenUser.Id)));
                }
            }

            var list = qrEvents.ToList();
            Session[CALENDAR_SEARCH_MODEL] = filter;
            ViewBag.filter = filter;
            return PartialView("_CalendarModelView", list);
        }

        public ActionResult SendEventEmail(long id)
        {
            var events = eventService.GetById(id);
            if (!events.UsersFollow.Any())
                return null;
            var to = events.UsersFollow.Where(x => string.IsNullOrEmpty(x.User.Email)).Select(x => x.User.Email).ToArray();
            if (!to.Any())
            {
                return Json(new
                {
                    Message = @"Gửi email thất bại! Lỗi: không tồn tại email người nhận ",
                    Success = false,
                }, JsonRequestBehavior.AllowGet);
            }
            var dept =
              usersServices.GetAll(
                  x => x.DeptId == CurrenUser.DeptId && x.RoleName == UsersModel.Positions.Manager.ToString());
            var ccEmail = string.Empty;
            ccEmail = dept.Where(u => string.IsNullOrEmpty(u.Email)).Aggregate(ccEmail, (current, u) => current + (u.Email + ","));
            var admin = usersServices.FindEntity(x => x.UserName.ToLower() == "admin");
            var user = !string.IsNullOrEmpty(CurrenUser.Email) ? CurrenUser : admin;
            var model = new EmailModel
            {
                EmailTo = string.Join(",", to),
                User = user,
                EmailCc = ccEmail + CurrenUser.Email,
                Message = string.Format("Bạn được yêu cầu cùng tham gia sự kiện {0} vào ngày {1:dd/MM/yyyy}", events.Subject, events.DateBegin),
                Subject = events.Subject
            };

            var email = new EmailCommon { EmailModel = model };
            string errorMessages;
            if (email.SendEmail(out errorMessages, true))
            {
                return Json(new
                {
                    Message = @"Gửi email thành công",
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                Message = @"Gửi email thất bại! Lỗi: " + errorMessages,
                Success = false,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserFollowDialog(long id, string name)
        {
            var customer = crmCustomerService.GetById(id);
            ViewBag.Customer = customer;
            var mode = followEventService.GetListModelsByCus(id);
            if (!mode.Any())
            {
                mode = new List<CRMFollowEventUserModel>();
            }
            var view = this.RenderPartialView("_UserFollowList", mode);
            var value = new
            {
                Views = view,
                CloseOther = true,
                Title = string.Format(@"Control user Follow for customer {0}", name),
            };
            return JsonResult(value, true);
        }
        [HttpPost]
        public ActionResult AddFollow(long visitId, long userId)
        {
            var exited = followEventService.Any(x => x.VisitId == visitId && x.UserId == userId);
            if (exited)
            {
                return Json(new
                {
                    Message = "Sale nay đã tồn tại trong theo dõi",
                    Success = false,
                    dialog = true
                }, JsonRequestBehavior.AllowGet);
            }
            var mode = new CRMFollowEventUserModel()
            {
                VisitId = visitId,
                UserId = userId,
                AddById = CurrenUser.Id
            };
            var id = followEventService.InsertToDb(mode);
            var db = followEventService.FindEntity(x => x.Id == id);
            var view = this.RenderPartialView("_UserFollowItem", db);
            var tr = string.Format("<tr id='user_{0}'>{1}</tr>", db.Id, view);
            return Json(tr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleleUserFollow(long id)
        {
            var db = followEventService.FindEntity(x => x.Id == id);
            if (db != null && !db.IsLook)
            {
                followEventService.DeleteToDb(id);
                if (CurrenUser.Id == db.UserId && CurrenUser.IsStaff())
                {
                    var value2 = new
                    {
                        Views = "You have ve leave out this event",
                        Title = "Success!",
                        FormClose = true,
                    };
                    return JsonResult(value2, true);
                }
                else
                {
                    var value = new
                    {
                        Views = "Bạn đã xoá thành công",
                        Title = "Success!",
                        IsRemve = true,
                        TdId = "del_" + id
                    };
                    return JsonResult(value, true);

                }

            }
            else
            {
                var result = new CommandResult(false)
                {
                    ErrorResults = new[] { "Sale is look by owner, you can not leave!" }
                };
                return JsonResult(result, null, true);
            }
        }
        public ActionResult SetLookUser(long id, bool isLook)
        {
            var db = followEventService.FindEntity(x => x.Id == id);
            if (db.IsLook && db.LockById != CurrenUser.DeptId && CurrenUser.IsDepManager() && !CurrenUser.IsAdmin())
            {
                var value = new
                {
                    Views = "This locked by director. Please contact him for unlock",
                    Title = "Error!",
                    ColumnClass = "col-md-6 col-md-offset-3"

                };
                return JsonResult(value, true);
            }
            followEventService.Look(id, isLook, CurrenUser);
            db = followEventService.FindEntity(x => x.Id == id);
            var view = this.RenderPartialView("_UserFollowItem", db);
            return Json(view, JsonRequestBehavior.AllowGet);
        }
    }
}