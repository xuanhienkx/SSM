using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.ViewModels;

namespace SSM.Services
{
    public interface IServicesTypeServices : IServices<ServicesType>
    {
        void Create(ServicesViewModel model);
        void Update(ServicesViewModel model);
        IQueryable<ServicesType> GetQuerys(ServicesType mode);
        ServicesViewModel GetModelById(int id);
        int GetId(string serviceName);
        List<ServicesType> ListService();
        bool CheckServiceFree(int id);
        bool SetActive(int id, bool isActive);
        void Delete(int id);
    }
    public class ServicesTypeServices : Services<ServicesType>, IServicesTypeServices
    {
        public void Create(ServicesViewModel model)
        {
            var db = new ServicesType
            {
                DateCreate = DateTime.Now,
                Name = model.Name,
                SerivceName = model.SerivceName,
                Description = model.Description,
                CreatedBy = model.CreatedBy
            };
            Insert(db);
        }

        public void Update(ServicesViewModel model)
        {
            var db = GetById(model.Id);
            if(db==null)
                throw new ArgumentNullException("model");
            db.Name = model.Name;
            db.SerivceName = model.SerivceName;
            db.Description = model.Description;
            db.DateModify = DateTime.Now;
            db.ModifiedBy = model.ModifiedBy;
            db.IsActive = model.IsActive;
            Commited();
        }

        public IQueryable<ServicesType> GetQuerys(ServicesType mode)
        {
            return GetQuery(x =>
                string.IsNullOrEmpty(mode.SerivceName) || x.SerivceName == mode.SerivceName &&
                string.IsNullOrEmpty(mode.Name) || x.Name == mode.Name
                );
        }

        public ServicesType GetById(int id)
        {
            return Context.ServicesTypes.FirstOrDefault(x => x.Id == id);
        }

        public ServicesViewModel GetModelById(int id)
        {
            var db = GetById(id);
            if (db == null)
                return null;
            var model = ToModels(db);
            return model;
        }

        public int GetId(string serviceName)
        {
            var sv = FindEntity(x => x.SerivceName.Equals(serviceName));
            if (sv!= null)
            {
                return sv.Id;
            }
            return 0;
        }

        public List<ServicesType> ListService()
        {
            string sql = @"select * from ServicesType   
                            where id not in(
                            select distinct ServiceId from Shipment
                            union
                             select distinct ServiceId from Freight)
                            ";
            var listFree = Context.ExecuteQuery<ServicesType>(sql);
            return listFree.ToList();
        }

        public bool CheckServiceFree(int id)
        {
            return ListService().Any(x => x.Id == id); 
        }

        public bool SetActive(int id, bool isActive)
        {
            var db = GetById(id);
            if (db!= null)
            {
                db.IsActive = isActive;
                Commited();
                return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            var sv = GetById(id);
            Delete(sv); 
        }
        private ServicesViewModel ToModels(ServicesType services)
        {
            if (services == null) return null;
            return Mapper.Map<ServicesViewModel>(services);
        }
    }
}