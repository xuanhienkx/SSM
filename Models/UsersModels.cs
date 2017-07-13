using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SSM.Controllers;
using System.Web.Mvc;
namespace SSM.Models
{

    public class UsersModel
    {
        #region Permission
        public static bool isEditShippment(string shipmentService, User User1)
        {
            if (isDeptManager(User1))
            {
                return false;
            }
            if (isAdminOrDirctor(User1))
            {
                return true;
            }
            if (((User1.AllowTrackAirIn && shipmentService.Equals(ShipmentModel.Services.AirInbound.ToString()))
                                 || (User1.AllowTrackAirOut && shipmentService.Equals(ShipmentModel.Services.AirOutbound.ToString()))
                                 || (User1.AllowTrackSeaIn && shipmentService.Equals(ShipmentModel.Services.SeaInbound.ToString()))
                                 || (User1.AllowTrackSeaOut && shipmentService.Equals(ShipmentModel.Services.SeaOutbound.ToString()))
                                 || (User1.AllowTrackInlandSer && shipmentService.Equals(ShipmentModel.Services.InlandService.ToString()))
                                 || (User1.AllowTrackProjectSer && shipmentService.Equals(ShipmentModel.Services.Other.ToString()))) && isOperation(User1))
            {
                return true;
            }
            return false;
        }

        public static bool IsEditTrading(User user)
        {
            if (isAdminNComDirctor(user) || user.AllowTrading)
            {
                return true;
            }
            return false;
        }

        public static bool IsAllowFuntion(User user)
        {
            if (isAdminOrDirctor(user) || isAccountant(user) || user.AllowTrading || isDeptManager(user))
                return true;
            return false;
        }

