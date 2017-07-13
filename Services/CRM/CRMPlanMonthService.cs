using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMPlanMonthService : IServices<CRMPlanMonth>
    {
        CRMPlanMonthModel InsertModel(CRMPlanMonthModel model);
        CRMPlanMonthModel GetModelById(long id);
        void UpdateModel(CRMPlanMonthModel model);
        void DeleteModel(long id);
        List<CRMPlanMonthModel> GetAllByProgram(int programId);
    }
    public class CRMPlanMonthService : Services<CRMPlanMonth>, ICRMPlanMonthService
    {
        public CRMPlanMonthModel InsertModel(CRMPlanMonthModel model)
        {
            var db = new CRMPlanMonth
            {
                PlanMonth = model.PlanMonth,
                PlanValue = model.PlanValue,
                PlanYear = model.PlanYear,
                ProgramMonthId = model.ProgramMonthId
            };
            var newdb = (CRMPlanMonth)Insert(db);
            return ToModel(newdb);
        }

        public CRMPlanMonthModel GetModelById(long id)
        {
            var dbm = FindEntity(x => x.Id == id);
            return ToModel(dbm);
        }

        public void UpdateModel(CRMPlanMonthModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            db.PlanMonth = model.PlanMonth;
            db.PlanValue = model.PlanValue;
            db.PlanYear = model.PlanYear; 
            Commited();
        }

        public void DeleteModel(long id)
        {
            var db = FindEntity(x => x.Id == id);
            if (db == null)
                throw new ArgumentNullException("id");
            Delete(db);
        }

        public List<CRMPlanMonthModel> GetAllByProgram(int programId)
        {
            var qr = GetAll(x => x.ProgramMonthId == programId).OrderBy(x => x.PlanMonth).ThenBy(x => x.PlanYear);
            return qr.Select(x => ToModel(x)).ToList();
        }

        private CRMPlanMonthModel ToModel(CRMPlanMonth db)
        {
            CRMPlanMonthModel model = Mapper.Map<CRMPlanMonthModel>(db);
            if (db.CRMPlanProgMonth != null)
                model.PlanProgMonth = Mapper.Map<CRMPlanProgMonthModel>(db.CRMPlanProgMonth); 
            return model;
        }
    }
}