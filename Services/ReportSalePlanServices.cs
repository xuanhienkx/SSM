using System.Collections.Generic;
using System.Linq;
using SSM.Models;

namespace SSM.Services
{
    public interface IReportSalePlanServices
    {
        IList<PlanModelMonth> GetPlanMonthYear(long id, int year, TypeOfPlan type = TypeOfPlan.User);
        IList<PlanUnitMonthModel> GetPlanUnitYear(long id, int year, TypeOfPlan type = TypeOfPlan.User);
        IList<MonthOfYearReport> GetOrderMountYear(long id, int year, TypeOfPlan type = TypeOfPlan.User);
        IList<ReportDetailYearModel> GetAllDeptOfCompay(long comId, int year);
        IList<ReportDetailYearModel> GetAllUserOfDept(long deptId, int year);
    }
    public class ReportSalePlanServices : IReportSalePlanServices
    {
        private readonly DataClasses1DataContext db;
        public ReportSalePlanServices()
        {
            db = new DataClasses1DataContext();
        }
        public IList<PlanModelMonth> GetPlanMonthYear(long id, int year, TypeOfPlan type = TypeOfPlan.User)
        {
            List<SalePlan> list;
            var result = new List<PlanModelMonth>();
            for (int i = 1; i <= 12; i++)
            {
                result.Add(new PlanModelMonth(i, 0));
            }
            switch (type)
            {
                case TypeOfPlan.User:
                    list = db.SalePlans.Where(x => x.PlanMonth.Value.Year == year && x.UserId == id).ToList();
                    break;
                case TypeOfPlan.Department:
                    list = db.SalePlans.Where(x => x.PlanMonth.Value.Year == year && x.User.DeptId == id).ToList();
                    break;
                case TypeOfPlan.Company:
                default:
                    list = db.SalePlans.Where(x => x.PlanMonth.Value.Year == year && x.User.ComId == id).ToList();
                    break;
            }
            if (list.Any())
            {
                foreach (var item in list)
                {
                    if (item?.PlanValue != null && item.PlanValue.Value > 0)
                        result[item.PlanMonth.Value.Month - 1].PValue = item.PlanValue.Value;
                }
            }
            return result.OrderBy(x => x.Month).ToList();
        }
        public IList<PlanUnitMonthModel> GetPlanUnitYear(long id, int year, TypeOfPlan type = TypeOfPlan.User)
        {
            IList<PlanUnitMonthModel> list;

            switch (type)
            {
                case TypeOfPlan.User:
                    var qry = db.Users.GroupJoin(
                            db.SalePlans,
                            u => u.Id,
                            p => p.UserId,
                            (x, y) => new { User = x, Plan = y })
                            .SelectMany(
                            x => x.Plan.DefaultIfEmpty(),
                            (x, y) => new { User = x.User, Plan = y });
                    list = qry.Where(x => x.Plan.PlanMonth.Value.Year == year && x.Plan.UserId == id)
                        .Select(r => new PlanUnitMonthModel(r.User.Id, r.User.FullName, r.Plan.PlanValue ?? 0)
                      ).ToList();
                    break;
                case TypeOfPlan.Department:
                    var qry2 = db.Departments.GroupJoin(
                            db.SalePlans,
                            u => u.Id,
                            p => p.User.DeptId,
                            (x, y) => new { User = x, Plan = y })
                        .SelectMany(
                            x => x.Plan.DefaultIfEmpty(),
                            (x, y) => new { User = x.User, Plan = y });
                    list = qry2.Where(x => x.Plan.PlanMonth.Value.Year == year && x.Plan.UserId == id)
                        .Select(r => new PlanUnitMonthModel(r.User.Id, r.User.DeptName, r.Plan.PlanValue ?? 0)
                        ).ToList();
                    break;
                case TypeOfPlan.Company:
                default:
                    var qry3 = db.Companies.GroupJoin(
                            db.SalePlans,
                            u => u.Id,
                            p => p.User.ComId,
                            (x, y) => new { User = x, Plan = y })
                        .SelectMany(
                            x => x.Plan.DefaultIfEmpty(),
                            (x, y) => new { User = x.User, Plan = y });
                    list = qry3.Where(x => x.Plan.PlanMonth.Value.Year == year && x.Plan.UserId == id)
                        .Select(r => new PlanUnitMonthModel(r.User.Id, r.User.CompanyName, r.Plan.PlanValue ?? 0)
                        ).ToList();
                    break;
            }

            return list?.OrderBy(x => x.Name).ToList();
        }
        public IList<MonthOfYearReport> GetOrderMountYear(long id, int year, TypeOfPlan type = TypeOfPlan.User)
        {
            var query = db.Revenues.Where(x => x.Shipment.DateShp.Value.Year == year);
            switch (type)
            {
                case TypeOfPlan.User:
                    query = query.Where(x => x.Shipment.SaleId == id);
                    break;
                case TypeOfPlan.Department:
                    query = query.Where(x => x.Shipment.User.DeptId == id);
                    break;
                case TypeOfPlan.Company:
                default:
                    query = query.Where(x => x.Shipment.User.ComId == id);
                    break;
            }
            var planOfYear = GetPlanMonthYear(id, year, type);
            var orders = query.GroupBy(g => new { g.Shipment.DateShp.Value.Month, g.SaleType })
                .Select(r => new MonthOfYearReport
                {
                    Month = r.Key.Month,
                    SaleType = r.Key.SaleType,
                    Profit = r.Sum(x => x.Earning??0),
                    Bonus = r.Where(a => a.Shipment.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString()))
                            .Sum(x => x.Earning ?? 0)

                }).ToList();
            var saleTypes = db.SaleTypes.Where(x => x.Active.Value).ToList();
            var saleTyleDetailMonth=new List<MonthOfYearReport>();
            foreach (var saleType in saleTypes)
            {
                for (var i = 1; i < 12; i++)
                {
                    var item = new MonthOfYearReport
                    {
                        Month = i,
                        SaleType = saleType.Name
                    };
                    saleTyleDetailMonth.Add(item);
                }
            }
            if (orders.Any())
            {
                
            }


