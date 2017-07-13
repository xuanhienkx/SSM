using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Common;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class ProductController : Controller
    {
        private IProductServices productServices;
        private GridNew<Product, ProductModel> gridView;
        private User currentUser;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            productServices = new ProductServices();
            currentUser = (User)Session[AccountController.USER_SESSION_ID];
        }

        public ActionResult Index(long id = 0)
        {
            gridView = new GridNew<Product, ProductModel>(
                new Pager
                {
                    CurrentPage = 1,
                    PageSize = 10,
                    Sord = "asc",
                    Sidx = "Name"
                });
            UpdateGrid();
            gridView.SearchCriteria =new Product(); 
            return View(gridView);
        }

        [HttpPost]
        public ActionResult Index(GridNew<Product, ProductModel> grid)
        {
            gridView = grid;
            gridView.ProcessAction(); 
            UpdateGrid();
            return PartialView("_productList",gridView);
        }

        public void UpdateGrid()
        {
            var page = gridView.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "Name" : page.Sidx, page.Sord == "asc");
            var prolist = productServices.GetAll(gridView.SearchCriteria);
            prolist = prolist.OrderBy(sort);
            int totalRows = prolist.Count();
            gridView.Pager.Init(totalRows);
            if (totalRows == 0)
            {
                gridView.Data = new List<Product>();
                return;
            }
            var list = productServices.GetListPager(prolist, page.CurrentPage, page.PageSize);
            gridView.Data = list;
        }

        public FileContentResult GetImange(long id)
        {
            var fileToRetrieve = productServices.GetProduct(id);
            if (fileToRetrieve.Image == null)
            {
                return null;
            }
            return File(fileToRetrieve.Image.ToArray(), fileToRetrieve.ContentType);
        }


        private bool CheckExistsCode(long id, string code)
        {
            var db = productServices.Count(x => x.Id != id && x.Code.ToLower() == code.ToLower());
            if (db > 0)
                return true;
            return false;
        }

        public ActionResult Delete(long id)
        {
            try
            {
                productServices.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                ViewBag.MessageErr = ex.Message;
            }

            return RedirectToAction("Index", new { id = 0 });
        }

        public ActionResult Edit(long id, bool isDetail = false)
        {
            
            if (id == 0)
                return PartialView("_formEdit", new ProductModel());
            var pro = productServices.GetProductModel(id);
            if (isDetail)
            {
                ViewBag.IsDatail = true;
            }
            return PartialView("_formEdit", pro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (CheckExistsCode(model.Id, model.Code))
                {
                    ViewBag.MessageErr = string.Format("The code: {0} had exists on orther product!", model.Code);
                    return PartialView("_formEdit", model);
                }
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        model.Image = reader.ReadBytes(upload.ContentLength);
                        model.FileName = upload.FileName;
                        model.ContentType = upload.ContentType;
                    }
                }
                if (model.Id > 0)
                {
                    model.ModifiedBy = currentUser.Id;
                    model.DateModify = DateTime.Now;
                    productServices.UpdateProduct(model);
                }
                else
                {
                    model.CreatedBy = currentUser.Id;
                    model.DateCreate = DateTime.Now;
                    productServices.InsertProduct(model);
                }
                return Json(1);
            }
            return PartialView("_formEdit", model);
        }
        // convert image to byte array
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        //Byte array to photo
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}