        public static bool IsTrading(User user)
        {
            if (isAdminOrDirctor(user) || user.AllowTrading || isAccountant(user) || (isDeptManager(user)))
                return true;
            return false;
        }
        public static bool IsMainDirector(User user)
        {
            if (isAdminOrDirctor(user) && user.Company.CompanyName == "SANCO FREIGHT LTD")
                return true;
            return false;
        }
        public static bool isAdminOrDirctor(User User1)
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Administrator.ToString())
                    || User1.RoleName.Equals(UsersModel.Positions.Director.ToString()))
            {
                return true;
            }
            return false;
        }
        public static bool isComDirctor(User User1)
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Director.ToString())
                    && (User1.DirectorLevel == 1))
            {
                return true;
            }
            return false;
        }
        public static bool isAdminNComDirctor(User User1)
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Administrator.ToString())
                    || (User1.RoleName.Equals(UsersModel.Positions.Director.ToString())
                    && (User1.DirectorLevel == 1)))
            {
                return true;
            }
            return false;
        }
        public static bool isOperation(User User1)
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Operations.ToString()))
            {
                return true;
            }
            return false;
        }
        public static bool isDeptManager(User User1)
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Manager.ToString()))
            {
                return true;
            }
            return false;
        }
        public static bool isSales(User User1)
        {
            if (UsersModel.Functions.Sales.ToString().Equals(User1.Department != null ? User1.Department.DeptFunction : ""))
            {
                return true;
            }
            return false;
        }
        public static bool isAccountant(User User1)
        {
            if (UsersModel.Functions.Accountant.ToString().Equals(User1.Department != null ? User1.Department.DeptFunction : ""))
            {
                return true;
            }
            return false;
        }
        public static bool isSetPassword(User User1)
        {
            if (isAdminOrDirctor(User1))
            {
                return true;
            }
            if (User1.SetPass == true)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Define Enum
        public static string ConvertToRoleName(string Position, string DeptFunction)
        {
            if (DisplayPositions.Director.ToString().Equals(Position))
            {
                return Positions.Director.ToString();
            }
            if (DisplayPositions.Manager.ToString().Equals(Position))
            {
                return Positions.Manager.ToString();
            }
            if (DisplayPositions.Admin.ToString().Equals(Position))
            {
                return Positions.Administrator.ToString();
            }
            if (Functions.Sales.ToString().Equals(Position))
            {
                return Positions.Sales.ToString();
            }
            if (Functions.Operations.ToString().Equals(Position))
            {
                return Positions.Operations.ToString();
            }
            return Positions.Accountant.ToString();
        }

        public static string RevertFromRoleName(string RoleName)
        {
            if (Positions.Director.ToString().Equals(RoleName))
            {
                return DisplayPositions.Director.ToString();
            }
            if (Positions.Manager.ToString().Equals(RoleName))
            {
                return DisplayPositions.Manager.ToString();
            }
            if (Positions.Administrator.ToString().Equals(RoleName))
            {
                return DisplayPositions.Admin.ToString();
            }
            return DisplayPositions.Staff.ToString();

        }
        public enum DisplayPositions
        {
            Director,
            Manager,
            Staff,
            Admin
        };

        public enum Positions
        {
            Director,
            Manager,
            Sales,
            Operations,
            Accountant,
            Administrator
        };
        public enum Functions
        {
            Director,
            Sales,
            Operations,
            Accountant
        };

        public enum Levels : int
        {
            [StringLabel("Director")]
            Director,
            [StringLabel("Deputy Director")]
            DeputyDirector,
            [StringLabel("Branch Manager")]
            BranchManager
        };
        #endregion
        #region Models
        [Required]
        [DisplayName("Full name")]
        public string FullName { get; set; }
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Description")]
        public string Note { get; set; }
        public long Id { get; set; }
        [DisplayName("Departerment")]
        public long DeptId { get; set; }
        public string DeptName { get; set; }
        [DisplayName("Company Name")]
        public long ComId { get; set; }
        public string ComName { get; set; }
        [DisplayName("Position")]
        public string RoleName { get; set; }
        [DisplayName("Level")]
        public int Level { get; set; }

        [DisplayName("Set your password")]
        public bool SetPass { get; set; }
        [DisplayName("Update ocean freight")]
        public bool AllowUpdateSeaRate { get; set; }
        [DisplayName("Update air freight")]
        public bool AllowUpdateAirRate { get; set; }

        [DisplayName("Sea Inbound")]
        public bool AllowTrackSeaIn { get; set; }
        [DisplayName("Sea Outbound")]
        public bool AllowTrackSeaOut { get; set; }
        [DisplayName("Air Inbound")]
        public bool AllowTrackAirIn { get; set; }
        [DisplayName("Air Outbound")]
        public bool AllowTrackAirOut { get; set; }
        [DisplayName("Inland Service")]
        public bool AllowTrackInlandSer { get; set; }
        [DisplayName("Project Service")]
        public bool AllowTrackProjectSer { get; set; }
        [DisplayName("Other")]
        public bool AllowTrackOtherSer { get; set; }

        [DisplayName("Approve for revenue")]
        public bool AllowApproRevenue { get; set; }
        [DisplayName("Raise quota for office")]
        public bool AllowQuotaOffice { get; set; }
        [DisplayName("Setting Policy for Sales")]
        public bool AllowSetSales { get; set; }
        [DisplayName("Raise quota for department")]
        public bool AllowQuotaDept { get; set; }
        [DisplayName("Setting for System")]
        public bool AllowSettingSystem { get; set; }

        [DisplayName("Update Agent")]
        public bool AllowUpdateAgent { get; set; }
        [DisplayName("Update Partner")]
        public bool AllowUpdatePartner { get; set; }
        [DisplayName("Sea Port")]
        public bool AllowSeaPort { get; set; }
        [DisplayName("Air Port")]
        public bool AllowAirPort { get; set; }
        [DisplayName("Customer")]
        public bool AllowCustomer { get; set; }
        [DisplayName("Trading")]
        public bool AllowTrading { get; set; }
        [DisplayName("Approved Stock")]
        public bool AllowApprovedStockCard { get; set; }
        [DisplayName("Freight")]
        public bool AllowFreight { get; set; }
        [DisplayName("Data")]
        public bool AllowDataTrading { get; set; }
        [DisplayName("Crate & Edit")]
        public bool AllowInforEdit { get; set; }
        [DisplayName("All Control")]
        public bool AllowInfoAll { get; set; }
        [DisplayName("Crate & Edit")]
        public bool AllowRegulationEdit { get; set; }
        [DisplayName("Approval")]
        public bool AllowRegulationApproval { get; set; }

        public bool IsActive { get; set; }
         [DisplayName("Checking Revenue")]
        public bool CheckingRevenue { get; set; }
        public List<ControlCompany> ControlCompany { get; set; }
        //CMR Function
         [DisplayName("Setting theo dõi phòng")]
        public bool AllowFollowDept { get; set; }
        [DisplayName("Edit company report")]
        public bool AllowEditReport { get; set; } 
        public List<Department> Departments { get; set; }
        public string DeptFollowIds { get; set; }
        public string EmailPassword { get; set; }
        [DisplayName("Email display name")]
        public string EmailNameDisplay { get; set; } 
        #endregion

    }
    public class DepartmentModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Department Name")]
        public string DeptName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        public string DeptCode { get; set; }
        public string DeptFunction { get; set; }
        public long ComId { get; set; }
        public bool IsActive { get; set; }
    }
    public class CompanyModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Office")]
        public string CompanyName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }

        public string CompanyCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class CompanyInfo
    {
        public static string COMPANY_LOGO = "COMPANY_LOGO";
        public static string COMPANY_HEADER = "COMPANY_HEADER";
        public static string COMPANY_FOOTER = "COMPANY_FOOTER";
        public static string ACCOUNT_INFO = "ACCOUNT_INFO";
        public string CompanyLogo { get; set; }
        public string CompanyHeader { get; set; }
        public string CompanyFooter { get; set; }
        public string AccountInfor { get; set; }
    }
    public class SettingModel
    {
        public static string TAX_COMMISSION = "TAX_COMMISSION";
        public static string PAGE_SETTING = "PAGE_SETTING"; 
        public static string CRM_DAYCANCEL_SETTING = "CRM_DAYCANCEL_SETTING"; 
        public string TaxCommistion { get; set; }
        public string PageNumber { get; set; }
        
        public long Id { get; set; }
        public string SaleType { get; set; }
        public double Bonus { get; set; }
        public bool Active { get; set; }
    }
    public class SalePlanModel
    {
        public enum PlanType
        {
            [StringLabel("Office")]
            OFFICE,
            [StringLabel("Department")]
            DEPARTMENT,
            [StringLabel("Quota in month")]
            QUOTA_IN_MONTH

        };

        public static SelectList PlanTypeList
        {
            get
            {
                var Plans = from PlanType p in Enum.GetValues(typeof(PlanType))
                            select new { Id = p, Name = p.GetStringLabel() };
                return new SelectList(Plans, "Id", "Name");
            }
        }

        public static string USER_SALE_PLAN_SESSION = "USER_SALE_PLAN_SESSION";
        public int Month { get; set; }
        public int Year { get; set; }
        public long OfficeId { get; set; }
        public long DeptId { get; set; }
        public long SaleId { get; set; }
        public string SaleName { get; set; }
        public double PlanValue { get; set; }
        public string PlanOfficeType { get; set; }
    }
    public class UserSalePlan
    {
        public string FullName { get; set; }
        public decimal? PlanValue { get; set; }
        public long Id { get; set; }
        public long ComId { get; set; }
        public long DeptId { get; set; }
        public UserSalePlan(long id, long DeptId, long ComId, string fullName, decimal? planValue)
        {
            this.Id = id;
            this.ComId = ComId;
            this.DeptId = DeptId;
            this.FullName = fullName;
            this.PlanValue = planValue;
        }
    }
}