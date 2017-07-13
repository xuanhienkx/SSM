using System;
using System.Linq;
using SSM.Models.CRM;
using SSM.Services.CRM;
using SSM.ViewModels;

namespace SSM.Common
{
    public class ScheduleControlCommon
    {
        private static ICRMScheduleServiec scheduleServiec;
        private ICRMEventService eventService;

        public ScheduleControlCommon()
        {
            scheduleServiec = new CRMScheduleServiec();
            eventService = new CRMEventService();
        }

        public void Start()
        {
            

        }
    }
}