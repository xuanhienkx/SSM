using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMStatusService : IServices<CRMStatus>
    {
        CRMBaseModel InsertCRMGroup(CRMBaseModel model);
        void UpdateCRMGroup(CRMBaseModel model);
        void DeleteCRMGroup(CRMBaseModel model);
        CRMBaseModel GetModel(int id);
        CRMBaseModel GetModel(CRMStatusCode code);
        IEnumerable<CRMBaseModel> GetList(SortField sort, string name);
    }
    public class CRMStatusService : Services<CRMStatus>, ICRMStatusService
    {
        public IEnumerable<CRMBaseModel> GetList(SortField sort, string name)
        {
            var list = GetQuery(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).OrderBy(sort).ToList();
            return list.Select(x => Mapper.Map<CRMBaseModel>(x));
        }
        public CRMBaseModel InsertCRMGroup(CRMBaseModel model)
        {
            var db = new CRMStatus { Name = model.Name, Description = model.Description, IsSystem = false, Code = (byte)CRMStatusCode.Orther };
            var pr = Insert(db);
            return Mapper.Map<CRMBaseModel>(pr);  
        }

        public void UpdateCRMGroup(CRMBaseModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            db.Name = model.Name;
            db.Description = model.Description;
            //db.IsSystem = model.IsSystem;
            //db.Code = (byte)model.Code;
            Commited();
        }

        public void DeleteCRMGroup(CRMBaseModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            Delete(db);
        }

        public CRMBaseModel GetModel(int id)
        {
            var db = FindEntity(x => x.Id == id);
            return Mapper.Map<CRMBaseModel>(db);
        }

        public CRMBaseModel GetModel(CRMStatusCode code)
        {
            var db = FindEntity(x => x.Code == (byte)code);
            return Mapper.Map<CRMBaseModel>(db);
        }
    }
    public interface ICRMPriceStatusService : IServices<CRMPriceStatus>
    {
        CRMBaseModel InsertModel(CRMBaseModel model);
        void UpdateModel(CRMBaseModel model);
        void DeleteModel(CRMBaseModel model);
        IEnumerable<CRMBaseModel> GetList(SortField sort, string name);
    }

    public class CRMPriceStatusService : Services<CRMPriceStatus>, ICRMPriceStatusService
    {
        public IEnumerable<CRMBaseModel> GetList(SortField sort, string name)
        {
            var list = GetQuery(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).OrderBy(x=>x.Name).ToList();
            return list.Select(x => Mapper.Map<CRMBaseModel>(x));
        }
        public CRMBaseModel InsertModel(CRMBaseModel model)
        {
            var db = new CRMPriceStatus { Name = model.Name, Description = model.Description, IsSystem = false, Code = (byte)CRMStatusCode.Orther };
            var pr = Insert(db);
            return Mapper.Map<CRMBaseModel>(pr);
        }

        public void UpdateModel(CRMBaseModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            db.Name = model.Name;
            db.Description = model.Description;
           // db.IsSystem = model.IsSystem;
           // db.Code = (byte)model.Code;
            Commited();
        }

        public void DeleteModel(CRMBaseModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            Delete(db);
        }
    }
}