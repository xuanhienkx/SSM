using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMUserFollowCustomerService : IServices<CRMFollowCusUser>
    {
        long InsertToDb(CRMFollowCusUserModel model);
        bool DeleteToDb(long id);
        bool Look(long id, bool isLock, User user);
        List<CRMFollowCusUser> GetListByCus(long cusId);
        List<CRMFollowCusUserModel> GetListModelsByCus(long cusId);
        CRMFollowCusUserModel GetModelById(long id);
    }

    public class CRMUserFollowCustomerService : Services<CRMFollowCusUser>, ICRMUserFollowCustomerService
    {
        public long InsertToDb(CRMFollowCusUserModel model)
        {
            var db = new CRMFollowCusUser()
            {
                CrmId = model.CrmId,
                UserId = model.UserId,
                IsLook = false,
                AddById = model.AddById
            };
            var newDb = (CRMFollowCusUser)Insert(db);
            return newDb.Id;
        }

        public bool DeleteToDb(long id)
        {
            var db = FindEntity(x => x.Id == id);
            Delete(db);
            return true;
        }

        public bool Look(long id, bool isLock, User user)
        {
            var db = FindEntity(x => x.Id == id);
            db.IsLook = isLock;
            db.LockById = user.Id;
            Commited();
            return true;
        }

        public List<CRMFollowCusUser> GetListByCus(long cusId)
        {
            var list = GetQuery(x => x.CrmId == cusId);
            return list.ToList();
        }

        public List<CRMFollowCusUserModel> GetListModelsByCus(long cusId)
        {
            var list = GetListByCus(cusId);
            return list.Select(x=> ToMode(x)).ToList();
            throw new System.NotImplementedException();
        }

        private CRMFollowCusUserModel ToMode(CRMFollowCusUser db)
        {
            var model = Mapper.Map<CRMFollowCusUserModel>(db);
            return model;
        }

        public CRMFollowCusUserModel GetModelById(long id)
        {
            var db = FindEntity(x => x.Id == id);
            return ToMode(db);
        }
    }
    public interface ICRMUserFollowEventService : IServices<CRMFollowEventUser>
    {
        long InsertToDb(CRMFollowEventUserModel model); 
        bool DeleteToDb(long id);
        bool Look(long id, bool isLock, User user);
        List<CRMFollowEventUser> GetListByCus(long cusId);
        List<CRMFollowEventUserModel> GetListModelsByCus(long cusId);
        CRMFollowEventUserModel GetModelById(long id);
    }

    public class CRMUserFollowEventService : Services<CRMFollowEventUser>, ICRMUserFollowEventService
    {
        public long InsertToDb(CRMFollowEventUserModel model)
        {
            var db = new CRMFollowEventUser()
            {
                VisitId = model.VisitId,
                UserId = model.UserId,
                IsLook = false,
                AddById = model.AddById
            };
            var newDb = (CRMFollowEventUser)Insert(db);
            return newDb.Id;
        }

        public bool DeleteToDb(long id)
        {
            var db = FindEntity(x => x.Id == id);
            Delete(db);
            return true;
        }

        public bool Look(long id, bool isLock, User user)
        {
            var db = FindEntity(x => x.Id == id);
            db.IsLook = isLock;
            db.LockById = user.Id;
            Commited();
            return true;
        }

        public List<CRMFollowEventUser> GetListByCus(long cusId)
        {
            var list = GetQuery(x => x.VisitId == cusId);
            return list.ToList();
        }

        public List<CRMFollowEventUserModel> GetListModelsByCus(long cusId)
        {
            var list = GetListByCus(cusId);
            return list.Select(x => ToMode(x)).ToList(); 
        }

        private CRMFollowEventUserModel ToMode(CRMFollowEventUser db)
        {
            var model = Mapper.Map<CRMFollowEventUserModel>(db);
            return model;
        }

        public CRMFollowEventUserModel GetModelById(long id)
        {
            var db = FindEntity(x => x.Id == id);
            return ToMode(db);
        }
    }
}