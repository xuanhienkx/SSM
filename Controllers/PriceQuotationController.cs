using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Common;
using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class PriceQuotationController : BaseController
    {
        private ICRMPriceQuotationService priceQuotationService;
        private static string PRICE_LIST_MODEL = "PRICE_LIST_MODEL";
        private Grid<CRMPriceQuotation> _grid;
        private static string EMAIL_LIST_MODEL = "EMAIL_LIST_MODEL";
        private Grid<CRMEmailHistoryModel> _gridEmailHist;
        private CRMSearchModel filterModel;
        private ICRMEmailHistoryService emailHistoryService;
        private ICRMCustomerService crmCustomerService;
        private UsersServices usersServices;
        private ICRMPriceStatusService priceStatusService;
        private PriceSearchModel priceSearch;
        private static string PRICE_SEARCH_MODEL = "PRICE_SEARCH_MODEL";
        public PriceQuotationController()
        {
            this.emailHistoryService = new CRMEmailHistoryService();
            this.priceQuotationService = new CRMPriceQuotationService();
            this.usersServices = new UsersServicesImpl();
            crmCustomerService = new CRMCustomerService();
            priceStatusService = new CRMPriceStatusService();
        }

        public ActionResult Index(long? refId)
        {
            _grid = (Grid<CRMPriceQuotation>)Session[PRICE_LIST_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<CRMPriceQuotation>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 50,
                                    Sord = "asc",
                                    Sidx = "Subject"
                                }
                            );
                _grid.SearchCriteria = new CRMPriceQuotation();
            }
            var searchModel = (PriceSearchModel)Session[PRICE_SEARCH_MODEL];
            priceSearch = searchModel ?? new PriceSearchModel() { DateType = "U" };
            ViewBag.SearchingModel = priceSearch;
            if (refId.HasValue)
                priceSearch.CusId = refId.Value; 
            GetBaseData();
            var sales = usersServices.GetAllSales(CurrenUser, false);
            var depts = usersServices.GetAllDepartmentActive(CurrenUser);
            ViewBag.AllSales = new SelectList(sales, "Id", "FullName");
            ViewBag.AllDept = new SelectList(depts, "Id", "DeptName");
            UpdateGrid(); 
            Session[PRICE_LIST_MODEL] = _grid;
            return View(_grid);
        }
        [HttpPost]
        public ActionResult Index(PriceSearchModel model, Grid<CRMPriceQuotation> grid)
        {
            _grid = grid;
            priceSearch = model;
            Session[PRICE_SEARCH_MODEL] = model;
            UpdateGrid(); 
            ViewBag.SearchingModel = model;
            return PartialView("_List", _grid);
        }

        private void UpdateGrid()
        {
            var totalRow = 0;
            var page = _grid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "Subject" : page.Sidx, page.Sord == "asc");
            IEnumerable<CRMPriceQuotation> customers = priceQuotationService.GetAll(priceSearch, sort, out totalRow, page.CurrentPage, page.PageSize, CurrenUser);
            _grid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _grid.Data = new List<CRMPriceQuotation>();
                ViewBag.TotalDisplay = string.Empty;
                return;
            }
            var crmPriceQuotations = customers as IList<CRMPriceQuotation> ?? customers.ToList();
            _grid.Data = crmPriceQuotations;
            
            var cusFollow = crmPriceQuotations.Count(x => x.CRMPriceStatus.Code == (byte)CRMPriceStaus.Following && x.CreatedById== CurrenUser.Id);
            var cancle = crmPriceQuotations.Count(x => x.CRMPriceStatus.Code == (byte)CRMPriceStaus.Cancel && x.CreatedById == CurrenUser.Id);
            var cusFinish = crmPriceQuotations.Count(x => x.CRMPriceStatus.Code == (byte)CRMPriceStaus.Finished && x.CreatedById == CurrenUser.Id);
            string totalDisplay =
                string.Format(Resources.Resource.CRM_PRICE_LIST_TOTAL,
                    totalRow, cusFollow, cusFinish, cancle);
            ViewBag.TotalDisplay = totalDisplay;
        }

        private CRMCustomerModel crmCustomer;
        public ActionResult Create(long refId)
        {
            crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(refId);
            var model = new CRMPriceQuotationModel { StatusId = (int)CRMPriceStaus.Following, CrmCusId = refId, IsCusCreate = refId > 0 };
            if (model.IsCusCreate)
            {
                model.CrmCusName = "fromCus";
            }
            GetBaseData();
            var value = new
            {
                Views = this.RenderPartialView("_Create", model),
                Title = string.Format(@"{0}", "Tạo báo giá cho khách hàng " + crmCustomer.CompanyShortName),
            };
          
            return JsonResult(value, true);
        }

        private void GetBaseData()
        {
            var status = priceStatusService.GetAll().OrderBy(x => x.Name);
            ViewBag.PriceStatus = new SelectList(status, "Id", "Name");
        }
        [HttpPost]
        public ActionResult Create(CRMPriceQuotationModel model)
        {
            if (model.CrmCusId > 0)
            {
                model.CrmCusName = "forcust";
            }
            GetBaseData();
            if (!ModelState.IsValid)
            {
                crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(model.CrmCusId);

                var value = new
                {
                    Views = this.RenderPartialView("_Create", model),
                    ColumnClass = "col-md-11",
                    Title = string.Format(@"{0}", "Tạo báo giá cho khách hàng " + crmCustomer.CompanyShortName),
                };
                return JsonResult(value, true);
            }
            model.CreatedBy = CurrenUser;
            model.CreatedDate = DateTime.Now;
            var id = priceQuotationService.InsertModel(model);
            var result = new CommandResult(true)
            {
                ErrorResults = new[] { "Tạo báo giá thành công" }
            };
            return JsonResult(result, null, true);
        }

        public ActionResult ListByCus(long refId)
        {
            var sort = new SSM.Services.SortField("Subject", true);
            var totalRow = 0;
            var search = new PriceSearchModel() { CusId = refId };
            IEnumerable<CRMPriceQuotation> customers = priceQuotationService.GetAll(search, sort, out totalRow, 1, 100, CurrenUser);
            crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(refId);
            var value = new
            {
                Views = this.RenderPartialView("_ListForCus", customers),
                CloseOther = true,
                Title = string.Format(@"{0}", "List Bao gia cua khách hàng " + crmCustomer.CompanyShortName),
            };
            return JsonResult(value, true);
        }
        public ActionResult Edit(long id)
        {
            var model = priceQuotationService.GetModel(id);
            GetBaseData();
            var value = new
            {
                Views = this.RenderPartialView("_Edit", model),
                ColumnClass = "col-md-11",
                Title = string.Format(@"{0}", "Sữa báo giá khách hàng " + model.CRMCustomer.CompanyShortName),
            };
            return JsonResult(value, true);

        }
        [HttpPost]
        public ActionResult Edit(CRMPriceQuotationModel model)
        {
            GetBaseData();
            if (!ModelState.IsValid)
            {
                crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(model.CrmCusId);
                var value = new
                {
                    Views = this.RenderPartialView("_Edit", model),
                    ColumnClass = "col-md-11",
                    Title = string.Format(@"{0}", "Sữa báo giá khách hàng " + crmCustomer.CompanyShortName),
                };
                return JsonResult(value, true);
            }
            model.ModifiedBy = CurrenUser;
            model.ModifiedDate = DateTime.Now;
            priceQuotationService.UpdateModel(model);
            var result = new CommandResult(true)
            {
                ErrorResults = new[] { "sữa báo giá thành công" }
            };
            return JsonResult(result);
        }

        public ActionResult PriceQuotationSendMail(long id)
        {
            var model = new EmailModel
            {
                User = CurrenUser,
                IdRef = id
            };
            var price = priceQuotationService.GetModel(id);
            var cus = price.CRMCustomer;
            var toAddress = string.Empty;
            var ctacts = cus.CRMContacts.ToList();
            foreach (var ctact in ctacts)
            {
                toAddress += ctact.Email;
                toAddress += ",";
                if (string.IsNullOrEmpty(ctact.Email2)) continue;
                toAddress += ctact.Email2;
                toAddress += ",";
            }
            model.EmailTo = toAddress;
            var value = new
            {
                Views = this.RenderPartialView("_EmailFormTemplateView", model),
                Title = string.Format(@"{0}", "Gữi email báo giá cho  " + cus.CompanyShortName),
            };
            return JsonResult(value, true);
            //return PartialView("_EmailFormTemplateView", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult PriceQuotationSendMail(EmailModel model)
        {

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Message = @"Gửi email thất bại",
                    Success = false,
                    View = this.RenderPartialView("_EmailFormTemplateView", model)
                }, JsonRequestBehavior.AllowGet);
            }
            var errorMessages = string.Empty;
            model.EmailTo = model.EmailTo.Replace(";", ",");
            foreach (var emailAddress in model.EmailTo.Split(',').Where(x => !string.IsNullOrEmpty(x)))
            {
                var errorMessage = string.Empty;
                if (!emailAddress.IsValid(out errorMessage))
                    errorMessages += string.Format("Invalid: {0} Error: {1}", emailAddress, errorMessage);
            }

            if (!string.IsNullOrEmpty(errorMessages))
            {
                ModelState.AddModelError(string.Empty, errorMessages);
                return Json(new
                {
                    Message = @"Gửi email thất bại xem chi tiết lỗi",
                    Success = false,
                    View = this.RenderPartialView("_EmailFormTemplateView", model)
                }, JsonRequestBehavior.AllowGet);
            }
            var admin = usersServices.FindEntity(x => x.UserName.ToLower() == "admin");
            var user = !string.IsNullOrEmpty(CurrenUser.Email) ? CurrenUser : admin;
            model.User = user;
            var ccEmail = string.Empty;
            var dept =
                usersServices.GetAll(
                    x => x.DeptId == CurrenUser.DeptId && x.RoleName == UsersModel.Positions.Manager.ToString());
            ccEmail = dept.Where(u => string.IsNullOrEmpty(u.Email)).Aggregate(ccEmail, (current, u) => current + (u.Email + ","));
            model.EmailCc = ccEmail + CurrenUser.Email;
            var email = new EmailCommon { EmailModel = model };
            if (email.SendEmail(out errorMessages, true))
            {
                var emailHistory = new CRMEmailHistoryModel
                {
                    CcAddress = model.EmailCc,
                    ToAddress = model.EmailTo,
                    Subject = model.Subject,
                    DateSend = DateTime.Now,
                    PriceId = model.IdRef
                };
                emailHistoryService.InsertModel(emailHistory);
                priceQuotationService.UpdateSendMail(model.IdRef);
                return Json(new
                {
                    Message = @"Gửi email thành công",
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            ModelState.AddModelError(string.Empty, errorMessages);
            return Json(new
            {
                Message = @"Gửi email thất bại xem chi tiết lỗi",
                Success = false,
                View = this.RenderPartialView("_EmailFormTemplateView", model)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailHistoryOfPrice(long id)
        {
            var price = priceQuotationService.GetModel(id);
            return PartialView("_EmailHistoryListOfPrice", price);
        }
        public ActionResult EmailList(long priceId)
        {
            _gridEmailHist = (Grid<CRMEmailHistoryModel>)Session[EMAIL_LIST_MODEL];
            if (_gridEmailHist == null)
            {
                _gridEmailHist = new Grid<CRMEmailHistoryModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 8,
                                    Sord = "desc",
                                    Sidx = "DateSend"
                                }
                            );
                _gridEmailHist.SearchCriteria = new CRMEmailHistoryModel();
            }
            UpdateGridEmailHist(priceId);
            return PartialView("_listEmail", _gridEmailHist);
        }
        private void UpdateGridEmailHist(long idPrice)
        {
            var price = priceQuotationService.GetModel(idPrice);
            if (price == null)
            {
                return;
            }
            var totalRow = 0;
            var page = _gridEmailHist.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "Subject" : page.Sidx, page.Sord == "asc");
            IEnumerable<CRMEmailHistoryModel> emaiList = emailHistoryService.GetAll(sort, out totalRow, page.CurrentPage, page.PageSize, idPrice);
            _gridEmailHist.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _gridEmailHist.Data = new List<CRMEmailHistoryModel>();
                ViewBag.TotalDisplay = string.Empty;
                return;
            }
            _gridEmailHist.Data = emaiList;
            ViewBag.PriceQuotation = price ?? new CRMPriceQuotationModel();
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var emailhist = emailHistoryService.GetAll(x => x.PriceId == id);
                if (emailhist.Any())
                {
                    emailHistoryService.DeleteAll(emailhist);
                }
                priceQuotationService.DeletePrice(id);
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
    }
}