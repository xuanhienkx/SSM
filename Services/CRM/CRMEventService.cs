using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;
using SSM.ViewModels.Shared;

namespace SSM.Services.CRM
{
    public interface ICRMEventService : IServices<CRMVisit>
    {
        CRMEventModel GetById(long id);
        CRMVisit GetDbById(long id);
        IEnumerable<CRMEventModel> GetByCusIds(long cusId);
        CRMVisit InsertToDb(CRMEventModel model);
        void UpdateToDb(CRMEventModel model);
        void DeleteToDb(long id);
        IEnumerable<CRMEventModel> GetAll(SortField sortField, out int totalRows, Pager pager, long cusId, bool isEventAction, User currenUser);
        CRMEventModel ToModel(CRMVisit visit);
    }
    public class CRMEventService : Services<CRMVisit>, ICRMEventService
    {
        public CRMEventModel GetById(long id)
        {
            var db = GetDbById(id);
            return ToModel(db);
        }

        public CRMVisit GetDbById(long id)
        {
            return FindEntity(x => x.Id == id);
        }

        public IEnumerable<CRMEventModel> GetByCusIds(long cusId)
        {
            var list = GetQuery(x => x.CrmCusId == cusId).OrderBy(x => x.Subject).AsEnumerable();
            return list.Select(x=>ToModel(x));
        }

        public CRMVisit InsertToDb(CRMEventModel model)
        {
            var db = ToDb(model);
            return (CRMVisit)Insert(db);
        }

        public void UpdateToDb(CRMEventModel model)
        {
            var db = GetDbById(model.Id);
            ConvertToDb(model, db);
            Commited();
        }

        public void DeleteToDb(long id)
        {
            var db = GetDbById(id);
            Delete(db);
        }

        public IEnumerable<CRMEventModel> GetAll(SortField sortField, out int totalRows, Pager pager, long cusId, bool isEventAction, User currenUser)
        {
            var qr = GetQuery(x => (cusId == 0 || x.CrmCusId == cusId) && x.IsEventAction == isEventAction);
            if ((!currenUser.IsAdmin() || !currenUser.IsAccountant()))
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
            qr = qr.OrderBy(sortField);
            totalRows = qr.Count();
            var list = GetListPager(qr, pager.CurrentPage, pager.PageSize);
            return list.Select(ToModel);
        }

        public CRMEventModel ToModel(CRMVisit visit)
        {
            if (visit == null) return null;
            var model = Mapper.Map<CRMEventModel>(visit);
            model.CusName = visit.CRMCustomer.CompanyShortName;
            if (visit.CRMEventType != null)
            {
                model.CRMEventType = visit.CRMEventType;
            }
            else
            {
                model.CRMEventType = new CRMEventType();
            }
            if (visit.CRMFollowEventUsers != null)
            {
                model.UsersFollow = Context.CRMFollowEventUsers.Where(x=>x.VisitId==visit.Id).ToList();
                model.UserFollowNames = visit.CRMFollowEventUsers.Aggregate("",
                    (current, u) => current + (u.User.FullName + ";"));
            }
            else
            {
                model.UsersFollow = new List<CRMFollowEventUser>();
            }
            return model;
        }

        private CRMVisit ToDb(CRMEventModel model)
        {
            var db = new CRMVisit()
            {
                AllowAdd = model.AllowAdd,
                AllowEdit = model.AllowEdit,
                AllowViewList = model.AllowViewList,
                CrmCusId = model.CrmCusId, 
                IsEventAction = model.IsEventAction,
                IsSchedule = model.IsSchedule,
                CreatedDate = DateTime.Now,
                DateEvent = model.DateEvent,
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                Description = model.Description,
                EventTypeId = model.EventTypeId,
                Status = (byte)model.Status,
                CreatedById = model.CreatedBy.Id,
                TimeOfRemider = model.TimeOfRemider,
                DayWeekOfRemider = model.DayWeekOfRemider,
                Subject = model.Subject
            };
            return db;
        }

        private void ConvertToDb(CRMEventModel model, CRMVisit db)
        {
            db.AllowAdd = model.AllowAdd;
            db.AllowEdit = model.AllowEdit;
            db.AllowViewList = model.AllowViewList; 
            db.IsSchedule = model.IsSchedule;
            db.DateEvent = model.DateEvent;
            db.DateBegin = model.DateBegin;
            db.DateEnd = model.DateEnd;
            db.Subject = model.Subject;
            db.Description = model.Description;
            db.EventTypeId = model.EventTypeId;
            db.Status = (byte)model.Status; 
            db.ModifiedById = model.ModifiedBy.Id;
            db.ModifiedDate = DateTime.Now; 
            db.DayWeekOfRemider = model.DayWeekOfRemider;
            db.TimeOfRemider = model.TimeOfRemider;
        }
    }
}