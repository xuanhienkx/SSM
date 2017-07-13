using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using SSM.Models;

namespace SSM.Services
{
    public interface ISupplierServices : IServices<Supplier>
    {
        IQueryable<Supplier> GetAll(Supplier model);
        IEnumerable<Supplier> GetAll(Supplier model, SSM.Services.SortField sort, out int rows, int page, int pageSize);
        Supplier GetSupplier(long id);
        SupplierModels GetSupplierModel(long id);
        void DeleteSupplier(long id);
        void InsertSupplier(SupplierModels supplierModel);
        void UpdateSupplier(SupplierModels supplierModel);

    }

    public class SupplierSerivecs : Services<Supplier>, ISupplierServices
    { 
        public IQueryable<Supplier> GetAll(Supplier model)
        {
            var qr = GetQuery(s =>
                    (string.IsNullOrEmpty(model.CompanyName) || (s.CompanyName.Contains(model.CompanyName)))
                 && (string.IsNullOrEmpty(model.FullName) || (s.FullName.Contains(model.FullName)))
                 && (string.IsNullOrEmpty(model.Address) || (s.Address.Contains(model.Address)))
                 && (!model.CountryId.HasValue || s.CountryId == model.CountryId)
               );
            return qr;
        }

        public IEnumerable<Supplier> GetAll(Supplier model, SortField sort, out int rows, int page, int pageSize)
        {
            var qr = GetQuery(s =>
                     (string.IsNullOrEmpty(model.CompanyName) || (s.CompanyName != null && s.CompanyName.Contains(model.CompanyName)))
                  && (string.IsNullOrEmpty(model.FullName) || (s.FullName != null && s.FullName.Contains(model.FullName)))
                  && (string.IsNullOrEmpty(model.Address) || (s.Address != null && s.Address.Contains(model.Address)))
                  && (!model.CountryId.HasValue || s.CountryId == model.CountryId)
                );
            qr = qr.OrderBy(sort);
            rows = qr.Count();
            var list = GetListPager(qr, page, pageSize);
            return list;
        }

        public Supplier GetSupplier(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }

        public SupplierModels GetSupplierModel(long id)
        {
            return ToModels(GetSupplier(id));
        }

        public void DeleteSupplier(long id)
        {
            var supplier = GetSupplier(id);
            if (supplier == null)
                throw new SqlNotFilledException("Not found supplier with id");
            Delete(supplier);
            Commited();
        }

        public void InsertSupplier(SupplierModels supplierModel)
        {
            var model = ToDbModel(supplierModel);
           Insert(model);
            Commited();
        }

        public void UpdateSupplier(SupplierModels supplierModel)
        {
            var dbSupplier = GetSupplier(supplierModel.Id);
            if (dbSupplier == null)
                throw new SqlNotFilledException("Not found supplier with id");
            CoppyToDbMode(supplierModel, dbSupplier);
            Commited();
        }

        private void CoppyToDbMode(SupplierModels model, Supplier supplier)
        {
            supplier.Id = model.Id;
            supplier.Address = model.Address;
            supplier.CompanyName = model.CompanyName;
            supplier.Description = model.Description;
            supplier.Email = model.Email;
            supplier.FullName = model.FullName;
            supplier.Fax = model.Fax;
            supplier.PhoneNumber = model.PhoneNumber;
            supplier.CountryId = model.CountryId;
            supplier.ModifiedBy = model.ModifiedBy;
            supplier.DateModify = model.DateModify;
        }
        public Supplier ToDbModel(SupplierModels model)
        {
            var db = new Supplier()
            {
                Id = model.Id,
                Address = model.Address,
                CompanyName = model.CompanyName,
                Description = model.Description,
                Email = model.Email,
                FullName = model.FullName,
                Fax = model.Fax,
                PhoneNumber = model.PhoneNumber,
                CountryId = model.CountryId,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                DateCreate = model.DateCreate,
                DateModify = model.DateModify
            };
            return db;
        }

        public SupplierModels ToModels(Supplier supplier)
        {
            if (supplier == null) return null;
            return Mapper.Map<SupplierModels>(supplier);
        }
    }
}