using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SSM.Common;

using SSM.Models;

namespace SSM.Services
{
    public interface ISettingService : IServices<Setting>
    {
        Setting GetByDataCode(string dataCode);
    }

    public class SettingServices : Services<Setting>, ISettingService
    {
        public Setting GetByDataCode(string dataCode)
        {
            return FindEntity(x => x.DataCode == dataCode);
        }
    }
    public interface UsersServices : IServices<User>
    {
        List<UsersModel> getAllUser();
        List<UsersModel> getOpnSaleUser();
        List<UsersModel> getUsersBy(UsersModel UsersModel1);
        List<UsersModel> getUsersBy(long DeptId);
        UsersModel getUserModelById(long UserId1);
        User getUserById(long UserId1);
        User getUserById(string UserName);
        void createUser(User User1);
        long createUser(UsersModel UsersModel1);
        void updateUser(UsersModel UsersModel1);
        User getUserLogIn(string UserName, string Password);
        IEnumerable<Company> getAllCompany();
        void createCompanyControl(long UserId1, long[] CompanyIds);
        IEnumerable<Department> getAllDepartment();
        IEnumerable<Department> GetAllDepartmentActive(User user1, bool isAll=false);
        Company getCompanyById(long id);
        Department getDepartmentById(long id);
        void updateCompany(CompanyModel CompanyModel1);
        void updateDepartment(DepartmentModel CompanyModel1);
        void insertCompany(CompanyModel CompanyModel1);
        void insertDepartment(DepartmentModel CompanyModel1);
        CompanyModel getCompanyModelById(long id);
        DepartmentModel getDepartmentModelById(long id);
        bool UpdateTaxCommission(long Tax);
        bool UpdatePageSeting(int page);
        bool UpdateSaleType(SaleType SaleType1);
        bool InsertSaleType(SaleType SaleType1);
        Setting getTaxCommissiong();
        Setting PageSettingNumber();
        IEnumerable<SaleType> getAllSaleTypes(bool Active);
        SaleType getSaleTypeById(long Id);
        bool DeleteSaleType(long Id);
        IEnumerable<UserSalePlan> getPlanDepts(DateTime SearchDate);
        IEnumerable<UserSalePlan> getPlanDepts(long CompanyId, DateTime SearchDate);
        IEnumerable<UserSalePlan> getPlanComs(DateTime SearchDate);
        IEnumerable<UserSalePlan> getPlanDept(long deptId, DateTime SearchDate);
        SalePlan getSalePlanByUser(long UserId1, DateTime SearchDate);
        SalePlan getSalePlanByDept(long DeptId1, DateTime SearchDate);
        SalePlan getSalePlanByCom(long ComdId1, DateTime SearchDate);
        bool UpdateSalePlan(long UserId, DateTime monthDate, double SalePlanValue);
        bool UpdateSalePlanDept(long DeptId, DateTime monthDate, double SalePlanValue);
        bool UpdateSalePlanCom(long DeptId, DateTime monthDate, double SalePlanValue);
        double getReportYear(long UserId1, int year);
        bool deleteUserById(long UserId1);
        bool deleteComById(long Id);
        bool deleteDeptById(long Id);
        IEnumerable<UserSalePlan> getQuataInMonth(long ComId, DateTime SearchDate);
        bool UpdateComSetting(String Name, Object Value);
        String getComSetting(String Name);
        Setting getComLogoSetting(String Name);
        void updateAny();
        //Add Office report
        double getReportYearDept(long DeptId, int year);
        IEnumerable<long> GetAllUsersComs(User user);
        bool SetActive(int id, bool isActive);
        bool SetActiveDepartment(int id, bool isActive);
        bool CheckSaleTypeFree(long id);
        List<User> GetAllSales(User user, bool isAll = true);
        IEnumerable<Department> GetSalesDept();
        IEnumerable<Company> GetCompanies(User user);
        IQueryable<User> GetAllSalesAndOp(User user, bool isAll = true);
        Setting CRMDayCanelSettingNumber();
        bool UpdateCRMDayCacelSeting(string day);
    }
    public class UsersServicesImpl : Services<User>, UsersServices
    {
        public List<UsersModel> getUsersBy(long DeptId)
        {
            try
            {
                List<User> UserList1 = (from User1 in db.Users
                                        where User1.Department.Id == DeptId && User1.IsActive == true
                                        orderby User1.FullName ascending
                                        select User1).ToList();
                List<UsersModel> UsersModel1 = new List<UsersModel>();
                if (UserList1 == null || UserList1.Count == 0)
                {
                    return UsersModel1;
                }
                foreach (User UserItem in UserList1)
                {
                    UsersModel1.Add(ConvertModel(UserItem));
                }
                return UsersModel1;
            }
            catch (Exception e) { }
            return null;
        }
        public void updateAny()
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }
        public bool deleteUserById(long UserId1)
        {
            try
            {
                User User1 = getUserById(UserId1);
                Delete(User1); 
                return true;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            return false;
        }
        public double getReportYear(long UserId1, int year)
        {
            try
            {
                var YearPlanTotal = (from SalePlan1 in db.SalePlans
                                     where (SalePlan1.User != null && SalePlan1.UserId == UserId1)
                                     && SalePlan1.PlanMonth.Value.Year == year
                                     group SalePlan1 by SalePlan1.User
                                         into _group
                                         select new YearPlan(Convert.ToDouble(_group.Sum(s => s.PlanValue)))).First().TotalPlan;
                return YearPlanTotal;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            return 0.0;
        }
        public double getReportYearDept(long DeptId, int year)
        {
            try
            {
                var YearPlanTotal = (from SalePlan1 in db.SalePlans
                                     where (SalePlan1.Department != null && SalePlan1.DeptId == DeptId)
                                     && SalePlan1.PlanMonth.Value.Year == year
                                     group SalePlan1 by SalePlan1.Department
                                         into _group
                                         select new YearPlan(Convert.ToDouble(_group.Sum(s => s.PlanValue)))).First().TotalPlan;
                return YearPlanTotal;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }

            return 0.0;
        }

        public IEnumerable<long> GetAllUsersComs(User user)
        {
            var list = Context.ControlCompanies.Where(x => x.UserId == user.Id).Select(c => c.ComId);
            return list;
        }

        public bool SetActive(int id, bool isActive)
        {
            var user = FindEntity(x => x.Id == id);
            if (user == null) return false;
            user.IsActive = isActive;
            Commited();
            return true;
        }

        public bool SetActiveDepartment(int id, bool isActive)
        {
            var depart = getDepartmentById(id);
            if (depart == null) return false;
            depart.IsActive = isActive;
            db.SubmitChanges();
            return true;
        }

        public bool CheckSaleTypeFree(long id)
        {
            var checkedFee = ListSaleType().Any(x => x.Id == id);
            return checkedFee;
        }

        public List<User> GetAllSales(User user, bool isAll = true)
        {
            var qr =
                GetQuery(
                    x =>
                        x.IsActive == true && !x.Department.DeptFunction.Equals(UsersModel.Positions.Accountant.ToString()) &&
                        !x.Department.DeptFunction.Equals(UsersModel.DisplayPositions.Director.ToString()));
            if (user.IsComDirecter())
            {
                var comId = user.ControlCompanies.Select(x => x.ComId).ToList();
                if (!comId.Contains(user.ComId.Value))
                {
                    comId.Add(user.ComId.Value);
                }
                qr = qr.Where(x => comId.Contains(x.ComId.Value));

            }
            if (user.IsAccountant())
            {
                qr = qr.Where(x => x.ComId == user.ComId);
            }
            if (!user.IsAdminAndAcct() && !isAll)
                qr = qr.Where(x => x.DeptId == user.DeptId);
            qr.ToList();
            return qr.OrderBy(x => x.FullName).ToList();
        }

        public IEnumerable<Department> GetSalesDept()
        {
            var list =
                Context.Departments.Where(
                    x => (
                        x.DeptFunction == UsersModel.Functions.Operations.ToString() ||
                        x.DeptFunction == UsersModel.Functions.Sales.ToString()) && x.IsActive);
            return list.OrderBy(x => x.DeptName).AsEnumerable();
        }

        public IEnumerable<Company> GetCompanies(User user)
        {
            var qr = Context.Companies.AsQueryable();
            if (user.IsComDirecter())
            {
                var comId = user.ControlCompanies.Select(x => x.ComId).ToList();
                if (!comId.Contains(user.ComId.Value))
                {
                    comId.Add(user.ComId.Value);
                }
                qr = qr.Where(x => comId.Contains(x.Id));

            }
            else
            {
                qr = qr.Where(x => x.Id == user.ComId);
            }
            return qr.AsEnumerable();
        }

        public List<SaleType> ListSaleType()
        {
            string sql = @"select * from SaleType   
                           where Name not in( select distinct SaleType from Shipment )";
            var listFree = Context.ExecuteQuery<SaleType>(sql);
            return listFree.ToList();
        }
        public IEnumerable<UserSalePlan> getPlanComs(DateTime SearchDate)
        {
            try
            {
                IEnumerable<UserSalePlan> LeftJoin = (from Com1 in db.Companies
                                                      join SalePlan12
                                                          in
                                                          (from SalePlan1 in db.SalePlans
                                                           where (SalePlan1.PlanMonth == null) || (SalePlan1.PlanMonth.Equals(SearchDate))
                                                           select SalePlan1)
                                                      on Com1.Id equals SalePlan12.OfficeId
                                                      into JoinUsers
                                                      from SalePlan11 in JoinUsers.DefaultIfEmpty()
                                                      select new UserSalePlan(
                                                          Com1.Id,
                                                          0,
                                                          Com1.Id,
                                                          Com1.CompanyName,
                                                         SalePlan11.PlanValue
                                                      ));
                return LeftJoin;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public SalePlan getSalePlanByCom(long ComdId1, DateTime SearchDate)
        {
            try
            {
                return db.SalePlans.FirstOrDefault(sp => sp.OfficeId == ComdId1 && (sp.PlanMonth == null || sp.PlanMonth.Equals(SearchDate)));
            }
            catch (Exception e) { }
            return null;
        }
        public bool UpdateSalePlanCom(long ComId, DateTime monthDate, double SalePlanValue)
        {
            SalePlan SalePlan1 = getSalePlanByCom(ComId, monthDate);
            try
            {
                if (SalePlan1 == null)
                {
                    SalePlan1 = new SalePlan();
                    SalePlan1.OfficeId = ComId;
                    SalePlan1.PlanMonth = monthDate;
                    SalePlan1.PlanValue = Convert.ToDecimal(SalePlanValue);
                    db.SalePlans.InsertOnSubmit(SalePlan1);
                    db.SubmitChanges();
                }
                else
                {
                    SalePlan1.PlanValue = Convert.ToDecimal(SalePlanValue);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public bool UpdateSalePlanDept(long DeptId, DateTime monthDate, double SalePlanValue)
        {
            SalePlan SalePlan1 = getSalePlanByDept(DeptId, monthDate);
            try
            {
                if (SalePlan1 == null)
                {
                    SalePlan1 = new SalePlan();
                    SalePlan1.DeptId = DeptId;
                    SalePlan1.PlanMonth = monthDate;
                    SalePlan1.PlanValue = Convert.ToDecimal(SalePlanValue);
                    db.SalePlans.InsertOnSubmit(SalePlan1);
                    db.SubmitChanges();
                }
                else
                {
                    SalePlan1.PlanValue = Convert.ToDecimal(SalePlanValue);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }
        public bool UpdateSalePlan(long UserId, DateTime monthDate, double SalePlanValue)
        {
            SalePlan SalePlan1 = getSalePlanByUser(UserId, monthDate);
            try
            {
                if (SalePlan1 == null)
                {
                    SalePlan1 = new SalePlan();
                    SalePlan1.UserId = UserId;
                    SalePlan1.PlanMonth = monthDate;
                    SalePlan1.PlanValue = Convert.ToDecimal(SalePlanValue);
                    db.SalePlans.InsertOnSubmit(SalePlan1);
                    db.SubmitChanges();
                }
                else
                {
                    SalePlan1.PlanValue = Convert.ToDecimal(SalePlanValue);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.Write(e.Message.ToString());
            }
            return false;
        }
        public SalePlan getSalePlanByDept(long DeptId1, DateTime SearchDate)
        {
            try
            {
                return db.SalePlans.FirstOrDefault(sp => sp.DeptId == DeptId1 && (sp.PlanMonth == null || sp.PlanMonth.Equals(SearchDate)));
            }
            catch (Exception e) { }
            return null;
        }
        public SalePlan getSalePlanByUser(long UserId1, DateTime SearchDate)
        {
            try
            {
                /* var Result = (from SalePlan1 in db.SalePlans
                         where SalePlan1.UserId == UserId1
                         && ((SalePlan1.PlanMonth == null) || (SalePlan1.PlanMonth.Equals(SearchDate)))
                         select SalePlan1);*/
                return db.SalePlans.FirstOrDefault(sp => sp.UserId == UserId1 && (sp.PlanMonth == null || sp.PlanMonth.Equals(SearchDate)));
            }
            catch (Exception e) { }
            return null;
        }
        public IEnumerable<UserSalePlan> getPlanDepts(DateTime SearchDate)
        {
            try
            {
                IEnumerable<UserSalePlan> LeftJoin = (from Dept1 in db.Departments
                                                      where "Sales,Operations".Split(',').Contains(Dept1.DeptFunction) && Dept1.IsActive==true
                                                      join SalePlan12
                                                          in
                                                          (from SalePlan1 in db.SalePlans
                                                           where (SalePlan1.PlanMonth == null) || (SalePlan1.PlanMonth.Equals(SearchDate))
                                                           select SalePlan1)
                                                      on Dept1.Id equals SalePlan12.DeptId
                                                      into JoinUsers
                                                      from SalePlan11 in JoinUsers.DefaultIfEmpty()
                                                      select new UserSalePlan(
                                                          Dept1.Id,
                                                          Dept1.Id,
                                                          Dept1.Company.Id,
                                                          Dept1.DeptName,
                                                         SalePlan11.PlanValue
                                                      ));
                return LeftJoin;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public IEnumerable<UserSalePlan> getPlanDepts(long CompanyId, DateTime SearchDate)
        {
            try
            {
                IEnumerable<UserSalePlan> LeftJoin = (from Dept1 in db.Departments
                                                      where Dept1.ComId == CompanyId && "Sales,Operations".Split(',').Contains(Dept1.DeptFunction) && Dept1.IsActive
                                                      join SalePlan12
                                                          in
                                                          (from SalePlan1 in db.SalePlans
                                                           where (SalePlan1.PlanMonth == null) || (SalePlan1.PlanMonth.Equals(SearchDate))
                                                           select SalePlan1)
                                                      on Dept1.Id equals SalePlan12.DeptId
                                                      into JoinUsers
                                                      from SalePlan11 in JoinUsers.DefaultIfEmpty()
                                                      select new UserSalePlan(
                                                          Dept1.Id,
                                                          Dept1.Id,
                                                          Dept1.Company.Id,
                                                          Dept1.DeptName,
                                                         SalePlan11.PlanValue
                                                      ));
                return LeftJoin;
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public IEnumerable<UserSalePlan> getQuataInMonth(long ComId, DateTime SearchDate)
        {
            try
            {
                IEnumerable<UserSalePlan> LeftJoin = (from User1 in db.Users
                                                      where User1.ComId == ComId
                                                      join SalePlan12
                                                          in
                                                          (from SalePlan1 in db.SalePlans
                                                           where (SalePlan1.PlanMonth == null) || (SalePlan1.PlanMonth.Equals(SearchDate))
                                                           select SalePlan1)
                                                      on User1.Id equals SalePlan12.UserId
                                                      into JoinUsers
                                                      from SalePlan11 in JoinUsers.DefaultIfEmpty()
                                                      select new UserSalePlan(
                                                          User1.Id,
                                                          User1.DeptId.Value,
                                                          User1.ComId.Value,
                                                          User1.FullName,
                                                         SalePlan11.PlanValue
                                                      ));
                return LeftJoin;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public IEnumerable<UserSalePlan> getPlanDept(long deptId, DateTime SearchDate)
        {
            try
            {
                IEnumerable<UserSalePlan> LeftJoin = (from User1 in db.Users
                                                      where User1.DeptId == deptId
                                                      join SalePlan12
                                                          in
                                                          (from SalePlan1 in db.SalePlans
                                                           where (SalePlan1.PlanMonth == null) || (SalePlan1.PlanMonth.Equals(SearchDate))
                                                           select SalePlan1)
                                                      on User1.Id equals SalePlan12.UserId
                                                      into JoinUsers
                                                      from SalePlan11 in JoinUsers.DefaultIfEmpty()
                                                      select new UserSalePlan(
                                                          User1.Id,
                                                           User1.DeptId.Value,
                                                          User1.ComId.Value,
                                                          User1.FullName,
                                                         SalePlan11.PlanValue
                                                      ));
                return LeftJoin;
            }
            catch (Exception e)
            {
            }
            return null;
        }
        private DataClasses1DataContext db;
        public UsersServicesImpl()
        {
            db = new DataClasses1DataContext();
        }
        public List<UsersModel> getAllUser()
        {
            try
            {
                List<User> UserList1 = (from user1 in db.Users
                                        where user1.RoleName != null && !user1.RoleName.Equals("Administrator")
                                        select user1).ToList();
                List<UsersModel> UsersModel1 = new List<UsersModel>();
                if (UserList1 == null || UserList1.Count == 0)
                {
                    return null;
                }
                foreach (User UserItem in UserList1)
                {
                    UsersModel1.Add(ConvertModel(UserItem));
                }
                return UsersModel1;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        public List<UsersModel> getOpnSaleUser()
        {
            try
            {
                List<User> UserList1 = (from user1 in db.Users
                                        where user1.Department != null && ("Operations".Equals(user1.Department.DeptFunction)
                                        || "Sales".Equals(user1.Department.DeptFunction)
                                        && user1.IsActive == true)
                                        orderby user1.FullName ascending
                                        select user1).ToList();
                List<UsersModel> UsersModel1 = new List<UsersModel>();
                if (UserList1 == null || UserList1.Count == 0)
                {
                    return new List<UsersModel>();
                }
                foreach (User UserItem in UserList1)
                {
                    UsersModel1.Add(ConvertModel(UserItem));
                }
                return UsersModel1;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return null;
        }
        #region Convert User data model
        public static UsersModel ConvertModel(User User1)
        {
            UsersModel UsersModel1 = new UsersModel();
            UsersModel1.Address = User1.Address;
            UsersModel1.Email = User1.Email;
            UsersModel1.FullName = User1.FullName;
            UsersModel1.Id = User1.Id;
            UsersModel1.Note = User1.Note;
            UsersModel1.Password = User1.PassWord;
            UsersModel1.UserName = User1.UserName;
            UsersModel1.RoleName = UsersModel.RevertFromRoleName(User1.RoleName);
            UsersModel1.ComId = (long)(User1.ComId ?? 0);
            UsersModel1.DeptId = (long)(User1.DeptId ?? 0);
            //------------------------------------------------------
            UsersModel1.AllowCustomer = User1.AllowCustomer;
            UsersModel1.AllowAirPort = User1.AllowAirPort;
            UsersModel1.AllowApproRevenue = User1.AllowApproRevenue;
            UsersModel1.AllowQuotaDept = User1.AllowQuotaDept;
            UsersModel1.AllowQuotaOffice = User1.AllowQuotaOffice;
            UsersModel1.AllowSeaPort = User1.AllowSeaPort;
            UsersModel1.AllowSetSales = User1.AllowSetSales;
            UsersModel1.AllowSettingSystem = User1.AllowSettingSystem;
            UsersModel1.AllowTrackAirIn = User1.AllowTrackAirIn;
            UsersModel1.AllowTrackAirOut = User1.AllowTrackAirOut;
            UsersModel1.AllowTrackInlandSer = User1.AllowTrackInlandSer;
            UsersModel1.AllowTrackOtherSer = User1.AllowTrackOtherSer;
            UsersModel1.AllowTrackProjectSer = User1.AllowTrackProjectSer;
            UsersModel1.AllowTrackSeaIn = User1.AllowTrackSeaIn;
            UsersModel1.AllowTrackSeaOut = User1.AllowTrackSeaOut;
            UsersModel1.AllowUpdateAgent = User1.AllowUpdateAgent;
            UsersModel1.AllowUpdateAirRate = User1.AllowUpdateAirRate;
            UsersModel1.AllowUpdatePartner = User1.AllowUpdatePartner;
            UsersModel1.AllowUpdateSeaRate = User1.AllowUpdateSeaRate;
            UsersModel1.AllowTrading = User1.AllowTrading;
            UsersModel1.AllowApprovedStockCard = User1.AllowApprovedStockCard;
            UsersModel1.AllowFreight = User1.AllowFreight;
            UsersModel1.AllowDataTrading = User1.AllowDataTrading;
            UsersModel1.AllowInforEdit = User1.AllowInforEdit;
            UsersModel1.AllowInfoAll = User1.AllowInfoAll;
            UsersModel1.AllowRegulationEdit = User1.AllowRegulationEdit;
            UsersModel1.AllowRegulationApproval = User1.AllowRegulationApproval;
            UsersModel1.Level = (int)(User1.DirectorLevel ?? 0);
            UsersModel1.SetPass = (bool)User1.SetPass;
            UsersModel1.ComName = User1.Company != null ? User1.Company.CompanyName : "";
            UsersModel1.DeptName = User1.Department != null ? User1.Department.DeptName : "";
            UsersModel1.ControlCompany = User1.ControlCompanies.ToList();
            UsersModel1.IsActive = User1.IsActive;
            UsersModel1.CheckingRevenue = User1.CheckingRevenue;
            UsersModel1.AllowEditReport = User1.AllowEditReport;
            UsersModel1.AllowFollowDept = User1.AllowFollowDept;
            UsersModel1.DeptFollowIds = User1.DeptFollowIds;
            UsersModel1.EmailNameDisplay = User1.EmailNameDisplay;
            if (!string.IsNullOrEmpty(User1.EmailPassword))
            {
                UsersModel1.EmailPassword = User1.EmailPassword.Decrypt();
            }


            return UsersModel1;
        }
        private User revertModel(UsersModel UsersModel1)
        {
            User User1 = new User();
            User1.Address = UsersModel1.Address;
            User1.Email = UsersModel1.Email;
            User1.FullName = UsersModel1.FullName;
            User1.Id = UsersModel1.Id;
            User1.Note = UsersModel1.Note;
            User1.PassWord = UsersModel1.Password;
            User1.UserName = UsersModel1.UserName;
            User1.ComId = UsersModel1.ComId;
            User1.DeptId = UsersModel1.DeptId;
            User1.RoleName = UsersModel.ConvertToRoleName(UsersModel1.RoleName, getDepartmentById(UsersModel1.DeptId).DeptFunction);
            User1.AllowAirPort = UsersModel1.AllowAirPort;
            User1.AllowCustomer = UsersModel1.AllowCustomer;
            User1.AllowApproRevenue = UsersModel1.AllowApproRevenue;
            User1.AllowQuotaDept = UsersModel1.AllowQuotaDept;
            User1.AllowQuotaOffice = UsersModel1.AllowQuotaOffice;
            User1.AllowSeaPort = UsersModel1.AllowSeaPort;
            User1.AllowSetSales = UsersModel1.AllowSetSales;
            User1.AllowSettingSystem = UsersModel1.AllowSettingSystem;
            User1.AllowTrackAirIn = UsersModel1.AllowTrackAirIn;
            User1.AllowTrackAirOut = UsersModel1.AllowTrackAirOut;
            User1.AllowTrackInlandSer = UsersModel1.AllowTrackInlandSer;
            User1.AllowTrackOtherSer = UsersModel1.AllowTrackOtherSer;
            User1.AllowTrackProjectSer = UsersModel1.AllowTrackProjectSer;
            User1.AllowTrackSeaIn = UsersModel1.AllowTrackSeaIn;
            User1.AllowTrackSeaOut = UsersModel1.AllowTrackSeaOut;
            User1.AllowUpdateAgent = UsersModel1.AllowUpdateAgent;
            User1.AllowUpdateAirRate = UsersModel1.AllowUpdateAirRate;
            User1.AllowUpdatePartner = UsersModel1.AllowUpdatePartner;
            User1.AllowUpdateSeaRate = UsersModel1.AllowUpdateSeaRate;
            User1.AllowTrading = UsersModel1.AllowTrading;
            User1.AllowFreight = UsersModel1.AllowFreight;
            User1.AllowDataTrading = UsersModel1.AllowDataTrading;
            User1.AllowApprovedStockCard = UsersModel1.AllowApprovedStockCard;
            User1.AllowInforEdit = UsersModel1.AllowInforEdit;
            User1.AllowInfoAll = UsersModel1.AllowInfoAll;
            User1.AllowRegulationEdit = UsersModel1.AllowRegulationEdit;
            User1.AllowRegulationApproval = UsersModel1.AllowRegulationApproval;
            User1.DirectorLevel = UsersModel1.Level;
            User1.SetPass = UsersModel1.SetPass;
            User1.IsActive = UsersModel1.IsActive;
            User1.CheckingRevenue = UsersModel1.CheckingRevenue;
            User1.AllowEditReport = UsersModel1.AllowEditReport;
            User1.AllowFollowDept = UsersModel1.AllowFollowDept;
            User1.DeptFollowIds = UsersModel1.DeptFollowIds;
            User1.EmailNameDisplay = UsersModel1.EmailNameDisplay;
            if (!string.IsNullOrEmpty(UsersModel1.EmailPassword))
            {
                User1.EmailPassword = UsersModel1.EmailPassword.Encrypt();
            }

            return User1;
        }
        private void revertModel(UsersModel UsersModel1, User User1)
        {
            User1.Address = UsersModel1.Address;
            User1.Email = UsersModel1.Email;
            User1.FullName = UsersModel1.FullName;
            //User1.Id = UsersModel1.Id;
            User1.Note = UsersModel1.Note;
            User1.PassWord = UsersModel1.Password;
            User1.UserName = UsersModel1.UserName;
            User1.ComId = UsersModel1.ComId;
            User1.DeptId = UsersModel1.DeptId;
            User1.RoleName = UsersModel.ConvertToRoleName(UsersModel1.RoleName, getDepartmentById(UsersModel1.DeptId).DeptFunction);
            User1.AllowAirPort = UsersModel1.AllowAirPort;
            User1.AllowCustomer = UsersModel1.AllowCustomer;
            User1.AllowApproRevenue = UsersModel1.AllowApproRevenue;
            User1.AllowQuotaDept = UsersModel1.AllowQuotaDept;
            User1.AllowQuotaOffice = UsersModel1.AllowQuotaOffice;
            User1.AllowSeaPort = UsersModel1.AllowSeaPort;
            User1.AllowSetSales = UsersModel1.AllowSetSales;
            User1.AllowSettingSystem = UsersModel1.AllowSettingSystem;
            User1.AllowTrackAirIn = UsersModel1.AllowTrackAirIn;
            User1.AllowTrackAirOut = UsersModel1.AllowTrackAirOut;
            User1.AllowTrackInlandSer = UsersModel1.AllowTrackInlandSer;
            User1.AllowTrackOtherSer = UsersModel1.AllowTrackOtherSer;
            User1.AllowTrackProjectSer = UsersModel1.AllowTrackProjectSer;
            User1.AllowTrackSeaIn = UsersModel1.AllowTrackSeaIn;
            User1.AllowTrackSeaOut = UsersModel1.AllowTrackSeaOut;
            User1.AllowUpdateAgent = UsersModel1.AllowUpdateAgent;
            User1.AllowUpdateAirRate = UsersModel1.AllowUpdateAirRate;
            User1.AllowUpdatePartner = UsersModel1.AllowUpdatePartner;
            User1.AllowUpdateSeaRate = UsersModel1.AllowUpdateSeaRate;
            User1.AllowTrading = UsersModel1.AllowTrading;
            User1.AllowApprovedStockCard = UsersModel1.AllowApprovedStockCard;
            User1.AllowFreight = UsersModel1.AllowFreight;
            User1.AllowDataTrading = UsersModel1.AllowDataTrading;
            User1.AllowInforEdit = UsersModel1.AllowInforEdit;
            User1.AllowInfoAll = UsersModel1.AllowInfoAll;
            User1.AllowRegulationEdit = UsersModel1.AllowRegulationEdit;
            User1.AllowRegulationApproval = UsersModel1.AllowRegulationApproval;
            User1.DirectorLevel = UsersModel1.Level;
            User1.SetPass = UsersModel1.SetPass;
            User1.IsActive = UsersModel1.IsActive;
            User1.CheckingRevenue = UsersModel1.CheckingRevenue;
            User1.AllowEditReport = UsersModel1.AllowEditReport;
            User1.AllowFollowDept = UsersModel1.AllowFollowDept;
            User1.DeptFollowIds = UsersModel1.DeptFollowIds;
            User1.EmailNameDisplay = UsersModel1.EmailNameDisplay;
            if (!string.IsNullOrEmpty(UsersModel1.EmailPassword))
            {
                User1.EmailPassword = UsersModel1.EmailPassword.Encrypt();
            }
        }
        #endregion
        public List<UsersModel> getUsersBy(UsersModel UsersModel1)
        {
            return null;
        }
        public long createUser(UsersModel UsersModel1)
        {
            User User1 = revertModel(UsersModel1);
            db.Users.InsertOnSubmit(User1);
            db.SubmitChanges();
            return User1.Id;
        }
        public void createUser(User User1)
        {
            db.Users.InsertOnSubmit(User1);
            db.SubmitChanges();
        }
        public void updateUser(UsersModel UsersModel1)
        {
            User OldUser = getUserById(UsersModel1.Id);
            revertModel(UsersModel1, OldUser);
            Commited();
        }
        public UsersModel getUserModelById(long UserId1)
        {
            User Users1 = (from Users_ in db.Users
                           where Users_.Id == UserId1
                           select Users_).First();
            if (Users1 != null)
            {
                return ConvertModel(Users1);
            }
            return null;
        }
        public User getUserById(long UserId1)
        {
            try
            {
                User Users1 = FindEntity(x => x.Id == UserId1);
                return Users1;
            }
            catch (Exception e) { }
            return null;
        }
        public User getUserById(String UserName)
        {
            try
            {
                User Users1 = (from Users_ in db.Users
                               where Users_.UserName == UserName
                               select Users_).First();
                return Users1;
            }
            catch (Exception e)
            {
            }
            return null;
        }
        public void createCompanyControl(long UserId1, long[] CompanyIds)
        {
            try
            {
                var ctr = db.ControlCompanies.Where(x => x.UserId == UserId1);
                db.ControlCompanies.DeleteAllOnSubmit(ctr);
                if (CompanyIds != null && CompanyIds.Any())
                {
                    foreach (long CompanyId1 in CompanyIds)
                    {
                        ControlCompany ControlCompany1 = new ControlCompany();
                        ControlCompany1.ComId = CompanyId1;
                        ControlCompany1.UserId = UserId1;
                        db.ControlCompanies.InsertOnSubmit(ControlCompany1);
                    }
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
        public User getUserLogIn(string UserName, string Password)
        {
            IEnumerable<User> UserList1 = (from Users_ in db.Users
                                           where Users_.UserName == UserName && Users_.PassWord == Password && Users_.IsActive
                                           select Users_);
            if (UserList1 == null || UserList1.Count() <= 0)
            {
                return null;
            }
            else
            {
                return UserList1.First();
            }
        }

        public IEnumerable<Department> getAllDepartment()
        {
            try
            {
                return db.Departments.ToList();
            }
            catch (Exception e)
            {
            }
            return new List<Department>();
        }

        public IEnumerable<Department> GetAllDepartmentActive(User user, bool isAll = false)
        {
            return db.Departments.Where(x => x.IsActive
                && ((user.IsAdminAndAcct() || isAll) || (isAll == false && user.DeptId == x.Id))
                ).OrderBy(x=>x.DeptName).Select(x => x);
        }

        public IEnumerable<Company> getAllCompany()
        {
            try
            {
                return db.Companies.OrderBy(x => x.CompanyName).ToList();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return new List<Company>();
        }
        public void updateCompany(CompanyModel CompanyModel1)
        {
            Company Company1 = getCompanyById(CompanyModel1.Id);
            revertCompany(CompanyModel1, Company1);
            db.SubmitChanges();
        }
        public void updateDepartment(DepartmentModel DepartmentModel1)
        {
            try
            {
                Department Department1 = getDepartmentById(DepartmentModel1.Id);
                revertDepartment(DepartmentModel1, Department1);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }
        public void insertCompany(CompanyModel CompanyModel1)
        {
            Company Company1 = revertCompany(CompanyModel1);
            db.Companies.InsertOnSubmit(Company1);
            db.SubmitChanges();
        }
        public bool deleteComById(long Id)
        {
            try
            {
                Company Company1 = getCompanyById(Id);
                db.Companies.DeleteOnSubmit(Company1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        public void insertDepartment(DepartmentModel DepartmentModel1)
        {
            Department Department1 = revertDepartment(DepartmentModel1);
            db.Departments.InsertOnSubmit(Department1);
            db.SubmitChanges();
        }
        public Company getCompanyById(long id)
        {
            try
            {
                return db.Companies.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }
        public Department getDepartmentById(long id)
        {
            try
            {
                return db.Departments.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }
        public bool deleteDeptById(long Id)
        {
            try
            {
                Department Department1 = getDepartmentById(Id);
                db.Departments.DeleteOnSubmit(Department1);
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }
        #region Convert Departmant Model Data
        private DepartmentModel revertDepartment(Department Department1)
        {
            DepartmentModel DepartmentModel1 = new DepartmentModel();
            DepartmentModel1.DeptCode = Department1.DeptCode;
            DepartmentModel1.DeptName = Department1.DeptName;
            DepartmentModel1.Description = Department1.Description;
            DepartmentModel1.DeptFunction = Department1.DeptFunction;
            DepartmentModel1.ComId = Department1.ComId.Value;
            DepartmentModel1.IsActive = Department1.IsActive;
            return DepartmentModel1;
        }
        private Department revertDepartment(DepartmentModel DepartmentModel1)
        {
            Department Department1 = new Department();
            Department1.DeptCode = DepartmentModel1.DeptCode;
            Department1.DeptName = DepartmentModel1.DeptName;
            Department1.Description = DepartmentModel1.Description;
            Department1.DeptFunction = DepartmentModel1.DeptFunction;
            Department1.ComId = DepartmentModel1.ComId;
            Department1.IsActive = DepartmentModel1.IsActive;
            return Department1;
        }
        private void revertDepartment(DepartmentModel DepartmentModel1, Department Department1)
        {
            Department1.DeptCode = DepartmentModel1.DeptCode;
            Department1.DeptName = DepartmentModel1.DeptName;
            Department1.Description = DepartmentModel1.Description;
            Department1.DeptFunction = DepartmentModel1.DeptFunction;
            Department1.ComId = DepartmentModel1.ComId;
            Department1.IsActive = DepartmentModel1.IsActive;
        }
        #endregion
        #region Convert  Company Model Data
        private CompanyModel revertCompany(Company Company1)
        {
            CompanyModel CompanyModel1 = new CompanyModel();
            CompanyModel1.CompanyCode = Company1.CompanyCode;
            CompanyModel1.CompanyName = Company1.CompanyName;
            CompanyModel1.Description = Company1.Description;
            CompanyModel1.Address = Company1.Address;
            CompanyModel1.PhoneNumber = Company1.PhoneNumber;
            return CompanyModel1;
        }
        private Company revertCompany(CompanyModel CompanyModel1)
        {
            Company Company1 = new Company();
            Company1.CompanyCode = CompanyModel1.CompanyCode;
            Company1.CompanyName = CompanyModel1.CompanyName;
            Company1.Description = CompanyModel1.Description;
            Company1.Address = CompanyModel1.Address;
            Company1.PhoneNumber = CompanyModel1.PhoneNumber;
            return Company1;
        }
        private void revertCompany(CompanyModel CompanyModel1, Company Company1)
        {
            Company1.CompanyCode = CompanyModel1.CompanyCode;
            Company1.CompanyName = CompanyModel1.CompanyName;
            Company1.Description = CompanyModel1.Description;
            Company1.Address = CompanyModel1.Address;
            Company1.PhoneNumber = CompanyModel1.PhoneNumber;
        }
        #endregion
        public CompanyModel getCompanyModelById(long id)
        {
            Company Company1 = getCompanyById(id);
            if (Company1 != null)
            {
                return revertCompany(Company1);
            }
            return null;
        }
        public DepartmentModel getDepartmentModelById(long id)
        {
            Department Department1 = getDepartmentById(id);
            if (Department1 != null)
            {
                return revertDepartment(Department1);
            }
            return null;
        }

        private Setting getTaxSetting()
        {
            try
            {
                return db.Settings.FirstOrDefault(s => s.DataCode == SettingModel.TAX_COMMISSION);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        private Setting getSetting(String Name)
        {
            try
            {
                return db.Settings.FirstOrDefault(s => s.DataCode == Name);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public bool UpdateComSetting(String Name, Object Value)
        {
            try
            {
                Setting TaxSetting = getSetting(Name);
                if (TaxSetting != null)
                {
                    TaxSetting.DataValue = Value.ToString();
                }
                else
                {
                    Setting Setting1 = new Setting();
                    Setting1.DataCode = Name;
                    Setting1.DataValue = Value.ToString();
                    db.Settings.InsertOnSubmit(Setting1);
                }
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
            return true;
        }

        public String getComSetting(String Name)
        {
            Setting TaxSetting = getSetting(Name);
            if (TaxSetting != null)
            {
                return TaxSetting.DataValue;
            }
            return "";
        }

        public Setting getComLogoSetting(String Name)
        {
            Setting TaxSetting = getSetting(Name);
            if (TaxSetting != null)
            {
                return TaxSetting;
            }
            return null;
        }
        public bool UpdateTaxCommission(long Tax)
        {
            try
            {
                Setting taxSetting = getTaxSetting();
                if (taxSetting != null)
                {
                    taxSetting.DataValue = Tax.ToString();
                }
                else
                {
                    Setting setting1 = new Setting();
                    setting1.DataCode = SettingModel.TAX_COMMISSION;
                    setting1.DataValue = Tax.ToString();
                    db.Settings.InsertOnSubmit(setting1);
                }
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool UpdatePageSeting(int page)
        {
            try
            {
                Setting pageSetting = Context.Settings.FirstOrDefault(x => x.DataCode == SettingModel.PAGE_SETTING);
                if (pageSetting != null)
                {
                    pageSetting.DataValue = page.ToString();
                }
                else
                {
                    Setting setting1 = new Setting();
                    setting1.DataCode = SettingModel.PAGE_SETTING;
                    setting1.DataValue = page.ToString();
                    db.Settings.InsertOnSubmit(setting1);
                }
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool UpdateCRMDayCacelSeting(string day)
        {
            try
            {
                Setting pageSetting = Context.Settings.FirstOrDefault(x => x.DataCode == SettingModel.CRM_DAYCANCEL_SETTING);
                if (pageSetting != null)
                {
                    pageSetting.DataValue = day;
                }
                else
                {
                    Setting setting1 = new Setting();
                    setting1.DataCode = SettingModel.PAGE_SETTING;
                    setting1.DataValue = day;
                    db.Settings.InsertOnSubmit(setting1);
                }
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
            return true;
        }
        public bool UpdateSaleType(SaleType SaleType1)
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
            return true;
        }

        public bool InsertSaleType(SaleType SaleType1)
        {
            try
            {
                db.SaleTypes.InsertOnSubmit(SaleType1);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
            return true;
        }

        public Setting getTaxCommissiong()
        {
            return getTaxSetting();
        }

        public Setting PageSettingNumber()
        {
            Setting pageSetting = Context.Settings.FirstOrDefault(x => x.DataCode == SettingModel.PAGE_SETTING);
            if (pageSetting == null)
            {
                pageSetting = new Setting()
                {
                    DataValue = "100",
                    DataCode = SettingModel.PAGE_SETTING
                };

                Context.Settings.InsertOnSubmit(pageSetting);
                Commited();
            }

            return pageSetting;
        }
        public Setting CRMDayCanelSettingNumber()
        {
            Setting pageSetting = Context.Settings.FirstOrDefault(x => x.DataCode == SettingModel.CRM_DAYCANCEL_SETTING);
            if (pageSetting == null)
            {
                pageSetting = new Setting()
                {
                    DataValue = "365",
                    DataCode = SettingModel.CRM_DAYCANCEL_SETTING
                };

                Context.Settings.InsertOnSubmit(pageSetting);
                Commited();
            }

            return pageSetting;
        }

        public SaleType getSaleTypeById(long Id)
        {
            try
            {
                return db.SaleTypes.FirstOrDefault(s => s.Id == Id);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }
        public IEnumerable<SaleType> getAllSaleTypes(bool Active)
        {
            try
            {
                return (from SaleType1 in db.SaleTypes
                        where !Active || SaleType1.Active == true
                        select SaleType1);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return null;
        }

        public bool DeleteSaleType(long Id)
        {
            try
            {

                db.SaleTypes.DeleteOnSubmit(getSaleTypeById(Id));
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            return false;
        }

        public IQueryable<User> GetAllSalesAndOp(User user, bool isAll = true)
        {
            var qr =
                GetQuery(
                    x =>
                        x.IsActive == true && !x.Department.DeptFunction.Equals(UsersModel.Positions.Operations.ToString()) &&
                        !x.Department.DeptFunction.Equals(UsersModel.Positions.Sales.ToString()));
            if (user.IsComDirecter())
            {
                var comId = user.ControlCompanies.Select(x => x.ComId).ToList();
                if (!comId.Contains(user.ComId.Value))
                {
                    comId.Add(user.ComId.Value);
                }
                qr = qr.Where(x => comId.Contains(x.ComId.Value));

            }
            if (user.IsAccountant())
            {
                qr = qr.Where(x => x.ComId == user.ComId);
            }
            if (!user.IsAdminAndAcct() && !isAll)
                qr = qr.Where(x => x.DeptId == user.DeptId);
            return qr.OrderBy(x => x.FullName);
        }
    }
}