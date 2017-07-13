using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSM.Models;
namespace SSM.Views.Shared
{
    public partial class WarningUser : ViewUserControl<User>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User model1 = (User)Model;
            System.Console.Out.WriteLine("User Name = >>>>>" + model1.UserName);
        }
    }
}