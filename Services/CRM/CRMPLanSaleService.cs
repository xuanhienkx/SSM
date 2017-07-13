using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{


    public interface ICRMPLanSaleService : IServices<CRMPLanSale>
    {
        CRMPLanSaleModel InsertModel(CRMPLanSaleModel model);
        CRMPLanSaleModel GetByIModel(long id);
        void UpdateModel(CRMPLanSaleModel model);
        void DeleteModel(long id);
        List<CRMPLanSaleModel> GetAllByUser(User user, int year, long deptId = 0);
        List<CRMSalesPlanOffice> GetAllDeptList(int year, long deptId = 0, long comId = 0);
    }
    public class CRMPLanSaleService : Services<CRMPLanSale>, ICRMPLanSaleService
    {
        private ICRMPlanProgMonthServics planProgMonthServics;

        public CRMPLanSaleService()
        {
            planProgMonthServics = new CRMPlanProgMonthServics();
        }
        public CRMPLanSaleModel InsertModel(CRMPLanSaleModel model)
        {
            var db = new CRMPLanSale
            {
                SalesId = model.SalesId,
                PlanYear = model.PlanYear
            };
            var newDb = (CRMPLanSale)Insert(db);
            return ToModel(newDb);
        }

        public CRMPLanSaleModel GetByIModel(long id)
        {
            var splan = FindEntity(x => x.Id == id);
            return ToModel(splan);
        }

        public void UpdateModel(CRMPLanSaleModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            ToDb(model, db);
            Commited();
        }

        public void DeleteModel(long id)
        {
            var db = FindEntity(x => x.Id == id);
            if (db == null)
                throw new ArgumentNullException("id");
            Delete(db);
        }

        public List<CRMPLanSaleModel> GetAllByUser(User user, int year, long deptId = 0)
        {
            List<CRMPLanSale> qr;
            if (user.IsAdminAndAcct())
                qr = GetAll(x => (deptId == 0 || x.Sales.DeptId == deptId) && x.PlanYear == year);
            else if (user.IsDepManager())
                qr = GetAll(x => x.Sales.DeptId == user.DeptId && x.PlanYear == year);
            else
                qr = GetAll(x => x.SalesId == user.Id && x.PlanYear == year);
            return qr.Select(x => ToModel(x)).ToList();
        }

        public List<CRMSalesPlanOffice> GetAllDeptList(int year, long deptId = 0, long comId = 0)
        {
            var planList =
                GetAll(x => x.PlanYear == year && (deptId == 0 || x.Sales.DeptId == deptId) && (comId == null || comId == x.Sales.ComId.Value));
            var qr = planList.GroupBy(x => x.Sales.Department)
                    .Select(x => new CRMSalesPlanOffice
                    {
                        Department = x.Key,
                        PlanProgMonths = x.SelectMany(m => m.CRMPlanProgMonths.Select(xp => planProgMonthServics.ToModel(xp)))
                    }).ToList();
            return qr;
        }

        private CRMPLanSaleModel ToModel(CRMPLanSale db)
        {
            CRMPLanSaleModel model = Mapper.Map<CRMPLanSaleModel>(db);
            if (db.CRMPlanProgMonths.Any())
            {
                model.CRMPlanProgMountModels = db.CRMPlanProgMonths.Select(x => planProgMonthServics.ToModel(x)).ToList();
            }
            model.Sales = db.Sales;
            if (db.ApprovedById != null)
                model.ApprovalBy = Context.Users.FirstOrDefault(x => x.Id == db.ApprovedById);
            if (db.SubmitedById != null)
                model.SubmitedBy = Context.Users.FirstOrDefault(x => x.Id == db.SubmitedById);
            return model;
        }

        private void ToDb(CRMPLanSaleModel model, CRMPLanSale db)
        {
            db.ApprovedById = model.ApprovalBy != null ? model.ApprovalBy.Id : (long?)null;
            db.ApprovedDate = model.ApprovedDate;
            db.SubmitedDate = model.SubmitedDate;
            db.SubmitedById = model.SubmitedBy != null ? model.SubmitedBy.Id : (long?)null;
            db.SalesId = model.SalesId;
        }
    }
}