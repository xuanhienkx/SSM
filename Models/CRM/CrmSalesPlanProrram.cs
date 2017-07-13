using System;
using System.Collections.Generic;

namespace SSM.Models.CRM
{
    public class CRMPlanMonthModel
    {
        public long Id { get; set; }
        public int PlanValue { get; set; }
        public int PlanMonth { get; set; }
        public int PlanYear { get; set; }
        public long ProgramMonthId { get; set; }
        public CRMPlanProgMonthModel PlanProgMonth { get; set; }
    }

    public class CRMPlanProgMonthModel
    {
        public long Id { get; set; }

        public int PlanYear { get; set; }

        public int ProgramId { get; set; }
        public long PlanSalesId { get; set; }
        public CRMPlanProgramModel CRMPlanProgramModel { get; set; }
        public CRMPLanSaleModel CRMPLanSaleModel { get; set; }
        public List<CRMPlanMonthModel> CRMPlanMonthModels { get; set; }
        public int TotalPlan { get; set; }
    }

    public class PlanFilter
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public long Id { get; set; }
        public int NextMonth { get; set; }
        public int NextYear { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate  { get; set; }  

        public ReportType ReportType { get; set; }
        public SummaryByType SummaryByType { get; set; }
    }

    public class CRMFilterProfit
    {
        public long UserId { get; set; }
        public long DeptId { get; set; }
        public long OfficeId { get; set; }
        public long CurrentUserId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Top { get; set; }
    }
    public class CRMFilterFollowProfit : CRMFilterProfit
    {
        public long DepartureId { get; set; }
        public long DestinationId { get; set; }
        public long AgentId { get; set; }
        public CRMStatusCode Status { get; set; }
        public long proId { get; set; }
        public string ProCode { get; set; }
        public string ProName { get; set; }
        public bool IsProfit { get; set; }
        public bool IsLost { get; set; }
        public int DaysOfLost { get; set; }

    }

    public class CRMFilterProfitResult
    {
        public CRMCustomer Customer { get; set; }
        public int TotalShipment { get; set; }
        public int TotalShipmentSuccess { get; set; }
        public bool AnyShipmentOld { get; set; }
        public decimal TotalProfit { get; set; }
        public int TotalQuotation { get; set; }
        public int TotalVisit { get; set; }
        public int TotalEvent { get; set; }
        public List<CRMFollowCusUser> CRMFollowCusUsers { get; set; }
        public CRMStatus Status { get; set; }
        public CRMStatusCode Code { get; set; }
        public User CreaterdBy { get; set; }
        public string Source { get; set; }
        public string SaleType { get; set; }
        public string FollowName { get; set; }
        public bool IsLost { get; set; }
    }

    public enum ReportType
    {
        Dayly,
        Monthly,
        Year,
    }

    public enum SummaryByType
    {
        ByUser,
        ByDeparment,
        ByOffice
    }

    public class CRMPlanProgramModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
    }

    public class ReportCustomerViewModel
    {
        public DateTime DateTime { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
    }
    public class CRMPLanSaleModel
    {
        public long Id { get; set; }
        public long SalesId { get; set; }
        public User Sales { get; set; }
        public int PlanYear { get; set; }
        public IList<CRMPlanProgMonthModel> CRMPlanProgMountModels { get; set; }
        public bool IsApproval { get; set; }
        public bool IsSubmited { get; set; }
        public User ApprovalBy { get; set; }
        public User SubmitedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? SubmitedDate { get; set; }
    }

    public class CRMSalesPlanOffice
    {
        public Department Department { get; set; }
        public IEnumerable<CRMPlanProgMonthModel> PlanProgMonths { get; set; }
    }

    public class CrmFilterTopProfitByStore
    {
        public long Id { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FullName { get; set; }
        public int CountSendMail { get; set; }
        public int Visted { get; set; }
        public int TotalShipment { get; set; }
        public decimal Profit { get; set; }
        public DateTime? LastTripDate { get; set; }
        public bool IsLost { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsClient { get; set; }
    }
}