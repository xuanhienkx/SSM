using System;
using SSM.Common;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMService : IServices<CRMCustomer>
    {
        long InsertToDb(CRMCustomerModel model);
        bool UpdateToDb(CRMCustomerModel model);
        bool DeleteToDb(CRMCustomerModel model);
        void SetMoveCustomerToData(long id, bool isSet, long ssmId);
        void SetStatus(long id, int statusId, CRMStatusCode code, bool isCancel = false);
    }
    public class CRMService : Services<CRMCustomer>, ICRMService
    {
        private CRMCustomer GetById(long id)
        {
            return FindEntity(x => x.Id == id);
        }
        public long InsertToDb(CRMCustomerModel model)
        {
            try
            {
                var db = ToDbModel(model);
                Insert(db);
                var id = db.Id;
                model.Id = id;
                return id;
            }
            catch (NullReferenceException ex)
            {
                Logger.LogError(ex.ToString());
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public bool UpdateToDb(CRMCustomerModel model)
        {
            try
            {
                var db = GetById(model.Id);
                if (db == null)
                    throw new ArgumentNullException("model");
                ConvertModel(model, db);
                Commited();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                throw ex;
            }
        }

        public bool DeleteToDb(CRMCustomerModel model)
        {
            throw new System.NotImplementedException();
        }
        private CRMCustomer ToDbModel(CRMCustomerModel model)
        {
            var db = new CRMCustomer
            {
                CompanyName = model.CompanyName,
                CompanyShortName = model.CompanyShortName,
                CrmAddress = model.CrmAddress,
                CrmPhone = model.CrmPhone,
                CrmCategoryId = model.CRMJobCategory.Id,
                CrmSourceId = model.CRMSource.Id,
                CrmGroupId = model.CRMGroup.Id,
                CrmProvinceId = model.Province.Id,
                SaleTypeId = model.SaleType.Id,
                CrmStatusId = model.CRMStatus.Id,
                Description = model.Description,
                CreatedById = model.CreatedBy.Id,
                CreatedDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                CrmDataType = (byte)model.DataType,
                SsmCusId = model.SsmCusId,
                CustomerType = (byte)model.CustomerType, 
            };
            return db;
        }
        private void ConvertModel(CRMCustomerModel model, CRMCustomer db)
        {
            db.CompanyName = model.CompanyName;
            db.CompanyShortName = model.CompanyShortName;
            db.CrmAddress = model.CrmAddress;
            db.CrmPhone = model.CrmPhone;
            db.CrmCategoryId = model.CRMJobCategory.Id;
            db.CrmSourceId = model.CRMSource.Id;
            db.CrmGroupId = model.CRMGroup.Id;
            db.CrmProvinceId = model.Province.Id;
            db.SaleTypeId = model.SaleType.Id;
            db.ModifyById = model.ModifyBy.Id;
            db.ModifyDate = model.ModifyDate;
            db.CrmStatusId = model.CRMStatus.Id;
            db.Description = model.Description;
            db.IsUserDelete = model.IsUserDelete;
            if (model.MoveToId.HasValue && model.MoveToId.Value > 0)
                db.CreatedById = model.MoveToId;
        }

        public void SetMoveCustomerToData(long id, bool isSet, long ssmId)
        {
            var db = GetById(id);
            db.SsmCusId = isSet ? ssmId : (long?)null;
            Commited();
        }

        public void SetStatus(long id, int statusId, CRMStatusCode code, bool isCancel = false)
        {
            var cus = GetById(id);
            cus.CrmStatusId = statusId;
            cus.DateCancel = isCancel ? DateTime.Now : cus.CreatedDate;
            Commited();
        }
    }
}