using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models.CRM
{
    public enum PeriodDate
    {
        CreateDate,
        ModifyDate
    }
    public class CRMCustomerModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Customer Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Abb Name")]
        public string CompanyShortName { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Address")]
        public string CrmAddress { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Ctity/Province")]
        public string StateName { get; set; }
        public long? CrmCountryId { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Country")]
        public string CountryName { get; set; }
        [Display(Name = "Phone")]
        //[Required(ErrorMessage = "{0} không được để trống")]
        public string CrmPhone { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        public CustomerType CustomerType { get; set; }
        public long? SsmCusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DateTime { get; set; }
        public CRMDataType DataType { get; set; }
        public List<CRMContactModel> CRMContacts { get; set; }
        public List<CRMFollowCusUserModel> FollowCusUsers { get; set; }
        public User CreatedBy { get; set; }
        public long? MoveToId { get; set; }
        public User ModifyBy { get; set; }
        public SaleType SaleType { get; set; }
        public CRMGroup CRMGroup { get; set; }
        public CRMJobCategory CRMJobCategory { get; set; }
        public CRMSource CRMSource { get; set; }
        public CRMStatus CRMStatus { get; set; }
        public Province Province { get; set; }
        public string UserTogheTheFollow { get; set; }
        public List<User> UsersFollow { get; set; }
        public CRMStatusCode StatusCode { get; set; }
        public bool IsUserDelete { get; set; }
        public SummaryCustomer Summary { get; set; }

    }

    public class SummaryCustomer
    {
        public long Id { get; set; }
        public int TotalDocument { get; set; }
        public int TotalSendEmail { get; set; }
        public int TotalFirstSendEmail { get; set; }
        public int TotalVisitedSuccess { get; set; }
        public int TotalVisited { get; set; }
        public int TotalEvent { get; set; }
        public int TotalShippments { get; set; }
        public int SuccessFully { get; set; }
        public decimal TotalProfit { get; set; }
        public CRMStatusCode StatusCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }

    public class CRMSearchModel
    {
        public PeriodDate PeriodDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CompanyName { get; set; }
        public long DeptId { get; set; }
        public string UserFullName { get; set; }
        public long SalesId { get; set; }
        public CRMDataType DataType { get; set; }
        public CRMJobCategory JobCategory { get; set; }
        public CRMStatusCode CRMStatus { get; set; }
        public CRMSource CRMSource { get; set; }
        public CRMGroup CrmGroup { get; set; }
        public SaleType SaleType { get; set; }
        public long? Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }   

    }
}