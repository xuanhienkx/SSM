using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMPLanProgramService : IServices<CRMPlanProgram>
    {
        CRMBaseModel InsertModel(CRMBaseModel model);
        void UpdateModel(CRMBaseModel model);
        void DeleteModel(CRMBaseModel model);
        IEnumerable<CRMBaseModel> GetList(SortField sort, string name);
        CRMPlanProgramModel GetModelById(int id);
        List<CRMPlanProgramModel> GetModelAll();
    }
    public class CRMPLanProgramService : Services<CRMPlanProgram>, ICRMPLanProgramService
    {
        public IEnumerable<CRMBaseModel> GetList(SortField sort, string name)
        {
            var list = GetQuery(x => string.IsNullOrEmpty(name) || x.Name.Contains(name)).OrderBy(sort).ToList();
            return list.Select(x => Mapper.Map<CRMBaseModel>(x)); ;
        }

        public CRMPlanProgramModel GetModelById(int id)
        {
            var db = FindEntity(x => x.Id == id);
            return Mapper.Map<CRMPlanProgramModel>(db);
        }

        public List<CRMPlanProgramModel> GetModelAll()
        {
            var qr = GetQuery().OrderBy(x => x.Name).ToList();
            return qr.Select(x => GetModelById(x.Id)).ToList();
        }



        public CRMBaseModel InsertModel(CRMBaseModel model)
        {
            var db = new CRMPlanProgram { Name = model.Name, Description = model.Description };
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