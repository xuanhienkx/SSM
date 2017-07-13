using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMJobCategoryService : IServices<CRMJobCategory>
    {
        CRMBaseModel InsertCRMGroup(CRMBaseModel model);
        void UpdateCRMGroup(CRMBaseModel model);
        void DeleteCRMGroup(CRMBaseModel model);
        IEnumerable<CRMBaseModel> GetList(SortField sort,string name);
    }

    public class CRMJobCategoryService : Services<CRMJobCategory>, ICRMJobCategoryService
    {
        public CRMBaseModel InsertCRMGroup(CRMBaseModel model)
        {
            var db = new CRMJobCategory { Name = model.Name, Description = model.Description };
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
            Commited();
        }

        public void DeleteCRMGroup(CRMBaseModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            Delete(db); 
        }

        public IEnumerable<CRMBaseModel> GetList(SortField sort,string name)
        {
            var list = GetQuery(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).OrderBy(sort).ToList();
            return list.Select(x=>Mapper.Map<CRMBaseModel>(x));
        }
    }
}