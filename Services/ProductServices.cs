using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using SSM.Models;
using SSM.ViewModels;
using WebGrease.Css.Ast.Selectors;

namespace SSM.Services
{
    public interface IProductServices:IServices<Product>
    { 
        IEnumerable<StockRecevingDiagloModel> GetSuggeCode(string stockCode);
        List<StockRecevingDiagloModel> GetAll(string name, int? top, bool meager=true);
        IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter, int? top);
        IQueryable<Product> GetAll(Product product);
        Product GetProduct(long id);
        ProductModel GetProductModel(long id);
        void DeleteProduct(long id);
        void InsertProduct(ProductModel supplierModel);
        void UpdateProduct(ProductModel supplierModel);
        bool Exists(string name);


    }
    public class ProductServices :Services<Product>, IProductServices
    {
        
        

        public IEnumerable<StockRecevingDiagloModel> GetSuggeCode(string stockCode)
        {
            var result = (from p in Context.Products
                          where (p.Code.Contains(stockCode) && (p.Uom != null && p.Uom.Trim() != ""))
                          orderby p.Code
                          select new StockRecevingDiagloModel
                          {
                              Id = p.Id,
                              Display =   p.Code.Trim(),
                              Other = p.Uom,
                              DbMode = "Product"
                          }).Take(20).ToList();

            return result;
        }

       
        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter, int? top)
        {
            if (top.HasValue)
                return Context.Products.Where(filter).OrderBy(x => x.Code).Take(top.Value).ToList();
            return Context.Products.Where(filter).OrderBy(x => x.Code).ThenBy(x=>x.Name).ToList();
        }

        public IQueryable<Product> GetAll(Product product)
        {
            product = product ?? new Product();
            var qr = GetQuery(p =>
                (string.IsNullOrEmpty(product.Code) || p.Code.Contains(product.Code))
                && (string.IsNullOrEmpty(product.Name) || p.Name.Contains(product.Name))
                );

            return qr;
        }

        public List<StockRecevingDiagloModel> GetAll(string name, int? top, bool meager = true)
        {
            if (!top.HasValue)
                top = int.MaxValue;
            var result = (from p in Context.Products
                          where (p.Name.Contains(name) || p.Code.Contains(name)) && (p.Uom != null && p.Uom.Trim() != "")
                          orderby p.Code
                          select new StockRecevingDiagloModel
                          {
                              Id = p.Id,
                              Display = meager ? p.Code.Trim() + "-" + p.Name.Trim() : p.Name.Trim(),
                              Other = p.Uom,
                              DbMode = "Product"

                          }).Take(top.Value).ToList();

            return result;
        }

        public bool Exists(string name)
        {
            var check = (from p in Context.Products
                         where (p.Code.Trim() + "-" + p.Name.Trim()) == name
                         select p.Id).FirstOrDefault();
            return check > 0;
        }
        public Product GetProduct(long id)
        {
            return Context.Products.FirstOrDefault(x => x.Id == id);
        }

        public ProductModel GetProductModel(long id)
        {
            var db = GetProduct(id);
            if (db == null)
                return null;
            var model = ToModels(db);
            if (db.SupplierId.HasValue)
                model.SupplierName = db.Supplier.FullName;
            return model;
        }

        public void DeleteProduct(long id)
        {
            var dbWh = GetProduct(id);
            if (dbWh == null)
                throw new SqlNotFilledException("Not found Warehouse with id");
            Context.Products.DeleteOnSubmit(dbWh);
            Commited();
        }

        public void InsertProduct(ProductModel model)
        {
            var dbModel = ToDbModel(model);
            Context.Products.InsertOnSubmit(dbModel);
            Commited();
        }

        public void UpdateProduct(ProductModel medel)
        {
            var dbWh = GetProduct(medel.Id);
            if (dbWh == null)
                throw new SqlNotFilledException("Not found Warehouse with id");
            CoppyToDbMode(medel, dbWh);
            Commited();
        }



        private ProductModel ToModels(Product product)
        {
            if (product == null) return null;
            return Mapper.Map<ProductModel>(product);
        }
        private void CoppyToDbMode(ProductModel model, Product dbModel)
        {
            dbModel.Id = model.Id;
            dbModel.Description = model.Description;
            dbModel.Code = model.Code.Trim();
            if (model.Image != null)
            {
                dbModel.Image = new System.Data.Linq.Binary(model.Image);
                dbModel.FileName = model.FileName;
                dbModel.ContentType = model.ContentType;
            }
            dbModel.Name = model.Name;
            dbModel.NameEnglish = model.NameEnglish;
            dbModel.Uom = model.Uom;
            dbModel.SupplierId = model.SupplierId;
            dbModel.ModifiedBy = model.ModifiedBy;
            dbModel.DateModify = model.DateModify;
        }
        private Product ToDbModel(ProductModel model)
        {
            var db = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                NameEnglish = model.NameEnglish,
                Code = model.Code.Trim(),
                SupplierId = model.SupplierId,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                DateCreate = model.DateCreate,
                DateModify = model.DateModify,
                ContentType = model.ContentType,
                Uom = model.Uom,
                FileName = model.FileName,
                Image = model.Image != null ? new System.Data.Linq.Binary(model.Image) : null
            };
            return db;
        }
    }
}