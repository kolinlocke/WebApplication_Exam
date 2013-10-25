using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
//using Layer01_Common.Connection;
using Layer02_Objects;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
//using Layer02_Objects.Modules_Base.Objects;
using Layer02_Objects._System;
using WebApplication_Exam.Base;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace WebApplication_Exam.Page
{
    public partial class Menu : ClsBaseMain_Page
    {
        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Setup(false, true);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack) { this.LoadMenu(); }
        }

        #endregion

        #region _Methods

        void LoadMenu()
        {
            DataTable Dt_Menu;
            if (this.pCurrentUser.pIsSystemAdmin)
            {
                ClsBase Base = new ClsBase();
                ClsQueryCondition Qc = Base.pDa.CreateQueryCondition();
                Qc.Add("IsHidden", "0", typeof(bool).Name, "0");
                Dt_Menu = Base.pDa.GetQuery("uvw_System_Modules", "", Qc, "Parent_OrderIndex, OrderIndex");
            }
            else
            {
                List<Do_Constants.Str_Parameters> List_Sp = new List<Do_Constants.Str_Parameters>();
                List_Sp.Add(new Do_Constants.Str_Parameters("UserID", this.pCurrentUser.pUserID));
                
                ClsConnection_SqlServer Cn = new ClsConnection_SqlServer();
                Dt_Menu = Cn.ExecuteQuery("usp_System_Modules_Load", List_Sp).Tables[0];
            }

            this.Panel_Menu.Controls.Add(new Literal() { Text = "<ul>" });

            foreach (DataRow Dr in Dt_Menu.Rows)
            { this.Panel_Menu.Controls.Add(new Literal() { Text = @"<li><a href='" + Do_Methods.Convert_String(Dr["Module_List"]) + "'> " + Do_Methods.Convert_String(Dr["Name"]) + "</a></li>" }); }

            this.Panel_Menu.Controls.Add(new Literal() { Text = "</ul>" });
        }

        #endregion
    }
}