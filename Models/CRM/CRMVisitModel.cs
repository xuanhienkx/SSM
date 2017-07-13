using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using SSM.ViewModels;

namespace SSM.Models.CRM
{
    public enum CRMEventStatus
    {
        All,
        Follow,
        Finished,
    }

    public enum TypeOfEvent
    {
        Visited,
        Events
    }

    public class EventFilter
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public long? Sales { get; set; }
        public string CustomerName { get; set; }
        public TypeOfEvent? OfEvent { get; set; }
        public CRMEventStatus Status { get; set; }
    }

    public class ListEventFilter
    {
        public int? Sales { get; set; }
        public string CustomerName { get; set; }
        public TypeOfEvent? OfEvent { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CRMEventStatus Status { get; set; }
    }
    public class CRMEventModel
    {
        public long Id { get; set; } 
        public string Description { get; set; }
        [Required]
        [Display(Name = @"Begin Date")]
        public DateTime DateBegin { get; set; }
        public string TimeBegin { get; set; }
        [Display(Name = @"End Date")]
        public DateTime DateEnd { get; set; }
        public string TimeEnd { get; set; }
        [Display(Name = @"Status")]
        public CRMEventStatus Status { get; set; }
        public TypeOfEvent TypeOfEvent { get; set; }
        public bool IsSchedule { get; set; }
        public DateTime DateEvent { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowViewList { get; set; } 
        public List<CRMFollowEventUser> UsersFollow { get; set; }
        [Required]
        [Display(Name = @"Title")]
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public CRMCustomer CRMCustomer { get; set; }
        public long CrmCusId { get; set; }
        [Display(Name = @"Customer")]
        [Required]
        public string CusName { get; set; }
        public CRMEventType CRMEventType { get; set; }
        public int? EventTypeId { get; set; }  
        public User CreatedBy { get; set; }
        public User ModifiedBy { get; set; }
        public List<HttpPostedFileBase> Uploads { get; set; }
        public IList<ServerFile> FilesList { get; set; }
        public bool IsEventAction { get; set; }
        public string UserFollowNames { get; set; }
        public DateTime? LastTimeReminder { get; set; }
        [Required]
        [Display(Name = @"Time")]
        public string TimeOfRemider { get; set; }
        public string DayWeekOfRemider { get; set; }
        public int[] DayOfWeek { get; set; }
        public List<CheckModel> CheckModels { get; set; }

    }

    public class CRMScheduleModel
    {
        public int Id { get; set; }

        public int[] DayOfWeek { get; set; } 
        public List<CheckModel> CheckModels { get; set; }

        public int? DayBeforeOfDatePlan { get; set; }

        public int? DayBeforeOfDateRevised { get; set; }

        public DateTime? DateOfSchedule { get; set; }
        [Required]
        public DateTime DateBegin { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public string TimeOfSchedule { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public int? DayAlert { get; set; }
        public int? MountAlert { get; set; }
    }

    public enum ScheduleType
    {
        DayLoop,
        DatePlan,
        DateVisit,
        OnDay,
        ForDays
    }
}