using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMDocumentService : IServices<CRMCusDocument>
    {
        CRMCusDocument GetById(long id);
        CrmCusDocumentModel GetModel(long id);
        long InsertModel(CrmCusDocumentModel model);
        void UpdateModel(CrmCusDocumentModel model);
        void DeleteModel(long id);
        IEnumerable<CrmCusDocumentModel> GetAll(CRMSearchModel filter, SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, User currenUser);
    }

    public class CRMDocumentService : Services<CRMCusDocument>, ICRMDocumentService
    {
        private IServerFileService fileService;
        public CRMDocumentService()
        {
            this.fileService = new ServerFileService();
        }

        public CRMCusDocument GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }

        public CrmCusDocumentModel GetModel(long id)
        {
            return ToModels(GetById(id));
        }

        public long InsertModel(CrmCusDocumentModel model)
        {
            var db = ToDbModel(model);
            Insert(db);
            return db.Id;
        }

        public void UpdateModel(CrmCusDocumentModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            ConvertModel(model, db);
            Commited();
        }

        public void DeleteModel(long id)
        {
            var db = GetById(id);
            if (db == null)
                throw new ArgumentNullException("id");
            Delete(db);
        }

        public IEnumerable<CrmCusDocumentModel> GetAll(CRMSearchModel filter, SortField sortField, out int totalRows, int page, int pageSize, User currenUser)
        {
            var qr = GetQuery(x => (!filter.Id.HasValue || filter.Id == x.CrmCusId) &&
                                   (string.IsNullOrEmpty(filter.CompanyName) || (x.CRMCustomer != null &&  x.CRMCustomer.CompanyShortName.Contains(filter.CompanyName)))  &&
                                   (filter.SalesId == 0 || 
                                   (x.CreatedById == filter.SalesId ||
                                   (x.CRMCustomer.CRMFollowCusUsers != null && x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == currenUser.Id))) ||
                                    (x.CRMCustomer.CreatedById == currenUser.Id))
                                   && (filter.DeptId == 0 || (x.User != null && x.User.DeptId == filter.DeptId)));
              if (filter.FromDate.HasValue)
                    qr = qr.Where(x => x.ModifiedDate >= filter.FromDate);
                if (filter.ToDate.HasValue)
                    qr = qr.Where(x => x.ModifiedDate <= filter.ToDate);
            
            if ((!currenUser.IsAdmin() || !currenUser.IsAccountant()) && filter.SalesId == 0)
            {
                if (currenUser.IsDirecter())
                {
                    var comid = Context.ControlCompanies.Where(x => x.UserId == currenUser.Id).Select(x => x.ComId).ToList();
                    comid.Add(currenUser.Id);
                    qr = qr.Where(x => comid.Contains(x.User.ComId.Value));
                }
                else if (currenUser.IsDepManager())
                {
                    qr = qr.Where(x => x.User.DeptId == currenUser.DeptId || 
                                       (x.CRMCustomer.CRMFollowCusUsers != null && x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == currenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == currenUser.Id));
                }
                else
                {
                    qr = qr.Where(x => x.CreatedById == currenUser.Id ||
                                       (x.CRMCustomer.CRMFollowCusUsers != null && x.CRMCustomer.CRMFollowCusUsers.Any(f => f.UserId == currenUser.Id)) ||
                                       (x.CRMCustomer.CreatedById == currenUser.Id));
                }
            }
            totalRows = qr.Count();
            qr = qr.OrderBy(sortField);
            var list = GetListPager(qr, page, pageSize);
            return list.Select(ToModels);
        }
        private CRMCusDocument ToDbModel(CrmCusDocumentModel model)
        {
            var db = new CRMCusDocument
            {
                DocName = model.DocName,
                CrmCusId = model.CrmCusId,
                ModifiedDate = DateTime.Now,
                Description = model.Description,
                LinkDoc = model.LinkDoc,
                CreatedById = model.CreatedById
            };
            return db;
        }
        private void ConvertModel(CrmCusDocumentModel model, CRMCusDocument db)
        {
            db.DocName = model.DocName;
            db.CrmCusId = model.CrmCusId;
            db.ModifiedDate = DateTime.Now;
            db.Description = model.Description;
            db.LinkDoc = model.Description;
            db.ModifiedById = model.ModifiedById;
        }

        private CrmCusDocumentModel ToModels(CRMCusDocument document)
        {
            if (document == null) return null;
            var model = Mapper.Map<CrmCusDocumentModel>(document);
            model.FilesList = fileService.GetServerFile(model.Id, model.GetType().ToString());
            model.Sales = Context.Users.FirstOrDefault(x => x.Id == document.CreatedById);
            return model;
        }
    }
}