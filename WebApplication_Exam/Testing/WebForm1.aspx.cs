using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Objects;

namespace WebApplication_Exam.Testing
{
    public partial class WebForm1 : WebApplication_Exam.Base.ClsBaseMain_Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!this.IsPostBack)
            {
                List<ClsBindGridColumn_Telerik> List_Gct = new List<ClsBindGridColumn_Telerik>();
                
                ClsBindGridColumn_Telerik RedirectColumn = new ClsBindGridColumn_Telerik("", "", 50, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
                RedirectColumn.mCommandName = "Select";
                RedirectColumn.mFieldText = ">>";
                List_Gct.Add(RedirectColumn);

                List_Gct.Add(new ClsBindGridColumn_Telerik("RecruitmentTestQuestionsID", "Question ID", 120));
                List_Gct.Add(new ClsBindGridColumn_Telerik("Question", "Question Description", 400));
                List_Gct.Add(new ClsBindGridColumn_Telerik("Status_Desc", "Status", 120));
                List_Gct.Add(new ClsBindGridColumn_Telerik("UserName_CreatedBy", "Created By", 120));
                List_Gct.Add(new ClsBindGridColumn_Telerik("UserName_ApprovedBy", "Approved By", 120));

                ClsBindGridColumn_Telerik DeleteColumn = new ClsBindGridColumn_Telerik("", "", 100, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
                DeleteColumn.mCommandName = "Delete";
                DeleteColumn.mFieldText = "Delete";
                List_Gct.Add(DeleteColumn);

                this.Grid.Setup_WithRequery(this.Master.pCurrentUser, new Layer02_Objects.Modules_Objects.ClsQuestion(), List_Gct);

                /*
                List_Gct = new List<ClsBindGridColumn_Telerik>();
                List_Gct.Add(new ClsBindGridColumn_Telerik("Name", "Name", 100));
                RedirectColumn = new ClsBindGridColumn_Telerik("", "", 50, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
                RedirectColumn.mCommandName = "Select";
                RedirectColumn.mFieldText = ">>";            
                List_Gct.Insert(0, RedirectColumn);

                DeleteColumn = new ClsBindGridColumn_Telerik("", "", 100, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
                DeleteColumn.mCommandName = "Delete";
                DeleteColumn.mFieldText = "Delete";
                List_Gct.Insert(2, DeleteColumn);

                this.Grid2.Setup(this.Master.pCurrentUser, new Layer02_Objects.Modules_Objects.ClsUser(), List_Gct);
                */
            }
        }
    }
}