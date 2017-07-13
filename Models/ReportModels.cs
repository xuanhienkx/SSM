using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class SalePerformamceModel
    {
        public int Priod { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public long ShipperId { get; set; }
        public long CneeId { get; set; }
        public long AgentId { get; set; }
        public int ServiceId { get; set; }
        public long SaleId { get; set; }
        public String ServiceName { get; set; }
        public String DateFrom { get; set; }
        public String DateTo { get; set; }
        public bool IsConsole { get; set; }
    }

    public class SaleTypePerform
    {
        public double Perform { get; set; }
        public double Bonus { get; set; }
        public String SaleType { get; set; }
        public override bool Equals(Object obj)
        {
            if (this.SaleType.Equals(((SaleTypePerform)obj).SaleType))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.SaleType.GetHashCode();
        }
    }
   
    public class ViewPerformance
    {
        public List<SaleTypePerform> SaleTypePerforms { get; set; }
        public SaleTypePerform SaleTypePerform1 { get; set; }
        public String Name { get; set; }
        public String Dept { get; set; }
        public double Percent { get; set; }
        public double Plan { get; set; }
        public double Perform { get; set; }
        public double Bonus { get; set; }
        public int Shipments { get; set; }
        public long UserId { get; set; }
        public String SaleType { get; set; }
        public override bool Equals(Object obj)
        {
            if (this.UserId.Equals(((ViewPerformance)obj).UserId) &&
                this.Name.Equals(((ViewPerformance)obj).Name))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.UserId.GetHashCode();
        }

        public ViewPerformance()
        {
            
        }
        public ViewPerformance(long UserId, String Name, String Dept, decimal Perform, int Shipment, decimal bonus)
        {
            this.UserId = UserId;
            this.Name = Name;
            this.Dept = Dept;
            this.Perform = Convert.ToDouble(Perform);
            this.Shipments = Shipment;
            this.Bonus = Convert.ToDouble(bonus);
        }
        public ViewPerformance(long UserId, String Name, String Dept, String saletype, decimal Plan, decimal Perform, decimal Percent, decimal bonus, int Shipment)
        {
            this.UserId = UserId;
            this.Name = Name;
            this.Dept = Dept;
            this.SaleType = saletype;
            this.Percent = Convert.ToDouble(Percent);
            this.Shipments = Shipment;
            this.Perform = Convert.ToDouble(Perform);
            this.Plan = Convert.ToDouble(Plan);
            this.Bonus = Convert.ToDouble(bonus);
            this.SaleTypePerform1 = new SaleTypePerform();
            this.SaleTypePerform1.Bonus = this.Bonus;
            this.SaleTypePerform1.SaleType = saletype;
            this.SaleTypePerform1.Perform = this.Perform;
        }
        public ViewPerformance(long UserId, String Name, String Dept, decimal Plan, decimal Perform, decimal Percent, decimal bonus, int Shipment)
        {
            this.UserId = UserId;
            this.Name = Name;
            this.Dept = Dept;
            this.Percent = Convert.ToDouble(Percent);
            this.Shipments = Shipment;
            this.Perform = Convert.ToDouble(Perform);
            this.Plan = Convert.ToDouble(Plan);
            this.Bonus = Convert.ToDouble(bonus);
        }
        public ViewPerformance(long UserId, decimal Plan)
        {
            this.UserId = UserId;
            this.Plan = Convert.ToDouble(Plan);
        }
        public void setPerform(SaleTypePerform SaleTypePerform1)
        {
            if (this.SaleTypePerforms == null)
            {
                this.SaleTypePerforms = new List<SaleTypePerform>();
            }
            if (SaleTypePerform1.SaleType == null || SaleTypePerform1.SaleType.Trim() == "")
            {
                SaleTypePerform1.SaleType = this.SaleTypePerforms[0].SaleType;
            }
            if (!this.SaleTypePerforms.Contains(SaleTypePerform1))
            {
                this.SaleTypePerforms.Add(SaleTypePerform1);
            }
            else
            {
                SaleTypePerform SaleTypePerformRe = this.SaleTypePerforms.Find(
                    delegate(SaleTypePerform SaleTypePerform2)
                    {
                        return SaleTypePerform2.SaleType == SaleTypePerform1.SaleType;
                    });
                SaleTypePerformRe.Bonus += SaleTypePerform1.Bonus;
                SaleTypePerformRe.Perform += SaleTypePerform1.Perform;
            }

        }
    }
    public class QuantityUnits {
        public String UnitName { get; set; }
        public double UnitCount { get; set; }
        public QuantityUnits(String UnitName1, double UnitCount1)
        {
            this.UnitName = UnitName1;
            this.UnitCount = UnitCount1;
        }
    }

    public class PerformModel
    {
        public override bool Equals(Object obj)
        {
            if (this.SaleType.Equals(((PerformModel)obj).SaleType))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.SaleType.GetHashCode();
        }

        public String SaleType { get; set; }
        public double Profit { get; set; }
        public double Perform { get; set; }
        public double Bonus { get; set; }
    }

    public class PerformanceReport
    {
        public List<PerformModel> PerformModels { get; set; }
        public PerformModel PerformModel1 { get; set; }
        public double Plan { get; set; }
        public double ProfitSale { get; set; }
        public double ProfitHandel { get; set; }
        public double PerformPS { get; set; }
        public double PerformPH { get; set; }
        public double SumProfit { get; set; }
        public double PerformSumProfit { get; set; }
        public double SumBonus { get; set; }
        public int Month { get; set; }
        public PerformanceReport(double _plan, double _profitSale, double _profitHandle, double _performPs, double _performPH,
            double _sumProfit, double _performSum)
        {
            this.Plan = _plan;
            this.ProfitSale = _profitSale;
            this.ProfitHandel = _profitHandle;
            this.PerformPS = _performPs;
            this.PerformPH = _performPH;
            this.PerformSumProfit = _performSum;
            this.SumProfit = _sumProfit;
        }
        private double divisible(double Number1, double Number2)
        {
            if (Number2 == null || Number2 == 0)
            {
                return 0;
            }
            return Number1 / Number2;
        }
        public void setPerform(PerformModel PerformModel1)
        {
            if (this.PerformModels == null)
            {
                this.PerformModels = new List<PerformModel>();
            }

            if (!this.PerformModels.Contains(PerformModel1))
            {
                PerformModel1.Perform = divisible(PerformModel1.Profit * 100, this.Plan);
                this.PerformModels.Add(PerformModel1);
            }
            else
            {
                PerformModel PerformModeRe = this.PerformModels.Find(
                    delegate(PerformModel PerformModel2)
                    {
                        return PerformModel2.SaleType == PerformModel1.SaleType;
                    });
                PerformModeRe.Profit += PerformModel1.Profit;
                PerformModeRe.Bonus += PerformModel1.Bonus;
                PerformModeRe.Perform = divisible(PerformModeRe.Profit * 100, this.Plan);
            }

            this.SumProfit += PerformModel1.Profit;
            this.SumBonus += PerformModel1.Bonus;
            this.PerformSumProfit = divisible(this.SumProfit * 100, this.Plan);
        }
        public PerformanceReport(int _month, double _plan, String SaleType, double _profit, double _bonus)
        {
            this.Month = _month;
            this.Plan = _plan;
            if (SaleType.Equals(ShipmentModel.SaleTypes.Sales.ToString()))
            {
                this.ProfitSale = _profit;
            }
            else
            {
                this.ProfitHandel = _profit;
            }
            this.PerformModel1 = new PerformModel();
            this.PerformModel1.SaleType = SaleType;
            this.PerformModel1.Profit = _profit;
            this.PerformModel1.Bonus = _bonus;
        }
        public PerformanceReport()
        {
            this.Plan = 0;
            this.ProfitSale = 0;
            this.ProfitHandel = 0;
            this.PerformPS = 0;
            this.PerformPH = 0;
            this.PerformSumProfit = 0;
            this.SumProfit = 0;
            this.SumBonus = 0;
        }

         
    }
    public class YearPlan {
        public double TotalPlan { get; set; }
        public YearPlan(double YearPlan1) {
            this.TotalPlan = YearPlan1;
        }
    }

    public class PersonReportModel {
        public int Year { get; set; }
        [Required]
        [DisplayName("Sale to report")]
        public int UserId { get; set; }
        public String Action { get; set; }
    }
    public class DepartmentportModel
    {
        public int Year { get; set; }
        [Required]
        [DisplayName("Department to report")]
        public int DeptId { get; set; }
        public String Action { get; set; }
    }
    public class OfficeReportModel
    {
        public int Year { get; set; }
        [Required]
        [DisplayName("Department to report")]
        public int OfficeId { get; set; }
        public String Action { get; set; }
    }
    public class ReportYearModel {
        public String SaleName { get; set; }
        public String Plan { get; set; }
        public String PlanPerMonth { get; set; }
        public String Jan { get; set; }
        public String Feb { get; set; }
        public String Mar { get; set; }
        public String Apr { get; set; }
        public String May { get; set; }
        public String Jun { get; set; }
        public String Jul { get; set; }
        public String Aug { get; set; }
        public String Sep { get; set; }
        public String Oct { get; set; }
        public String Nov { get; set; }
        public String Dec { get; set; }
        public String Total { get; set; }
        public String Remain { get; set; }
    }
}