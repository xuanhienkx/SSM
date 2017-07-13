using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Quartz;
using Quartz.Impl;
using SSM.Controllers;
using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels;
using Logging = Common.Logging;
using RequestContext = CrystalDecisions.Shared.RequestContext;

namespace SSM.Common
{

    public class JobScheduler
    {

        public static void Start()
        {
            try
            {
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<EventSchedulJob>()
                    .WithIdentity("eventsScheduleJob", "group1")
                    .Build();

                // Trigger the job to run now, and then every 30 seconds
                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("eventsScheduleTrigger", "group1")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInMinutes(2)
                      .RepeatForever())
                  .Build();

                sched.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Logger.LogError(se);
            }
        }

        #region SetStatus

        public static void CrmSatusSet()
        {
            try
            {
                DateTime beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 00, 0);
                DateTime dateRun = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 15, 0);
                DateTime endDate = beginDate.AddMinutes(30);
                if ((DateTime.Now >= beginDate && DateTime.Now < dateRun) || (DateTime.Now > dateRun.AddMinutes(30) && DateTime.Now < endDate)) return;
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                IScheduler sched = schedFact.GetScheduler();
                sched.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<CRMStatusProccessJob>()
                    .WithIdentity("crmStatusJob", "group2")
                    .Build();

                // Trigger the job to run now, and then every 40 seconds
                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("crmStatusTrigger", "group2")
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInHours(1)
                      .RepeatForever())
                  .Build();

                sched.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Logger.LogError(se);
            }
        }

        #endregion

    }
    public class EventSchedulJob : IJob
    {
        private ICRMEventService eventService;
        private UsersServices usersServices;

        public EventSchedulJob()
        {
            eventService = new CRMEventService();
            usersServices = new UsersServicesImpl();
        }
        public void Execute(IJobExecutionContext context)
        {
             
            var currendate = DateTime.Now.Date;
            var startDate = new DateTime(currendate.Year, currendate.Month, currendate.Day, 6, 0, 0);
            var endDate = new DateTime(currendate.Year, currendate.Month, currendate.Day, 23, 59, 59);
            if (DateTime.Now < startDate && DateTime.Now > endDate) return;
            var list = eventService.GetAll(x => x.Status == (byte)CRMEventStatus.Follow && x.IsSchedule && (x.DateBegin >= currendate
                                            || (x.DateEnd >= currendate && x.DateEnd <= currendate.AddDays(1)))
                                            && (x.LastTimeReminder == null || x.LastTimeReminder.Value.Date != currendate));
                     
            foreach (var schedule in list)
            {
                try
                {
                    
                    var nowtime = DateTime.Now;
                    if (schedule == null) continue;
                    var lastDate = schedule.LastTimeReminder;
                    if (lastDate != null && lastDate.Value.Date == currendate) continue;
                    var jobDate = currendate.ToString("M/d/yyyy") + " " + schedule.TimeOfRemider;
                    DateTime dt;
                    if (!DateTime.TryParse(jobDate, out dt)) continue;
                    bool isSendMail = true;
                    if (currendate <= schedule.DateEnd && !string.IsNullOrEmpty(schedule.DayWeekOfRemider))
                    {
                        var dayOfWs = schedule.DayWeekOfRemider.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x));
                        if (!dayOfWs.Contains((int)nowtime.DayOfWeek)) isSendMail = false;
                        if (schedule.DateBegin == schedule.DateEnd) isSendMail = true;
                    }

                    if (!isSendMail || nowtime < dt.AddMinutes(-1)) continue;
                    Logger.LogDebug("Process send email for Event");
                    var admin = usersServices.FindEntity(x => x.UserName.ToLower() == "admin");
                    var user = !string.IsNullOrEmpty(schedule.User.Email) ? schedule.User : admin;
                    var emailTo = string.IsNullOrEmpty(schedule.User.Email) ? admin.Email : schedule.User.Email;
                    string emailJoinEvent = string.Empty;
                    if (schedule.CRMFollowEventUsers != null && schedule.CRMFollowEventUsers.Any())
                        emailJoinEvent = schedule.CRMFollowEventUsers.Where(x => !string.IsNullOrEmpty(x.User.Email)).Aggregate(emailJoinEvent,
                            (current, u) => current + (u.User.Email + ","));
                    emailTo = emailTo + "," + emailJoinEvent;     
                    string actionUrl = $"{Helpers.Site}/CRMEvent/Detail/{schedule.Id}";      
                    var link = $"Bạn có thể <a href=\"{actionUrl}\" >click vào đây</a> để xem chi tiết";
                    var model = new EmailModel()
                    {
                        EmailTo = emailTo,
                        EmailCc = admin.Email,
                        User = user,
                        Subject = $"[{schedule.User.FullName}], You have 1 {(schedule.IsEventAction ? "event" : "visit")} {schedule.CRMCustomer.CompanyName}",
                        Message =
                            $"Dear [{schedule.User.FullName}] <br/> You have 1 {(schedule.IsEventAction ? "event" : "visit")}  at {jobDate} <br/> {schedule.Description}  .<br/> {link}",
                        IsUserSend = false,
                        IdRef = schedule.Id,
                    };
                    var errorMessages = string.Empty;
                    var email = new EmailCommon { EmailModel = model };
                    if (!email.SendEmail(out errorMessages, true))
                    {
                        Logger.LogError(errorMessages);
                    }
                    else
                    {
                        schedule.LastTimeReminder = DateTime.Now;
                        eventService.Commited();
                        Logger.Log("Send mail schechdule at " + nowtime.ToString("g"));
                    }
                }
                catch (Exception e)
                {
                    Logger.LogError("Loi job event send mail schedule:"); 
                    Logger.LogError(e); 
                }
            }
        }
    }

    #region JobStatus    

    public class CRMStatusProccessJob : IJob
    {
        private ICRMService crmService;
        private ICRMCustomerService crmCustomerService;
        private ICRMStatusService statusService;
        private ShipmentServices shipmentServices;
        private UsersServices usersServices;

        public CRMStatusProccessJob()
        {
            crmService = new CRMService();
            crmCustomerService = new CRMCustomerService();
            statusService = new CRMStatusService();
            shipmentServices = new ShipmentServicesImpl();
            usersServices = new UsersServicesImpl();
        }

        private int days = 365;
        public void Execute(IJobExecutionContext context)
        {
            var crms = crmCustomerService.GetAll(x => x.CRMStatus.Code != (byte)CRMStatusCode.Client && x.DateCancel.Value < DateTime.Now);
            var crmstting = usersServices.CRMDayCanelSettingNumber();
            days = int.Parse(crmstting.DataValue);
            foreach (var crm in crms)
            {
                SetSatusCrm(crm);
            }
        }

        public void SetSatusCrm(CRMCustomer crm)
        {
            bool isCancel = false;
            CRMStatusCode code = CRMStatusCode.Potential;
            DateTime cancelDate = crm.DateCancel ?? crm.CreatedDate.Date;

            DateTime lastCancelDate = cancelDate.AddDays(days);
            if (crm.SsmCusId.HasValue)
            {
                var shipments = shipmentServices.Count(s => s.CneeId.Value == crm.SsmCusId.Value || s.ShipperId.Value == crm.SsmCusId.Value);
                if (shipments == 0)
                    code = CRMStatusCode.Potential;
                if (shipments >= 1)
                    code = CRMStatusCode.Success;

            }
            else
            {
                if (lastCancelDate < DateTime.Today)
                {
                    isCancel = true;
                    code = CRMStatusCode.Client;
                }
            }

            var status = statusService.GetModel(code);
            if (crm.CRMStatus.Code == (byte)code) return;
            crmService.SetStatus(crm.Id, status.Id, code, isCancel);
        }
    }

    #endregion
}