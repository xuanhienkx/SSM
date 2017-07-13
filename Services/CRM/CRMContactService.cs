using System;
using AutoMapper;
using SSM.Common;

using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMContactService : IServices<CRMContact>
    {
        CRMContactModel GetById(int id);
        CRMContactModel GetById(int id, long cusId);
        void InsertToDb(CRMContactModel model);
        void UpdateToDb(CRMContactModel model);
        void DeleteToDb(CRMContactModel model);
        bool DeleteAllCtOfCus(long cusId);

    }

    public class CRMContactService : Services<CRMContact>, ICRMContactService
    {
        public CRMContactModel GetById(int id)
        {
            var db = FindEntity(x => x.Id == id);
            return ToModels(db);
        }

        public CRMContactModel GetById(int id, long cusId)
        {
            var db = FindEntity(x => x.Id == id && x.CmrCustomerId == cusId);
            return ToModels(db);
        }

        public void InsertToDb(CRMContactModel model)
        {
            var db = ToDbModel(model);
            Insert(db);
            Commited();
        }

        public void UpdateToDb(CRMContactModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            ConvertModel(model, db);
            Commited();
        }

        public void DeleteToDb(CRMContactModel model)
        {
            var db = FindEntity(x => x.Id == model.Id);
            if (db == null)
                throw new ArgumentNullException("model");
            Delete(db);
            Commited();
        }

        public bool DeleteAllCtOfCus(long cusId)
        {
            try
            {
                var dbs = GetAll(x => x.CmrCustomerId == cusId);
                DeleteAll(dbs);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
        }

        public CRMContact ToDbModel(CRMContactModel model)
        {
            var db = new CRMContact
            {
                FullName = model.FullName,
                Phone = model.Phone,
                Phone2 = model.Phone2,
                Email = model.Email,
                Email2 = model.Email2,
                CmrCustomerId = model.CmrCustomerId
            };
            return db;
        }
        private void ConvertModel(CRMContactModel model, CRMContact db)
        {
            db.FullName = model.FullName;
            db.Phone = model.Phone;
            db.Phone2 = model.Phone2;
            db.Email = model.Email;
            db.Email2 = model.Email2;
            db.CmrCustomerId = model.CmrCustomerId;

        }
        public CRMContactModel ToModels(CRMContact contact)
        {
            if (contact == null) return null;
            return Mapper.Map<CRMContactModel>(contact);
        }

    }
}