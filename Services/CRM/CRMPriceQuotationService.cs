using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMPriceQuotationService : IServices<CRMPriceQuotation>
    {
        CRMPriceQuotation GetById(long id);
        CRMPriceQuotationModel GetModel(long id);
        long InsertModel(CRMPriceQuotationModel model);
        void UpdateModel(CRMPriceQuotationModel model);
        void UpdateSendMail(long id);
        void DeletePrice(long id);

        IEnumerable<CRMPriceQuotation> GetAll(PriceSearchModel fModel, SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, User currenUser);
    }
    public class CRMPriceQuotationService : Services<CRMPriceQuotation>, ICRMPriceQuotationService
    {
        public CRMPriceQuotation GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }

        public CRMPriceQuotationModel GetModel(long id)
        {
            var db = GetById(id);
            if (db == null)
                throw new ArgumentNullException("id");
            return ToModels(db);
        }

        public long InsertModel(CRMPriceQuotationModel model)
        {
            var db = ToDbModel(model);
            Insert(db);
            return db.Id;
        }

        public void UpdateModel(CRMPriceQuotationModel model)
        {
            var db = GetById(model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            ConvertModel(model, db);
            Commited();
        }

        public void UpdateSendMail(long id)
        {
            var db = GetById(id);
            if (db == null)
                throw new ArgumentNullException("id");
            var emails = db.CRMEmailHistories.Count;
            db.LastDateSend = DateTime.Now;
            db.CountSendMail = emails;
            Commited();
        }

        public void DeletePrice(long id)
        {
            var db = GetById(id);
            if (db == null)
                throw new ArgumentNullException("id");
            Delete(db);
        }

        public IEnumerable<CRMPriceQuotation> GetAll(PriceSearchModel fModel, SortField sortField, out int totalRows, int page, int pageSize, User currenUser)
        {
            totalRows = 0;
            var qr = GetQuery(p =>
                (fModel.RefId == null || p.Id == fModel.RefId) &&
                (string.IsNullOrEmpty(fModel.Name) || p.Subject.Contains(fModel.Name)) &&
                (fModel.SalesId == 0 ||
                (p.CreatedById == fModel.SalesId ||
                (p.CRMCustomer.CRMFollowCusUsers != null && p.CRMCustomer.CRMFollowCusUsers.Any(x => x.UserId == fModel.SalesId))) ||
                (p.CRMCustomer.CreatedById == fModel.SalesId)) &&
                (fModel.DepId == 0 || (p.User != null && p.User.DeptId == fModel.DepId)) &&
                (string.IsNullOrEmpty(fModel.CustomerName) || (p.CRMCustomer != null && p.CRMCustomer.CompanyShortName.Contains(fModel.CustomerName)))
                );

            if (fModel.StatusId != 0)
            {
                qr = qr.Where(x => x.StatusId == fModel.StatusId);
            }
            if (fModel.CusId > 0)
            {
                qr = qr.Where(x => x.CrmCusId == fModel.CusId);
            }
            if (fModel.DateType == "M")
            {
                if (fModel.FromDate.HasValue)
                    qr = qr.Where(x => x.LastDateSend >= fModel.FromDate);
                if (fModel.ToDate.HasValue)
                    qr = qr.Where(x => x.LastDateSend <= fModel.ToDate);
            }
            else
            {
                if (fModel.FromDate.HasValue)
                    qr = qr.Where(x => x.CreatedDate >= fModel.FromDate);
                if (fModel.ToDate.HasValue)
                    qr = qr.Where(x => x.CreatedDate <= fModel.ToDate);
            }
            if ((!currenUser.IsAdmin() || !currenUser.IsAccountant()) && fModel.SalesId == 0)
            {
                if (currenUser.IsDirecter())
                {
                    var comid = Context.ControlCompanies.Where(x => x.UserId == currenUser.Id).Select(x => x.ComId).ToList();
                    comid.Add(currenUser.Id);
                    qr = qr.Where(x => comid.Contains(x.User.ComId.Value));
                }
                else if (currenUser.IsDepManager())
                {
                    qr = qr.Where(x => x.User.DeptId == currenUser.DeptId || (x.CRMCustomer.CRMFollowCusUsers != null &&
                                        x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == currenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == currenUser.Id));
                }
                else
                {
                    qr = qr.Where(x => x.CreatedById == currenUser.Id ||
                                       (x.CRMCustomer.CRMFollowCusUsers != null &&
                                        x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == currenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == currenUser.Id));
                }
            }
            totalRows = qr.Count();
            qr = qr.OrderBy(sortField);
            var list = GetListPager(qr, page, pageSize);
            return list;
        }

        private CRMPriceQuotation ToDbModel(CRMPriceQuotationModel model)
        {
            var db = new CRMPriceQuotation
            {
                Subject = model.Subject,
                Description = model.Description,
                CreatedById = model.CreatedBy.Id,
                CrmCusId = model.CrmCusId,
                CreatedDate = DateTime.Now,
                StatusId = model.StatusId,
                IsDelete = false
            };
            return db;
        }
        private void ConvertModel(CRMPriceQuotationModel model, CRMPriceQuotation db)
        {
            db.Subject = model.Subject;
            db.Description = model.Description;
            db.ModifiedById = model.ModifiedBy.Id;
            db.ModifiedDate = DateTime.Now;
            db.StatusId = model.StatusId;
            db.IsDelete = model.IsDelete;


        }
        public CRMPriceQuotationModel ToModels(CRMPriceQuotation priceQuotation)
        {
            if (priceQuotation == null) return null;
            var model = Mapper.Map<CRMPriceQuotationModel>(priceQuotation);
            model.CrmCusName = model.CRMCustomer.CompanyShortName;
            model.CRMEmailHistories = priceQuotation.CRMEmailHistories.ToList();
            return model;
        }
    }
}