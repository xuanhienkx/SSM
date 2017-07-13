using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Resources;
using SSM.Common;

using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class CRMDocumentController : BaseController
    {
        private ICRMDocumentService documentService;
        private ICRMCustomerService crmCustomerService;
        private static string DOC_LIST_MODEL = "DOC_LIST_MODEL";
        private Grid<CrmCusDocumentModel> _grid;
        private static String DOCUMENT_PATH = "/FileManager/CRMDocument";
        private IServerFileService fileService;
        private UsersServices usersServices;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            documentService = new CRMDocumentService();
            crmCustomerService = new CRMCustomerService();
            fileService = new ServerFileService();
            usersServices = new UsersServicesImpl();
        }
        public ActionResult Index()
        {
            _grid = (Grid<CrmCusDocumentModel>)Session[DOC_LIST_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<CrmCusDocumentModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 50,
                                    Sord = "asc",
                                    Sidx = "DocName"
                                }
                            );
                _grid.SearchCriteria = new CrmCusDocumentModel();
            }
            UpdateGrid();
            ViewData["Departments"] = usersServices.GetAllDepartmentActive(CurrenUser);
            return View(_grid);
        }
        [HttpPost]
        public ActionResult Index(Grid<CrmCusDocumentModel> grid, CRMSearchModel fiter)
        {
            _grid = grid;
            UpdateGrid(fiter);
            Session[DOC_LIST_MODEL] = _grid;
            ViewData["Departments"] = usersServices.GetAllDepartmentActive(CurrenUser);
            return PartialView("_List", _grid);
        }
        
        private void UpdateGrid(CRMSearchModel filter = null)
        {
            var totalRow = 0;
            filter = filter ?? new CRMSearchModel();
            var page = _grid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "Subject" : page.Sidx, page.Sord == "asc");
            IEnumerable<CrmCusDocumentModel> document = documentService.GetAll(filter, sort, out totalRow, page.CurrentPage, page.PageSize, CurrenUser);
            _grid.Pager.Init(totalRow);
            ViewBag.SearchingMode = filter;
            var sales = usersServices.GetAllSales(CurrenUser, false);
            ViewBag.AllSales = new SelectList(sales, "Id", "FullName");
            if (totalRow == 0)
            {
                _grid.Data = new List<CrmCusDocumentModel>();
                ViewBag.TotalDisplay = string.Empty;
                return;
            }
            _grid.Data = document;
          
            string totalDisplay =
                string.Format("Tổng cộng:{0} tài liệu", totalRow);
            ViewBag.TotalDisplay = totalDisplay;
        }

        public JsonResult ListForCus(long refId)
        {
            var sort = new SSM.Services.SortField("DocName", true);
            var totalRow = 0;
            //  var search = new PriceSearchModel() { CusId = cusId, PriceStaus = CRMPriceStaus.All };
            var filter = new CRMSearchModel() { Id = refId };
            IEnumerable<CrmCusDocumentModel> documentModels = documentService.GetAll(filter, sort, out totalRow, 1, int.MaxValue, CurrenUser);
            crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(refId);
            var value = new
            {
                Views = this.RenderPartialView("_ListForCus", documentModels),
                Title = string.Format(@"{0}", "Danh sách tài liệu của khách hàng " + crmCustomer.CompanyShortName),
            };
            return JsonResult(value, true);
        }

        private CRMCustomerModel crmCustomer;
        public ActionResult Edit(long cusId, long id = 0)
        {
            var model = new CrmCusDocumentModel { CrmCusId = cusId, FilesList = new List<ServerFile>() };
            var doc = documentService.GetModel(id);
            crmCustomer = crmCustomer ?? crmCustomerService.GetModelById(cusId);
            if (doc != null)
            {
                model = doc;
                model.FilesList = fileService.GetServerFile(id, new CrmCusDocumentModel().GetType().ToString());
            }
            var value = new
            {
                Views = this.RenderPartialView("_TemplateEditView", model),
                Title = string.Format(@"{0} tài liệu {1} ", id > 0 ? "Sửa " : "Tạo ", crmCustomer.CompanyShortName),
            };
            return JsonResult(value, true);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CrmCusDocumentModel model)
        {
            model.FilesList = new List<ServerFile>();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
                return Json(new
                {
                    Message = Resources.Resource.CRM_EDIT_ERROR_MESSAGE_BLANK,
                    Success = false,
                    View = this.RenderPartialView("_TemplateEditView", model)
                }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (model.Id == 0)
                {
                    model.CreatedById = CurrenUser.Id;
                    model.Id = documentService.InsertModel(model);
                }
                else
                {
                    model.ModifiedById = CurrenUser.Id;
                    documentService.UpdateModel(model);
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
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new
                {
                    Message = Resources.Resource.CRM_EDIT_ERROR_MESSAGE,
                    Success = false,
                    View = this.RenderPartialView("_TemplateEditView", model)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private void UploadFile(CrmCusDocumentModel model, List<HttpPostedFileBase> filesUpdoad)
        {
            if (filesUpdoad == null || !filesUpdoad.Any()) return;
            var type = model.GetType().ToString();
            foreach (var file in filesUpdoad.Where(file => file != null && file.ContentLength > 0))
            {
                try
                {
                    var pathfoder = Path.Combine(Server.MapPath(@"~/" + DOCUMENT_PATH), model.CrmCusId.ToString("D6"), type, model.Id.ToString("D4"));
                    if (!Directory.Exists(pathfoder))
                    {
                        Directory.CreateDirectory(pathfoder);
                    }
                    var filePath = Path.Combine(pathfoder, Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);
                    var fileName = file.FileName;
                    if (file.FileName.Length > 100)
                    {
                        var fileList = file.FileName.Split('.');
                        var typeDoc = fileList[fileList.Length - 1];
                        fileName = file.FileName.Substring(0, 95) + "." + typeDoc;
                    }
                    //save file to db
                    var fileSave = new ServerFile
                    {
                        ObjectId = model.Id,
                        ObjectType = type,
                        //  Path = string.Format("{0}/{1}", pathfoder, file.FileName),
                        Path = string.Format("{0}/{1}/{2}/{3}/{4}", DOCUMENT_PATH, model.CrmCusId.ToString("D6"), type, model.Id.ToString("D4"), file.FileName),
                        FileName = fileName,
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

        public ActionResult Delete(long id)
        {
            try
            {
                documentService.DeleteModel(id);
                var filelist = fileService.GetServerFile(id, new CrmCusDocumentModel().GetType().ToString());
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
        public ActionResult Download(long id)
        {
            var document = fileService.GetById(id);
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
                var file = fileService.GetById(id);
                if (file != null)
                    if (System.IO.File.Exists(Server.MapPath(file.Path)))
                        System.IO.File.Delete(Server.MapPath(file.Path));
                fileService.Delete(file);
                var value = new
                {
                    Views = "Bạn đã xoá thành công",
                    Title = "Success!",
                    ColumnClass= "col-md-6 col-md-offset-3",
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