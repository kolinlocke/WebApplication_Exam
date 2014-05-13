using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects;
using Layer02_Objects._System;
using Layer02_Objects.Modules_Objects;
using WebApplication_Exam;
using WebApplication_Exam.Base;

namespace WebApplication_Exam.Page
{
    public partial class Rights : ClsBaseList_Page
    {
        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            ClsBindDefinition BindDef = new ClsBindDefinition();
            BindDef.List_Gc = new List<ClsBindGridColumn>();
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Name", "Name", new Unit("90%")));
            BindDef.KeyName = "RecruitmentTestRightsID";
            BindDef.AllowPaging = true;
            BindDef.AllowSort = true;
            BindDef.IsPersistent = true;

            this.Setup(
                Layer02_Objects._System.Layer02_Constants.eSystem_Modules.Rights
                , new ClsRights(this.pCurrentUser)
                , BindDef
                , true
                , true);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.pBtn_New.Text = "New Role";
        }

    }
}