using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSM.Models;

namespace SSM.Views.Shared
{
    public partial class TopNarBarUC : ViewUserControl<UsersModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Table1.Visible = false;
            UsersModel model1 = (UsersModel) Model;
            System.Console.Out.WriteLine("User Name = >>>>>" + model1.UserName);
        }
    }
}