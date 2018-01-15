using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSM.Models
{
    public interface ReportServices
    {
        IEnumerable<ViewPerformance> getViewPerformances(long UserId1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnits(long UserId1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<ViewPerformance> getViewPerformancesSales(long DeptId1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnitsSales(long DeptId1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnitsCom(DateTime SearchDate1, DateTime SearchDateTo, bool isConsole = false);
        IEnumerable<QuantityUnits> getQuantityUnitsDept(long ComId, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<ViewPerformance> getViewPerformancesByDept(long ComId, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<ViewPerformance> getViewPerformancesCom(DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<PerformanceReport> getSaleReport(long UserId, int Year);
        IEnumerable<PerformanceReport> getSaleReportDept(long DeptId, int Year);
        IEnumerable<ViewPerformance> getViewPerformancesByDept(long ComId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<ViewPerformance> getViewPerformancesSales(long DeptId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<ViewPerformance> getViewPerformances(long UserId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<ViewPerformance> getViewPerformancesCom(SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnitsCom(SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnitsDept(long ComId, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnitsSales(long DeptId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<QuantityUnits> getQuantityUnits(long UserId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo);
        IEnumerable<Shipment> getAllShipment(SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo, long ComId);
        IEnumerable<PerformanceReport> getSaleReportOffice(long OfficeId, int Year);
        IList<PlanModelMonth> GetPlanYear(long id, int year, TypeOfPlan type = TypeOfPlan.User);
        IList<MonthOfYearReport> GetOrderMountYear(long id, int year, TypeOfPlan type = TypeOfPlan.User);
    }
    public class ReportServicesImpl : ReportServices
    {
        private DataClasses1DataContext db;
        public ReportServicesImpl()
        {
            db = new DataClasses1DataContext();
        }
        public IEnumerable<ViewPerformance> getViewPerformances(long UserId1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<SalePlan> listSalePlan = from SalePlan1 in db.SalePlans
                                                     where SalePlan1.User.Id == UserId1 && SalePlan1.PlanMonth.Equals(SearchDate1)
                                                     select SalePlan1;

                if (listSalePlan == null || listSalePlan.ToList().Count == 0)
                {
                    IEnumerable<ViewPerformance> LeftJoin1 = from subShipment in db.Shipments
                                                             where subShipment.User.Id == UserId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                             && subShipment.IsMainShipment == false
                                                             group subShipment by new { subShipment.User, subShipment.Revenue.SaleType } into _group
                                                             select new ViewPerformance(
                                                                 _group.Key.User.Id,
                                                                 _group.Key.User.UserName,
                                                                 _group.Key.User.Department.DeptName,
                                                                 _group.Key.SaleType,
                                                                 0,
                                                                _group.Sum(s => s.Revenue.Earning.Value) == null ? 0 : _group.Sum(s => s.Revenue.Earning.Value),
                                                                100,
                                                                _group.Sum(s => s.Revenue.AmountBonus2) == null ? 0 : _group.Sum(s => s.Revenue.AmountBonus2),
                                                                _group.Count() == null ? 0 : _group.Count()
                                                                )
                                                              ;

                    return LeftJoin1;
                }

                IEnumerable<ViewPerformance> LeftJoin = (
                                                            from SalePlan12
                                                                 in
                                                                (from SalePlan1 in db.SalePlans
                                                                 where SalePlan1.User.Id == UserId1 && SalePlan1.PlanMonth.Equals(SearchDate1)
                                                                 select new
                                                                 {
                                                                     UserId = SalePlan1.User.Id,
                                                                     Plan = SalePlan1.PlanValue.Value,
                                                                     UserName = SalePlan1.User.FullName,
                                                                     Dept = SalePlan1.Department.DeptName
                                                                 }
                                                            )
                                                            join
                                                             Shipment1 in
                                                                (from subShipment in db.Shipments
                                                                 where subShipment.User.Id == UserId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                                 group subShipment by new { subShipment.User, subShipment.Revenue.SaleType } into _group
                                                                 select new
                                                                 {
                                                                     UserId = _group.Key.User.Id,
                                                                     Perform = _group.Sum(s => s.Revenue.Earning.Value),
                                                                     Shipments = _group.Count(),
                                                                     Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                                                                     _SaleType = _group.Key.SaleType
                                                                 }
                                                                 )

                                                         on SalePlan12.UserId equals Shipment1.UserId
                                                         into _leftjoins
                                                            from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                            select new ViewPerformance(SalePlan12.UserId,
                                                             SalePlan12.UserName,
                                                             SalePlan12.Dept,
                                                             _leftjoin._SaleType,
                                                             SalePlan12.Plan,
                                                             _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                                                            ((_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                                                            _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                                                            _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<ViewPerformance> getViewPerformancesSales(long DeptId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (
                    from SalePlan12
                         in
                        (from SalePlan1 in db.SalePlans
                         where SalePlan1.User != null && SalePlan1.User.Department.Id == DeptId1 && SalePlan1.PlanMonth.Equals(SearchDate1)
                         group SalePlan1 by SalePlan1.User into _group
                         select new { UserId = _group.Key.Id, Plan = _group.Sum(s => s.PlanValue.Value), UserName = _group.Key.FullName, Dept = _group.Key.Department.DeptName }
                    )

                    join Shipment1 in
                        (from subShipment in db.Shipments
                         where DeptId1.Equals(subShipment.User.Department.Id) && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                         && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                         && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                         && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                         && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                         && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                         && ((SalePerformamceModel1.IsConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null)))
                          || (SalePerformamceModel1.IsConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))))

                         group subShipment by new { subShipment.User, subShipment.Revenue.SaleType } into _group
                         select new
                         {
                             UserId = _group.Key.User.Id,
                             Perform = _group.Sum(s => s.Revenue.Earning.Value),
                             Shipments = _group.Count(),
                             Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                             _SaleType = _group.Key.SaleType
                         }
                         )

                   on SalePlan12.UserId equals Shipment1.UserId into _leftjoins
                    from _leftjoin in _leftjoins.DefaultIfEmpty()
                    select new ViewPerformance(SalePlan12.UserId,
                        SalePlan12.UserName,
                        SalePlan12.Dept,
                        _leftjoin._SaleType,
                        SalePlan12.Plan,
                        _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                       ((_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                        _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                       _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments)
                                    );

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<ViewPerformance> getViewPerformancesSales(long DeptId1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (
                    from SalePlan12
                         in
                        (from SalePlan1 in db.SalePlans
                         where SalePlan1.User != null && SalePlan1.User.Department.Id == DeptId1 && SalePlan1.PlanMonth.Equals(SearchDate1)
                         select new { UserId = SalePlan1.User.Id, Plan = SalePlan1.PlanValue.Value, UserName = SalePlan1.User.FullName, Dept = SalePlan1.User.Department.DeptName }
                    )
                    join Shipment1 in
                        (from subShipment in db.Shipments
                         where subShipment.Department.Id == DeptId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                         && subShipment.IsMainShipment == false
                         group subShipment by new { subShipment.User, subShipment.Revenue.SaleType } into _group
                         select new
                         {
                             UserId = _group.Key.User.Id,
                             Perform = _group.Sum(s => s.Revenue.Earning.Value),
                             Shipments = _group.Count(),
                             Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                             _SaleType = _group.Key.SaleType
                         }
                         )

                    on SalePlan12.UserId equals Shipment1.UserId into _leftjoins
                    from _leftjoin in _leftjoins.DefaultIfEmpty()
                    select new ViewPerformance(SalePlan12.UserId,
                                                             SalePlan12.UserName,
                                                             SalePlan12.Dept,
                                                             _leftjoin._SaleType,
                                                             SalePlan12.Plan,
                                                             _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                                                            ((_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                                                             _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                                                             _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<ViewPerformance> getViewPerformancesByDept(long ComId1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (from
                                                         SalePlan12
                                                              in
                                                             (from SalePlan1 in db.SalePlans
                                                              where SalePlan1.Department != null && SalePlan1.Department.ComId.Value == ComId1 && SalePlan1.PlanMonth.Equals(SearchDate1)
                                                              select new { UserId = SalePlan1.Department.Id, Plan = SalePlan1.PlanValue.Value, UserName = "", Dept = SalePlan1.Department.DeptName }
                                                         )
                                                         join Shipment1 in
                                                             (from subShipment in db.Shipments
                                                              where subShipment.Company.Id == ComId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                              && subShipment.IsMainShipment == false
                                                              group subShipment by new { subShipment.Department, subShipment.Revenue.SaleType } into _group
                                                              select new
                                                              {
                                                                  UserId = _group.Key.Department.Id,
                                                                  Perform = _group.Sum(s => s.Revenue.Earning.Value),
                                                                  Shipments = _group.Count(),
                                                                  Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                                                                  _SaleType = _group.Key.SaleType
                                                              }
                                                              )

                                                          on SalePlan12.UserId equals Shipment1.UserId into _leftjoins
                                                         from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                         select new ViewPerformance(SalePlan12.UserId,
                                                              SalePlan12.UserName,
                                                              SalePlan12.Dept,
                                                              _leftjoin._SaleType,
                                                              SalePlan12.Plan,
                                                               _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                                                             ((_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                                                             _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                                                             _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<ViewPerformance> getViewPerformancesByDept(long ComId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (from
                                                         SalePlan12
                                                              in
                                                             (from SalePlan1 in db.SalePlans
                                                              where SalePlan1.Department != null && SalePlan1.Department.ComId.Value == ComId1 && SalePlan1.PlanMonth.Equals(SearchDate1)
                                                              group SalePlan1 by SalePlan1.Department into _group
                                                              select new { UserId = _group.Key.Id, Plan = _group.Sum(s => s.PlanValue.Value), UserName = "", Dept = _group.Key.DeptName }
                                                         )
                                                         join Shipment1 in
                                                             (from subShipment in db.Shipments
                                                              where subShipment.Company.Id == ComId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                              && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                                                              && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                                                              && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                                                              && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                                                              && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                                                              && subShipment.IsMainShipment == false
                                                              group subShipment by new { subShipment.Department, subShipment.Revenue.SaleType } into _group
                                                              select new
                                                              {
                                                                  UserId = _group.Key.Department.Id,
                                                                  Perform = _group.Sum(s => s.Revenue.Earning.Value),
                                                                  Shipments = _group.Count(),
                                                                  Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                                                                  _SaleType = _group.Key.SaleType
                                                              }
                                                              )

                                                         on SalePlan12.UserId equals Shipment1.UserId into _leftjoins
                                                         from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                         select new ViewPerformance(SalePlan12.UserId,
                                                              SalePlan12.UserName,
                                                              SalePlan12.Dept,
                                                              _leftjoin._SaleType,
                                                              SalePlan12.Plan,
                                                               _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                                                             ((_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                                                             _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                                                             _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }

        public IEnumerable<ViewPerformance> getViewPerformances(long UserId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (from SalePlan12
                                                                 in
                                                             (from SalePlan1 in db.SalePlans
                                                              where UserId1 == SalePlan1.UserId && SalePlan1.PlanMonth.Equals(SearchDate1)
                                                              group SalePlan1 by SalePlan1.User into _group
                                                              select new { UserId = _group.Key.Id, Plan = _group.Sum(s => s.PlanValue.Value), UserName = _group.Key.FullName, Dept = _group.Key.Department.DeptName }
                                                         )
                                                         join Shipment1 in
                                                             (from subShipment in db.Shipments
                                                              where UserId1.Equals(subShipment.SaleId) && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                              && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                                                              && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                                                              && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                                                              && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                                                              && ((SalePerformamceModel1.IsConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null)))
                                                               || (SalePerformamceModel1.IsConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))))

                                                              group subShipment by new { subShipment.User, subShipment.Revenue.SaleType } into _group
                                                              select new
                                                              {
                                                                  UserId = _group.Key.User.Id,
                                                                  Perform = _group.Sum(s => s.Revenue.Earning.Value),
                                                                  Shipments = _group.Count(),
                                                                  Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                                                                  _SaleType = _group.Key.SaleType
                                                              }
                                                              )
                                                      on SalePlan12.UserId equals Shipment1.UserId
                                                      into _leftjoins
                                                         from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                         select new ViewPerformance(SalePlan12.UserId,
                                                          SalePlan12.UserName,
                                                          SalePlan12.Dept,
                                                          _leftjoin._SaleType,
                                                          SalePlan12.Plan,
                                                          _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                                                         ((_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                                                         _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                                                         _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<ViewPerformance> getViewPerformancesCom(SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (
                    from SalePlan12
                         in
                        (
                        from Company1 in db.Companies
                        join subSalePlan in
                            (
                                from SalePlan1 in db.SalePlans
                                where SalePlan1.OfficeId != null && SalePlan1.PlanMonth >= SearchDate1 && SalePlan1.PlanMonth <= SearchDateTo
                                && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId == SalePlan1.UserId)
                                group SalePlan1 by SalePlan1.Company into _group
                                select new { UserId = _group.Key.Id, Plan = _group.Sum(s => s.PlanValue.Value), UserName = _group.Key.CompanyName, Dept = "" }
                                ) on Company1.Id equals subSalePlan.UserId into saleJoins
                        from saleJoin in saleJoins.DefaultIfEmpty()
                        select new
                        {
                            UserId = Company1.Id,
                            Plan = saleJoin.Plan == null ? 0 : saleJoin.Plan,
                            UserName = Company1.CompanyName,
                            Dept = ""
                        }
                        )
                    join Shipment1 in
                        (from subShipment in db.Shipments
                         where subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                         && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                         && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                         && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                         && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                         && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                          && ((SalePerformamceModel1.IsConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null)))
                          || (SalePerformamceModel1.IsConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))))

                         group subShipment by new { subShipment.Company, subShipment.Revenue.SaleType } into _group
                         select new
                         {
                             UserId = _group.Key.Company.Id,
                             Perform = _group.Sum(s => s.Revenue.Earning.Value),
                             Shipments = _group.Count(),
                             Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                             _SaleType = _group.Key.SaleType
                         }
                         )

                on SalePlan12.UserId equals Shipment1.UserId into _leftjoins
                    from _leftjoin in _leftjoins.DefaultIfEmpty()
                    select new ViewPerformance(SalePlan12.UserId,
                         SalePlan12.UserName,
                         SalePlan12.Dept,
                         _leftjoin._SaleType,
                         SalePlan12.Plan,
                          _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                        (SalePlan12.Plan == 0 ? 0 : (_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                        _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                        _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<Shipment> getAllShipment(SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo, long ComId)
        {
            try
            {
                return (from subShipment in db.Shipments
                        where subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                        && subShipment.CompanyId == ComId
                        //&& (UserIds.Count == 0 || UserIds.Contains(subShipment.SaleId.Value))
                        && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                        && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                        && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                        && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                        && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                        && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                        && subShipment.IsMainShipment == false
                        select subShipment
                  ).OrderBy(m => m.IsControl).ThenBy(x => x.Id).ThenBy(x => x.ShipmentRef).ThenBy(x => x.ControlStep); ;

            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<ViewPerformance> getViewPerformancesCom(DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<ViewPerformance> LeftJoin = (
                                                        from SalePlan12
                                                            in
                                                            (from Company1 in db.Companies
                                                             join subSalePlan in
                                                                 (
                                                                     from SalePlan1 in db.SalePlans
                                                                     where SalePlan1.OfficeId != null && SalePlan1.PlanMonth >= SearchDate1 && SalePlan1.PlanMonth <= SearchDateTo
                                                                     group SalePlan1 by SalePlan1.Company into _group
                                                                     select new { UserId = _group.Key.Id, Plan = _group.Sum(s => s.PlanValue.Value), UserName = _group.Key.CompanyName, Dept = "" }
                                                                     ) on Company1.Id equals subSalePlan.UserId into saleJoins
                                                             from saleJoin in saleJoins.DefaultIfEmpty()
                                                             select new
                                                             {
                                                                 UserId = Company1.Id,
                                                                 Plan = saleJoin.Plan == null ? 0 : saleJoin.Plan,
                                                                 UserName = Company1.CompanyName,
                                                                 Dept = ""
                                                             }
                                                            )
                                                        join Shipment1 in
                                                            (from subShipment in db.Shipments
                                                             where subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                             && subShipment.IsMainShipment == false
                                                             group subShipment by new { subShipment.Company, subShipment.Revenue.SaleType } into _group
                                                             select new
                                                             {
                                                                 UserId = _group.Key.Company.Id,
                                                                 Perform = _group.Sum(s => s.Revenue.Earning.Value),
                                                                 Shipments = _group.Count(),
                                                                 Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(s => s.Revenue.AmountBonus2),
                                                                 _SaleType = _group.Key.SaleType
                                                             }
                                                             )
                                                         on SalePlan12.UserId equals Shipment1.UserId into _leftjoins
                                                        from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                        select new ViewPerformance(SalePlan12.UserId,
                                                             SalePlan12.UserName,
                                                             SalePlan12.Dept,
                                                             _leftjoin._SaleType,
                                                             SalePlan12.Plan,
                                                             _leftjoin.Perform == null ? 0 : _leftjoin.Perform,
                                                           (SalePlan12.Plan == 0 ? 100 : (_leftjoin.Perform == null ? 0 : _leftjoin.Perform) * 100 / SalePlan12.Plan),
                                                            _leftjoin.Bonus == null ? 0 : _leftjoin.Bonus,
                                                            _leftjoin.Shipments == null ? 0 : _leftjoin.Shipments));

                return LeftJoin;
            }
            catch (Exception e)
            {
                e.Message.ToString();

            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnits(long UserId1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.User.Id == UserId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                      && subShipment.IsMainShipment == false
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnits(long UserId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.User.Id == UserId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                       && ((SalePerformamceModel1.IsConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null)))
                                                        || (SalePerformamceModel1.IsConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))))
                                                      && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                                                              && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                                                              && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                                                              && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                                                              && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnitsSales(long DeptId1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.Department.Id == DeptId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                      && subShipment.IsMainShipment == false
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnitsSales(long DeptId1, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.Department.Id == DeptId1 && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                       && ((SalePerformamceModel1.IsConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null)))
                                                        || (SalePerformamceModel1.IsConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))))
                                                      && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                                                              && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                                                              && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                                                              && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                                                              && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnitsDept(long ComId, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.Company.Id == ComId && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                      && subShipment.IsMainShipment == false
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnitsDept(long ComId, SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.Company.Id == ComId && subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                      && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                                                              && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                                                              && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                                                              && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                                                              && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                                                              && subShipment.IsMainShipment == false
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnitsCom(DateTime SearchDate1, DateTime SearchDateTo, bool isConsole = false)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                      && ((isConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null))
                                                      || isConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))
                                                     ))
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<QuantityUnits> getQuantityUnitsCom(SalePerformamceModel SalePerformamceModel1, DateTime SearchDate1, DateTime SearchDateTo)
        {
            try
            {
                IEnumerable<QuantityUnits> LeftJoin = from subShipment in db.Shipments
                                                      where subShipment.DateShp >= SearchDate1 && subShipment.DateShp <= SearchDateTo
                                                      && ((SalePerformamceModel1.IsConsole == true && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == true && subShipment.ShipmentRef != null)))
                                                        || (SalePerformamceModel1.IsConsole == false && (subShipment.ShipmentRef == null || (subShipment.IsMainShipment == false && subShipment.ShipmentRef != null))))
                                                      && (SalePerformamceModel1.AgentId == 0 || SalePerformamceModel1.AgentId.Equals(subShipment.AgentId))
                                                              && (SalePerformamceModel1.CneeId == 0 || SalePerformamceModel1.CneeId.Equals(subShipment.CneeId))
                                                              && (SalePerformamceModel1.SaleId == 0 || SalePerformamceModel1.SaleId.Equals(subShipment.SaleId))
                                                              && (SalePerformamceModel1.ShipperId == 0 || SalePerformamceModel1.ShipperId.Equals(subShipment.ShipperId))
                                                              && (0.Equals(SalePerformamceModel1.ServiceId) || SalePerformamceModel1.ServiceId.Equals(subShipment.ServiceId))
                                                              && subShipment.IsMainShipment == false
                                                      group subShipment by subShipment.QtyUnit into _group
                                                      select new QuantityUnits(_group.Key, _group.Sum(m => m.QtyNumber));
                return LeftJoin;

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IEnumerable<PerformanceReport> getSaleReport(long UserId, int Year)
        {
            try
            {
                IEnumerable<PerformanceReport> results = from
                                                             reportPlan in
                                                             (from SalePlan1 in db.SalePlans
                                                              where (SalePlan1.UserId != null && SalePlan1.UserId.Value == UserId) && SalePlan1.PlanMonth.Value.Year == Year
                                                              select new
                                                              {
                                                                  Month = SalePlan1.PlanMonth.Value.Month,
                                                                  UserPlan = SalePlan1.PlanValue
                                                              })
                                                         join
                                                 reportPerform in
                                                             (from Shipment1 in db.Shipments
                                                              where UserId == Shipment1.SaleId.Value && Shipment1.DateShp.Value.Year == Year
                                                              && Shipment1.IsMainShipment == false
                                                              group Shipment1 by new { Shipment1.DateShp.Value.Month, Shipment1.Revenue.SaleType }
                                                                  into _group
                                                              select new
                                                              {
                                                                  Month = _group.Key.Month,
                                                                  SaleType = _group.Key.SaleType,
                                                                  profit = _group.Sum(sh => sh.Revenue != null ? sh.Revenue.Earning : 0),
                                                                  Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(sh => sh.Revenue != null ? sh.Revenue.AmountBonus2 : 0)
                                                              }) on reportPlan.Month equals reportPerform.Month into _leftjoins
                                                         from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                         select new PerformanceReport(reportPlan.Month,
                                                             Convert.ToDouble(reportPlan.UserPlan != null ? reportPlan.UserPlan : 0),
                                                             _leftjoin.SaleType != null ? _leftjoin.SaleType.ToString() : ShipmentModel.SaleTypes.Sales.ToString(),
                                                             Convert.ToDouble(_leftjoin.profit != null ? _leftjoin.profit : 0),
                                                             Convert.ToDouble(_leftjoin.Bonus != null ? _leftjoin.Bonus : 0)


                                                             );
                 
                return results;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        /*
        public IEnumerable<PerformanceReport> getSaleReport(long UserId, int Year) {

            try
            {
                IEnumerable<PerformanceReport> results = from reportPerform in
                                 (from Shipment1 in db.Shipments
                                  where UserId == Shipment1.SaleId.Value && Shipment1.DateShp.Value.Year == Year
                                  group Shipment1 by new { Shipment1.DateShp.Value.Month, Shipment1.SaleType }
                                      into _group
                                      select new
                                      {
                                          Month = _group.Key.Month,
                                          SaleType = _group.Key.SaleType,
                                          profit = _group.Sum(sh => sh.Revenue != null ? sh.Revenue.Earning:0)
                                      })
                             from reportPlan in
                                 (from SalePlan1 in db.SalePlans
                                  where (SalePlan1.UserId != null && SalePlan1.UserId.Value == UserId) && SalePlan1.PlanMonth.Value.Year == Year
                                  select new
                                  {
                                      Month = SalePlan1.PlanMonth.Value.Month,
                                      UserPlan = SalePlan1.PlanValue
                                  })
                             where reportPerform.Month == reportPlan.Month
                             select new PerformanceReport(reportPlan.Month, 
                                 Convert.ToDouble(reportPlan.UserPlan), 
                                 reportPerform.SaleType.ToString(), 
                                 Convert.ToDouble(reportPerform.profit));
                return results;
            }
            catch (Exception e) {
                e.Message.ToString();
            }
            return null;
        }
        */
        /*
       public IEnumerable<PerformanceReport> getSaleReportDept(long DeptId, int Year) {

           try
           {
               IEnumerable<PerformanceReport> results = from reportPerform in
                                                             (from Shipment1 in db.Shipments
                                                              where DeptId == Shipment1.User.Department.Id && Shipment1.DateShp.Value.Year == Year
                                                              group Shipment1 by new { Shipment1.DateShp.Value.Month, Shipment1.SaleType }
                                                                  into _group
                                                                  select new
                                                                  {
                                                                      Month = _group.Key.Month,
                                                                      SaleType = _group.Key.SaleType,
                                                                      profit = _group.Sum(sh => sh.Revenue.Earning)
                                                                  })
                                        from reportPlan in
                                                             (from SalePlan1 in db.SalePlans
                                                              where (SalePlan1.DeptId != null && SalePlan1.Department.Id == DeptId) && SalePlan1.PlanMonth.Value.Year == Year
                                                              select new
                                                              {
                                                                  Month = SalePlan1.PlanMonth.Value.Month,
                                                                  UserPlan = SalePlan1.PlanValue
                                                              })
                            where reportPerform.Month == reportPlan.Month
                                                        select new PerformanceReport(reportPlan.Month,
                                                            Convert.ToDouble(reportPlan.UserPlan != null ? reportPlan.UserPlan : 0),
                                                            reportPerform.SaleType.ToString(),
                                                            reportPerform.profit != null ? Convert.ToDouble(reportPerform.profit) : 0);
               return results;
           }
           catch (Exception e) {
               e.Message.ToString();
           }
           return null;
       }
       */
        public IEnumerable<PerformanceReport> getSaleReportDept(long DeptId, int Year)
        {
            try
            {
                IEnumerable<PerformanceReport> results = from
                                                             reportPlan in
                                                             (from SalePlan1 in db.SalePlans
                                                              where (SalePlan1.DeptId != null && SalePlan1.Department.Id == DeptId) && SalePlan1.PlanMonth.Value.Year == Year
                                                              select new
                                                              {
                                                                  Month = SalePlan1.PlanMonth.Value.Month,
                                                                  UserPlan = SalePlan1.PlanValue
                                                              })


                                                         join
                                                 reportPerform in
                                                             (from Shipment1 in db.Shipments
                                                              where DeptId == Shipment1.User.Department.Id && Shipment1.DateShp.Value.Year == Year
                                                              && Shipment1.IsMainShipment == false
                                                              group Shipment1 by new { Shipment1.DateShp.Value.Month, Shipment1.Revenue.SaleType }
                                                                  into _group
                                                              select new
                                                              {
                                                                  Month = _group.Key.Month,
                                                                  SaleType = _group.Key.SaleType,
                                                                  profit = _group.Sum(sh => sh.Revenue.Earning),
                                                                  bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(sh => sh.Revenue.AmountBonus2)
                                                              }) on reportPlan.Month equals reportPerform.Month into _leftjoins
                                                         from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                         select new PerformanceReport(reportPlan.Month,
                                                             Convert.ToDouble(reportPlan.UserPlan != null ? reportPlan.UserPlan : 0),
                                                             _leftjoin.SaleType != null ? _leftjoin.SaleType.ToString() : ShipmentModel.SaleTypes.Sales.ToString(),
                                                             Convert.ToDouble(_leftjoin.profit != null ? _leftjoin.profit : 0),
                                                             Convert.ToDouble(_leftjoin.bonus != null ? _leftjoin.bonus : 0)
                                                             );
                return results;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }

        //Get sales Report Offices
        public IEnumerable<PerformanceReport> getSaleReportOffice(long OfficeId, int Year)
        {
            try
            {
                IEnumerable<PerformanceReport> results = from
                                                             reportPlan in
                                                             (from SalePlan1 in db.SalePlans
                                                              where (SalePlan1.OfficeId != null && SalePlan1.Company.Id == OfficeId) && SalePlan1.PlanMonth.Value.Year == Year
                                                              select new
                                                              {
                                                                  Month = SalePlan1.PlanMonth.Value.Month,
                                                                  UserPlan = SalePlan1.PlanValue
                                                              })


                                                         join
                                                 reportPerform in
                                                             (from Shipment1 in db.Shipments
                                                              where OfficeId == Shipment1.User.Company.Id && Shipment1.DateShp.Value.Year == Year
                                                              && Shipment1.IsMainShipment == false
                                                              group Shipment1 by new { Shipment1.DateShp.Value.Month, Shipment1.Revenue.SaleType }
                                                                  into _group
                                                              select new
                                                              {
                                                                  Month = _group.Key.Month,
                                                                  SaleType = _group.Key.SaleType,
                                                                  profit = _group.Sum(sh => sh.Revenue.Earning),
                                                                  Bonus = _group.Where(x => x.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved)).Sum(sh => sh.Revenue.AmountBonus2)
                                                              }) on reportPlan.Month equals reportPerform.Month into _leftjoins
                                                         from _leftjoin in _leftjoins.DefaultIfEmpty()
                                                         select new PerformanceReport(reportPlan.Month,
                                                             Convert.ToDouble(reportPlan.UserPlan != null ? reportPlan.UserPlan : 0),
                                                             _leftjoin.SaleType != null ? _leftjoin.SaleType.ToString() : ShipmentModel.SaleTypes.Sales.ToString(),
                                                             Convert.ToDouble(_leftjoin.profit != null ? _leftjoin.profit : 0),
                                                             Convert.ToDouble(_leftjoin.Bonus != null ? _leftjoin.Bonus : 0)
                                                             );
                return results;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public IList<PlanModelMonth> GetPlanYear(long id, int year, TypeOfPlan type = TypeOfPlan.User)
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
            var planOfYear = GetPlanYear(id, year, type);
            var orders = query.GroupBy(g => new { g.Shipment.DateShp.Value.Month, g.SaleType })
                .Select(r => new
                {
                    Month = r.Key.Month,
                    SaleType = r.Key.SaleType,
                    Profit = r.Sum(x => x.Earning),
                    Bonus = r.Where(a => a.Shipment.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString()))
                            .Sum(x => x.Earning)

                }).ToList();


            var result = (from pl in planOfYear
                          join at in orders on pl.Month equals at.Month into temp
                          from at in temp.DefaultIfEmpty()
                          select new MonthOfYearReport()
                          {
                              Month = pl.Month,
                              PlanValue = pl.PValue,
                              SaleType = at.SaleType,
                              Profit = at.Profit ?? 0,
                              Perform = (pl.PValue == 0) ? 0 : at.Profit ?? 0 / pl.PValue,
                              Bonus = at.Bonus ?? 0
                          });
            return result.ToList();
        }

    }
}