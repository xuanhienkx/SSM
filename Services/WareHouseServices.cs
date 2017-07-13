using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using AutoMapper;
using SSM.Models;

namespace SSM.Services
{
    public interface IWarehouseSevices
    {
        IEnumerable<Warehouse> GetAll(bool cache = false);
        IEnumerable<Warehouse> GetAll(Warehouse model);
        Warehouse GetWarehouse(long id);
        WareHouseModel GetWareHouseModel(long id);
        void DeleteWareHouse(long id);
        void InsertWareHouse(WareHouseModel model);
        void UpdateWareHouse(WareHouseModel model);
    }
    public class WareHouseServices : IWarehouseSevices
    {
        private readonly DataClasses1DataContext context;

        public WareHouseServices()
        {
            this.context = new DataClasses1DataContext();
        }

        private IEnumerable<Warehouse> warehouses;
        public IEnumerable<Warehouse> GetAll(bool cache = false)
        {
            if (cache == true && warehouses != null)
                return warehouses;
            return warehouses = context.Warehouses.ToList();
        }

        public IEnumerable<Warehouse> GetAll(Warehouse model)
        {
            return from wh in context.Warehouses
                   where (string.IsNullOrEmpty(model.Code) || wh.Code.Contains(model.Code))
                         && (string.IsNullOrEmpty(model.Address) || wh.Address.Contains(model.Address))
                         && (string.IsNullOrEmpty(model.Name) || wh.Address.Contains(model.Name))
                   select wh;
        }

        public Warehouse GetWarehouse(long id)
        {
            return context.Warehouses.FirstOrDefault(x => x.Id == id);
        }

        public WareHouseModel GetWareHouseModel(long id)
        {
            return ToModels(GetWarehouse(id));
        }

        public void DeleteWareHouse(long id)
        {
            var dbWh = GetWarehouse(id);
            if (dbWh == null)
                throw new SqlNotFilledException("Not found Warehouse with id");
            context.Warehouses.DeleteOnSubmit(dbWh);
            context.SubmitChanges();
        }

        public void InsertWareHouse(WareHouseModel model)
        {
            var dbModel = ToDbModel(model);
            context.Warehouses.InsertOnSubmit(dbModel);
            context.SubmitChanges();
        }

        public void UpdateWareHouse(WareHouseModel medel)
        {
            var dbWh = GetWarehouse(medel.Id);
            if (dbWh == null)
                throw new SqlNotFilledException("Not found Warehouse with id");
            CoppyToDbMode(medel, dbWh);
            context.SubmitChanges();
        }
        private WareHouseModel ToModels(Warehouse model)
        {
            if (model == null) return null;
            return Mapper.Map<WareHouseModel>(model);
        }
        private void CoppyToDbMode(WareHouseModel model, Warehouse warehouse)
        {
            warehouse.Id = model.Id;
            warehouse.Address = model.Address;
            warehouse.Description = model.Description;
            warehouse.Email = model.Email;
            warehouse.Name = model.Name;
            warehouse.Code = model.Code;
            warehouse.AreaId = model.AreaId;
            warehouse.Fax = model.Fax;
            warehouse.PhoneNumber = model.PhoneNumber;
            warehouse.ModifiedBy = model.ModifiedBy;
            warehouse.DateModify = model.DateModify;
        }
        private Warehouse ToDbModel(WareHouseModel model)
        {
            var db = new Warehouse()
            {
                Id = model.Id,
                Address = model.Address,
                Description = model.Description,
                Email = model.Email,
                Name = model.Name,
                Code = model.Code,
                Fax = model.Fax,
                PhoneNumber = model.PhoneNumber,
                AreaId = model.AreaId,
                CreatedBy = model.CreatedBy,
                ModifiedBy = model.ModifiedBy,
                DateCreate = model.DateCreate,
                DateModify = model.DateModify
            };
            return db;
        }
    }
}