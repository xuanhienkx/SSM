using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMPlanProgMonthServics : IServices<CRMPlanProgMonth>
    {
        CRMPlanProgMonthModel InsertModel(CRMPlanProgMonthModel model);
        CRMPlanProgMonthModel GetModelById(long id);
        void UpdateModel(CRMPlanProgMonthModel model);
        void DeleteModel(long id);
        CRMPlanProgMonthModel ToModel(CRMPlanProgMonth db);

        List<CRMSalesPlanOffice> GetPlanOffice(int year, long deptId);

    }
    public class CRMPlanProgMonthServics : Services<CRMPlanProgMonth>, ICRMPlanProgMonthServics
    {
        public CRMPlanProgMonthModel InsertModel(CRMPlanProgMonthModel model)
        {
            var db = new CRMPlanProgMonth
            {
                PlanYear = model.PlanYear,
                PlanSalesId = model.PlanSalesId,
                ProgramId = model.ProgramId,
            };
            var newDb = (CRMPlanProgMonth)Insert(db);
            return ToModel(newDb);
        }

        public CRMPlanProgMonthModel GetModelById(long id)
        {
            var db = FindEntity(x => x.Id == id);
            return ToModel(db);
        }

        public void UpdateModel(CRMPlanProgMonthModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteModel(long id)
        {
            var db = FindEntity(x => x.Id == id);
            if (db == null)
                throw new ArgumentNullException("id");
            Delete(db);
        }

        public CRMPlanProgMonthModel ToModel(CRMPlanProgMonth db)
        {
            CRMPlanProgMonthModel model = Mapper.Map<CRMPlanProgMonthModel>(db);
            if (db.CRMPLanSale != null)
                model.CRMPLanSaleModel = Mapper.Map<CRMPLanSaleModel>(db.CRMPLanSale);
            if (db.CRMPlanProgram != null)
                model.CRMPlanProgramModel = Mapper.Map<CRMPlanProgramModel>(db.CRMPlanProgram);
            if (db.CRMPlanMonths.Any())
            {
                model.CRMPlanMonthModels = db.CRMPlanMonths.Select(x => Mapper.Map<CRMPlanMonthModel>(x)).ToList();
                model.TotalPlan = model.CRMPlanMonthModels.Sum(x => x.PlanValue);
            }

            return model;
        }

        public List<CRMSalesPlanOffice> GetPlanOffice(int year, long deptId)
        {
            var list = GetAll(x => x.PlanYear == year && (deptId == 0 || x.CRMPLanSale.Sales.DeptId == deptId)).Select(x => ToModel(x));
            var qr = list.GroupBy(x => new { Sales = x.CRMPLanSaleModel, Program = x.CRMPlanProgramModel })
                    .Select(m => new CRMPlanProgMonthModel
                    {
                        CRMPLanSaleModel = m.Key.Sales,
                        CRMPlanProgramModel = m.Key.Program,
                        CRMPlanMonthModels = m.SelectMany(g => g.CRMPlanMonthModels.GroupBy(xp => xp.PlanMonth).Select(it => new CRMPlanMonthModel
                        {
                            PlanMonth = it.Key,
                            PlanValue = it.Sum(v=>v.PlanValue),
                            PlanYear = year
                        })).ToList()
                    });
            var listDep = qr.GroupBy(x => x.CRMPLanSaleModel.Sales.Department).Select(g => new CRMSalesPlanOffice()
            {
                Department = g.Key,
                PlanProgMonths = g
            });
            return listDep.ToList();
        }
    }
}