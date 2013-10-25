using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common.Connection;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Common;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects;
using Layer02_Objects.DataAccess;
using Layer02_Objects._System;
using Layer02_Objects.Modules_Base;
using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Telerik.Web.UI;

namespace WebApplication_Exam.Testing
{
    public partial class WebForm4 : ClsBaseMain_Page
    {
        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.pCurrentUser.AdministratorLogin();
            this.pCurrentUser.pIsLoggedIn = true;
            this.Setup(false, true);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            this.Selection.Setup(this.pCurrentUser);

            ClsBindDefinition BindDef = new ClsBindDefinition();
            BindDef.DataSourceName = "RecruitmentTestQuestions";
            BindDef.KeyName = "RecruitmentTestQuestionsID";

            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Question", "Question", 200));

            BindDef.List_Gc = new List<ClsBindGridColumn>(List_Gc);

            this.Selection.Setup_AddHandlers(this.Btn_Select, this.RadAjaxPanel1, BindDef, true, "", 600, 600);
        }
    }
}