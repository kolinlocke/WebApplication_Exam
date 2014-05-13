using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication_Exam.Base;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Common;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects._System;
using Layer02_Objects.Modules_Objects;
using Layer02_Objects.Modules_Base.Abstract;

namespace WebApplication_Exam.Page
{
    public partial class Question_Org : ClsBaseMain_Page
    {
        #region _Variables
        #endregion

        #region _EventHandlers

        protected override void Page_Load(object sender, EventArgs e)
        {
            this.Master.Setup(false, true, Layer02_Constants.eSystem_Modules.None, false, true);
            base.Page_Load(sender, e);
            
            this.QuestionGrid.pGrid.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler(pGrid_ItemCommand);
            this.ListFilter.EvFiltered += new UserControl.Control_Filter.DsFiltered(ListFilter_EvFiltered);

            if (!this.IsPostBack) { this.SetupPage(); }
        }

        void ListFilter_EvFiltered(ClsQueryCondition Qc)
        { 
            this.QuestionGrid.RebindGrid(Qc);
            
        }

        void pGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Int64 Key = 0;

            // Get the ID for the row to select or delete
            if (e.CommandName.ToUpper() == "SELECT" || e.CommandName.ToUpper() == "DELETE")
            {
                Key = this.QuestionGrid.GetKey(e.Item.ItemIndex);
            }

            switch (e.CommandName)
            {
                case "Select":
                    this.Response.Redirect(@"~/Page/Question_Details.aspx?ID=" + Key);
                    break;

                case "Delete":
                    ClsKeys ClsKey = new ClsKeys();
                    ClsKey.Add("RecruitmentTestQuestionsID", Convert.ToInt64(Key));

                    ClsQuestion Obj_Question = new ClsQuestion(this.Master.pCurrentUser);
                    Obj_Question.Load(ClsKey);

                    if (this.Master.pCurrentUser.pUserType != Layer02_Constants.eLookupUserType.Administrator)
                    {
                        Int64 CurrentUserID = Methods.Convert_Int64(this.Master.pCurrentUser.pDrUser["RecruitmentTestUserID"], 0);
                        Int64 OwnerUserID = Methods.Convert_Int64(Obj_Question.pDr["RecruitmentTestUserID_CreatedBy"], 0);

                        if (CurrentUserID != OwnerUserID)
                        {
                            this.Show_EventMsg("You can't delete this question.", ClsBaseMain_Master.eStatus.Event_Error);
                            this.QuestionGrid.RebindGrid();
                            return;
                        }
                    }

                    Obj_Question.Delete();
                    this.QuestionGrid.RebindGrid();
                    break;
            }
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            ClsQuestion Obj_Question = new ClsQuestion(this.pCurrentUser);
            
            //[-]

            List<ClsBindGridColumn_Web_Telerik> List_Gct = new List<ClsBindGridColumn_Web_Telerik>();

            /*
            ClsBindGridColumn_Telerik RedirectColumn = new ClsBindGridColumn_Telerik("", "", 50, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
            RedirectColumn.mCommandName = "Select";
            RedirectColumn.mFieldText = ">>";
            List_Gct.Add(RedirectColumn);
            */
            
            ClsBindGridColumn_Web_Telerik Gc_Select = new ClsBindGridColumn_Web_Telerik("", "", 50, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_HyperLink);
            Gc_Select.mFieldText = ">>";
            Gc_Select.mFieldNavigateUrl_Text = this.ResolveUrl("~/Page/Question_Details.aspx?ID={0}");
            Gc_Select.mFieldNavigateUrl_Field = "RecruitmentTestQuestionsID";
            List_Gct.Add(Gc_Select);

            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("RecruitmentTestQuestionsID", "Question ID", 120));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Question", "Question Description", 400));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Category_Desc", "Category", 200));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("QuestionType_Desc", "Question Type", 200));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Status_Desc", "Status", 120));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("UserName_CreatedBy", "Created By", 120));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("UserName_ApprovedBy", "Approved By", 120));

            ClsBindGridColumn_Web_Telerik DeleteColumn = new ClsBindGridColumn_Web_Telerik("", "", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button);
            DeleteColumn.mCommandName = "Delete";
            DeleteColumn.mFieldText = "Delete";
            List_Gct.Add(DeleteColumn);

            QuestionGrid.pGrid.ClientSettings.ClientEvents.OnCommand = "Grid_OnDeleteCommand";
            QuestionGrid.Setup_WithRequery(this.Master.pCurrentUser, Obj_Question, List_Gct, "RecruitmentTestQuestionsID", true, true);

            //[-]

            this.ListFilter.Setup(this.pCurrentUser, new List<ClsBindGridColumn>(List_Gct), Obj_Question.List_Empty(), this.QuestionGrid.pAjaxPanel);

            //[-]

            this.Panel_Back.Visible = this.Master.pCurrentUser.pUserType == Layer02_Constants.eLookupUserType.Administrator;

        }

        #endregion
    }
}