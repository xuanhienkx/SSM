using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
using Microsoft.Ajax.Utilities;
using SSM.Models;

namespace SSM.Services
{
    public interface IPerformanceReportService : IServices<Shipment>
    {
        List<ViewPerformance> GetPerformancesCom(DateTime fromDate, DateTime toDate, long? comId = null);
    }
    public class PerformanceReportService : Services<Shipment>, IPerformanceReportService
    {
        public List<ViewPerformance> GetPerformancesCom(DateTime fromDate, DateTime toDate, long? comId = null)
        {

            var sqlstr = string.Format(@" cp.Id  as UserId,cp.CompanyName as Name ,'' as Dept,sr.SaleType, cp.PValue,  sr.Perform,sr.Bonus, sr.shipments 
                          from(
                         select c.Id, c.CompanyName, x.PValue from Company c left join  (
                        select  p.OfficeId,SUM( p.PlanValue) as PValue from SalePlan p
                        where p.OfficeId is not null and p.PlanMonth between '{0}' and '{1}'
                        group by p.OfficeId) x on c.Id = x.OfficeId
                        ) cp
                        left join (
                        select CompanyId, s.SaleType, COUNT(*) as shipments, sum(r.Earning) as Perform, SUM(r.AmountBonus2) as Bonus
                        from Shipment s left join Revenue r on s.Id= r.Id 
                        where s.DateShp between '{0}' and '{1}'
                        group by CompanyId, s.SaleType 
                        ) sr on cp.Id= sr.CompanyId  ",fromDate.ToString(), toDate.ToString());
           // var plans = Context.ExecuteQuery()
            return new List<ViewPerformance>();
        }
    }
}