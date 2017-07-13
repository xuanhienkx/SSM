using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMEvetypeService : IServices<CRMEventType>
    {
        CRMBaseModel InsertModel(CRMBaseModel model);
        void UpdateModel(CRMBaseModel model);
        void DeleteModel(CRMBaseModel model);
        IEnumerable<CRMBaseModel> GetList(SortField sort, string name);
    }
    public class CRMEvetypeService : Services<CRMEventType>, ICRMEvetypeService
    {
        public IEnumerable<CRMBaseModel> GetList(SortField sort, string name)
        {
            var list = GetQuery(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).OrderBy(sort).ToList();
            return list.Select(x => Mapper.Map<CRMBaseModel>(x)); ;
        }
        public CRMBaseModel InsertModel(CRMBaseModel model)
        {
            var db = new CRMEventType { Name = model.Name, Description = model.Description };
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