            var result = (from pl in planOfYear
                          join at in orders on pl.Month equals at.Month into temp
                          from at in temp.DefaultIfEmpty()
                          select new MonthOfYearReport()
                          {
                              Month = pl.Month,
                              PlanValue = pl.PValue,
                              SaleType = at?.SaleType,
                              Profit = at?.Profit ?? 0,
                              Perform = (pl.PValue == 0) ? 0 : at?.Profit ?? 0 / pl.PValue,
                              Bonus = at.Bonus
                          });
            return result.OrderBy(x => x.SaleType).ThenBy(x => x.Month).ToList();
        }
        public IList<ReportDetailYearModel> GetAllDeptOfCompay(long comId, int year)
        {
            var plans = GetPlanUnitYear(comId, year, TypeOfPlan.Department);
            var orders = db.Revenues.Join(db.Departments, r => r.Shipment.User.DeptId, d => d.Id,
                    (r, d) => new { R = r, D = d })
                .Where(x => x.R.Shipment.DateShp.Value.Year == year && x.D.ComId == comId)
                .GroupBy(g => new { g.D.Id, g.R.Shipment.DateShp.Value.Month })
                .Select(v => new MonthOfYearReport
                {
                    SaleType = v.Key.Id.ToString(),
                    Month = v.Key.Month,
                    Profit = v.Sum(x => x.R.Earning ?? 0),
                    Bonus = v.Where(a =>
                                    a.R.Shipment.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString()))
                            .Sum(x => x.R.Earning ?? 0)
                }).ToList();

            var totalPlanYear = plans.Sum(x => x.PValue);
            var result = plans.GroupJoin(
                orders,
                p => p.Id.ToString(),
                o => o.SaleType,
                    (p, o) => new { Plan = p, Orders = o })
                .SelectMany(
                    x => x.Orders.DefaultIfEmpty(),
                    (x, y) => new { User = x.Plan, Order = y })
                    .Select(r => new ReportDetailYearModel
                    {
                        Month = r.Order.Month,
                        Plan = r.User.PValue,
                        Name = r.User.Name,
                        Profit = r.Order.Profit,
                        Bonus = r.Order.Bonus,
                        Perform = r.User.PValue == 0 ? 0 : r.Order.Profit * 100 / r.User.PValue,
                        PlanPerMonth = totalPlanYear / 12
                    });

            return result.OrderBy(x => x.Name).ThenBy(m => m.Month).ToList();
        }
        public IList<ReportDetailYearModel> GetAllUserOfDept(long deptId, int year)
        {
            var plans = GetPlanUnitYear(deptId, year);
            var orders = db.Revenues
                .Join(db.Users,
                    r => r.Shipment.SaleId,
                    d => d.Id,
                    (r, d) => new { R = r, D = d })
                .Where(x => x.R.Shipment.DateShp.Value.Year == year && x.D.DeptId == deptId)
                .GroupBy(g => new { g.D.Id, g.R.Shipment.DateShp.Value.Month })
                .Select(v => new MonthOfYearReport
                {
                    SaleType = v.Key.Id.ToString(),
                    Month = v.Key.Month,
                    Profit = v.Sum(x => x.R.Earning ?? 0),
                    Bonus = v.Where(a =>
                            a.R.Shipment.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString()))
                        .Sum(x => x.R.Earning ?? 0)
                }).ToList();

            var totalPlanYear = plans.Sum(x => x.PValue);
            var result = plans.GroupJoin(
                    orders,
                    p => p.Id.ToString(),
                    o => o.SaleType,
                    (p, o) => new { Plan = p, Orders = o })
                .SelectMany(
                    x => x.Orders.DefaultIfEmpty(),
                    (x, y) => new { User = x.Plan, Order = y })
                .Select(r => new ReportDetailYearModel
                {
                    Month = r.Order.Month,
                    Plan = r.User.PValue,
                    Name = r.User.Name,
                    Profit = r.Order.Profit,
                    Bonus = r.Order.Bonus,
                    Perform = r.User.PValue == 0 ? 0 : r.Order.Profit * 100 / r.User.PValue,
                    PlanPerMonth = totalPlanYear / 12
                });

            return result.OrderBy(x => x.Name).ThenBy(m => m.Month).ToList();
        }
    }
}