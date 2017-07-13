using System;
using AutoMapper;
using SSM.Models;
using SSM.Models.CRM;

namespace SSM.Services.CRM
{
    public interface ICRMScheduleServiec : IServices<CRMSchedule>
    {
        CRMScheduleModel GetById(int id);
        CRMSchedule InsertToDb(CRMScheduleModel model);
        void UpdateToDb(CRMScheduleModel model);
        void DeleteToDb(int id);
    }
    public class CRMScheduleServiec : Services<CRMSchedule>, ICRMScheduleServiec
    {
        public CRMScheduleModel GetById(int id)
        {
            var db = GetDbById(id);
            return ToModels(db);
        }
        public CRMSchedule InsertToDb(CRMScheduleModel model)
        {
            var db = ToDbModel(model);
            return (CRMSchedule)Insert(db);
        }

        public void UpdateToDb(CRMScheduleModel model)
        {
            var db = GetDbById(model.Id);
            ConvertModel(model, db);
            Commited();
        }

        public void DeleteToDb(int id)
        {
            var db = GetDbById(id);
            Delete(db);
        }
        public CRMScheduleModel ToModels(CRMSchedule schedule)
        {
            if (schedule == null) return null;
            return Mapper.Map<CRMScheduleModel>(schedule);
        }
        public CRMSchedule ToDbModel(CRMScheduleModel model)
        {
            var db = new CRMSchedule
            {
                DateBegin = model.DateBegin,
                DateEnd = model.DateEnd,
                DateOfSchedule = model.DateOfSchedule,
                DayBeforeOfDatePlan = model.DayBeforeOfDatePlan,
                DayBeforeOfDateRevise = model.DayBeforeOfDateRevised,
                DayOfWeek = model.DayOfWeek != null ? string.Join(",", model.DayOfWeek) : null,
                TimeOfSchedule = model.TimeOfSchedule,
                DayAlert = model.DayAlert,
                MountAlert = model.MountAlert

            };
            return db;
        }
        private void ConvertModel(CRMScheduleModel model, CRMSchedule db)
        {
            db.DateBegin = model.DateBegin;
            db.DateEnd = model.DateEnd;
            db.DateOfSchedule = model.DateOfSchedule;
            db.DayBeforeOfDatePlan = model.DayBeforeOfDatePlan;
            db.DayBeforeOfDateRevise = model.DayBeforeOfDateRevised;
            db.DayOfWeek = model.DayOfWeek != null ? string.Join(",", model.DayOfWeek) : null;
            db.TimeOfSchedule = model.TimeOfSchedule;
            db.DayAlert = model.DayAlert;
            db.MountAlert = model.MountAlert;
        }

        private CRMSchedule GetDbById(int id)
        {
            return FindEntity(x => x.Id == id);
        }
    }
}