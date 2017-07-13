using System.Linq;
using SSM.Models;

namespace SSM.Services
{
    public static class TradingPermission
    {
        public static bool IsAdmin(this User user)
        {
            return user.RoleName.Equals(UsersModel.Positions.Administrator.ToString());
        } 
        public static bool IsTrading(this User user)
        {
            if (user.IsAdmin())
                return true;
            if (user.AllowTrading)
                return true;
            //if (user.IsDirecter() || user.IsDepManager() || user.IsAccountant())
            //    return true;
            return false;

        }
        public static bool IsEditTrading(this User user)
        {
            if (user.IsAdmin())
                return true;
            if (user.IsDirecter() || user.IsAccountant())
                return false;
            if ( user.AllowTrading)
                return true;
            return false;
        }

        public static bool IsMainDirecter(this User user)
        {
            if (user.IsAdmin())
                return true;
            return user.RoleName.Equals(UsersModel.Positions.Director.ToString()) && user.DirectorLevel.Value == 1 && user.ComId == 1;
        }
        public static bool IsDirecter(this User user)
        {
            if (user.IsAdmin())
                return true;
            return user.RoleName.Equals(UsersModel.Positions.Director.ToString());
        }
        public static bool IsComDirecter(this User user)
        {
            if (user.IsAdmin())
                return true;
            return user.RoleName.Equals(UsersModel.Positions.Director.ToString()) && user.DirectorLevel.Value == 1;
        }

        public static bool IsDepOrDirecter(this User user)
        {
            if (user.IsAdmin() || user.IsDirecter())
                return true;
            return user.RoleName.Equals(UsersModel.Positions.Manager.ToString());
        }
        public static bool IsDepManager(this User user)
        {
            if (user.IsAdmin())
                return true;
            return user.RoleName.Equals(UsersModel.Positions.Manager.ToString());
        }

        public static bool IsAccountant(this User user)
        {
            if (user.IsAdmin())
                return true;
            return UsersModel.Functions.Accountant.ToString().Equals(user.Department != null ? user.Department.DeptFunction : string.Empty);
        }

        public static bool IsFreight(this User user)
        {
            if (user.IsAdmin())
                return true;
            if (user.AllowFreight != null && user.AllowFreight == true)
                return true;
            if (user.AllowTrading != null && user.AllowTrading == true)
                return false;
            return true;
        }

        public static bool IsOwnner(this User user, long id)
        {
            if (user.IsAdmin() || user.Id == id)
                return true;
            return false;
        }

        public static bool IsOperations(this User user)
        {
            return user.IsAdmin() || UsersModel.Positions.Operations.ToString().Equals(user.Department.DeptFunction);
        }
        public static bool IsOpsAndSales(this User user)
        {
            return user.IsAdmin() || UsersModel.Positions.Operations.ToString().Equals(user.Department.DeptFunction) || UsersModel.Positions.Sales.ToString().Equals(user.Department.DeptFunction);
        }
        public static bool IsStaff(this User user)
        {
            return !user.IsAdmin() && !user.IsDepOrDirecter();
        }
        public static bool IsDataTrading(this User user)
        {
            if (user.IsAdmin())
                return true;
            if (user.AllowDataTrading != null && user.AllowDataTrading == true && user.AllowTrading != null && user.AllowTrading == true)
                return true;
            return false;
        }
        // Information 
        public static bool IsEditNew(this User user, NewsModel model = null)
        {
            if (user.IsAdmin() || user.AllowInfoAll)
                return true;
            if (user.AllowInforEdit && model == null)
                return true;
            if (model != null)
            {
                if (user.AllowInforEdit && (model.CreaterBy != null && model.CreaterBy.Id == user.Id))
                    return true;
                if (model.ListUserUpdate != null && model.ListUserUpdate.Any())
                {
                    var check = model.ListUserUpdate.Contains(user.Id) && model.IsAllowAnotherUpdate;
                    return check;
                }
            }
            return false;
        }
        //Regulation 
        public static bool IsEditReula(this User user, NewsModel model = null)
        {
            if (user.IsAdmin())
                return true;
            if (user.AllowRegulationEdit && model == null)
                return true;
            if (model != null)
            {
                if (user.AllowRegulationEdit && (model.CreaterBy != null && model.CreaterBy.Id == user.Id))
                    return true;
                if (model.ListUserUpdate != null && model.ListUserUpdate.Any())
                {
                    var check = model.ListUserUpdate.Contains(user.Id) && model.IsAllowAnotherUpdate;
                    return check;
                }
            } 
            return false;
        }
        public static bool IsApprovedRegula(this User user)
        {
            if (user.IsAdmin())
                return true;
            return user.AllowRegulationApproval;
        }
        //Regulation 
        public static bool IsAdminAndAcct(this User user )
        {
            return user.IsAdmin() || user.IsDirecter()|| user.IsAccountant();
        } 
        
    }
}