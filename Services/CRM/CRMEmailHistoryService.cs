using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMEmailHistoryService : IServices<CRMEmailHistory>
    {
        CRMEmailHistory GetById(long id);
        CRMEmailHistoryModel GetModel(long id);
        long InsertModel(CRMEmailHistoryModel model);
        void UpdateModel(CRMEmailHistoryModel model);
        void DeleteDelete(long id);
        IEnumerable<CRMEmailHistoryModel> GetAll(SSM.Services.SortField sortField, out int totalRows, int page, int pageSize, long priceId);
    }
    public class CRMEmailHistoryService : Services<CRMEmailHistory>, ICRMEmailHistoryService
    {
        public CRMEmailHistory GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }

        public CRMEmailHistoryModel GetModel(long id)
        {
            return ToModels(GetById(id));
        }

        public long InsertModel(CRMEmailHistoryModel model)
        {

            var db = ToDbModel(model);
            var email = (CRMEmailHistory)Insert(db);
            return email.Id;
        }

        public void UpdateModel(CRMEmailHistoryModel model)
        {
            var db = GetById(model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            ConvertModel(model, db);
            Commited();
        }
        public void DeleteDelete(long id)
        {
            var db = GetById(id);
            if (db == null)
                throw new ArgumentNullException("id");
            Delete(db);
        }

        public IEnumerable<CRMEmailHistoryModel> GetAll(SortField sortField, out int totalRows, int page, int pageSize, long priceId)
        {
            totalRows = 0;
            var qr = GetQuery(x => x.PriceId == priceId);
            totalRows = qr.Count();
            qr = qr.OrderBy(sortField);
            var list = GetListPager(qr, page, pageSize);
            return list.Select(ToModels);
        }
        private CRMEmailHistory ToDbModel(CRMEmailHistoryModel model)
        {
            var db = new CRMEmailHistory
            {
                Subject = model.Subject,
                ToAddress = model.ToAddress,
                CcAddress = model.CcAddress,
                DateSend = DateTime.Now,
                PriceId = model.PriceId
            };
            return db;
        }
        private void ConvertModel(CRMEmailHistoryModel model, CRMEmailHistory db)
        {
            db.Subject = model.Subject;
            db.ToAddress = model.ToAddress;
            db.CcAddress = model.CcAddress;
            db.DateSend = model.DateSend;
            db.PriceId = model.PriceId;
        }
        public CRMEmailHistoryModel ToModels(CRMEmailHistory emailHistory)
        {
            if (emailHistory == null) return null;
            var model = Mapper.Map<CRMEmailHistoryModel>(emailHistory);
            return model;
        }

    }
}