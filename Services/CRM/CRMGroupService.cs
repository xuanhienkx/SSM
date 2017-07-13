using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMGroupService : IServices<CRMGroup>
    {
        CRMBaseModel InsertCRMGroup(CRMBaseModel model);
        void UpdateCRMGroup(CRMBaseModel model);
        void DeleteCRMGroup(CRMBaseModel model);
        IEnumerable<CRMBaseModel> GetList(SortField sort, string name);
    }
    public class CRMGroupService : Services<CRMGroup>, ICRMGroupService
    {
        public IEnumerable<CRMBaseModel> GetList(SortField sort, string name)
        {
            var list = GetQuery(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).OrderBy(sort).ToList();
            return list.Select(x => Mapper.Map<CRMBaseModel>(x));
        }
        public CRMBaseModel InsertCRMGroup(CRMBaseModel model)
        {
            var db = new CRMGroup { Name = model.Name, Description = model.Description, ParentId = model.ParentId };
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
            db.ParentId = model.ParentId;
            Commited();
        }

        public void DeleteCRMGroup(CRMBaseModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            if (db.ParentId == null)
            {
                var grChildren = GetAll(x => x.ParentId == model.Id);
                if (grChildren.Any())
                {
                    DeleteAll(grChildren);
                }
            }
            Delete(db);
        }
    }
   
}