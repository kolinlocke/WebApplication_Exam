using System;
using System.Collections;
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
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Common;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects;
//using Layer02_Objects.DataAccess;
using Layer02_Objects._System;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using WebApplication_Exam;
using WebApplication_Exam._Base;
using Telerik.Web.UI;

namespace WebApplication_Exam.Page
{
    public partial class Question_Details :  ClsBaseDetails_Page
    {
        #region _Variables

        ClsQuestion mObj;
        bool mSave_IsApprove = false;
     
        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Setup(Layer02_Constants.eSystem_Modules.Question, new ClsQuestion(this.pCurrentUser));
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);
            
            this.RadAjaxPanel_AnswerWindow.AjaxRequest += new Telerik.Web.UI.RadAjaxControl.AjaxRequestDelegate(RadAjaxPanel_AnswerWindow_AjaxRequest);
            this.RadAjaxPanel1.AjaxRequest += new Telerik.Web.UI.RadAjaxControl.AjaxRequestDelegate(RadAjaxPanel1_AjaxRequest);
            this.Btn_Approve.Click += new EventHandler(Btn_Approve_Click);
            
            this.mObj = (ClsQuestion)this.pObj_Base;

            if (!this.IsPostBack)
            {
                this.SetupPage_Rights();
                this.SetupPage();
            }
        }

        void RadAjaxPanel_AnswerWindow_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {            
            Int32 Index = -1;
            Int64 Key = 0;

            if (e.Argument != string.Empty)
            {
                Index = Do_Methods.Convert_Int32(e.Argument);
                try { Key = Do_Methods.Convert_Int64(this.Grid_Answers.pGrid.MasterTableView.Items[Index].GetDataKeyValue("TmpKey").ToString()); }
                catch { }
            }

            bool IsNew = false;

            if (Key != 0)
            {
                ClsQuestion Obj = this.mObj;
                ClsAnswer Obj_Answer = null;
                DataRow Dr_QA = null;
                DataRow[] Arr_Dr = Obj.pDt_QuestionAnswer.Select("TmpKey = " + Key);
                if (Arr_Dr.Length > 0)
                {
                    Dr_QA = Arr_Dr[0];
                    Obj_Answer = Obj.pObj_Answer_Get(Key);

                    this.RadWindow1_Hid_TmpKey.Value = Key.ToString();
                    this.RadWindow1_RadEditor_Answer.Content = Do_Methods.Convert_String(Obj_Answer.pDr["Answer"], "");
                    this.RadWindow1_Chk_IsAnswer.Checked = (bool)Do_Methods.IsNull(Dr_QA["IsAnswer"], false);
                    this.RadWindow1_Chk_IsFixed.Checked = (bool)Do_Methods.IsNull(Dr_QA["IsFixed"], false);
                    this.RadWindow1_Txt_OrderIndex.Text = Do_Methods.Convert_Int32(Dr_QA["OrderIndex"], 0).ToString();
                }
                else
                { IsNew = true; }
            }
            else
            { IsNew = true; }

            if (IsNew)
            {
                this.RadWindow1_Hid_TmpKey.Value = string.Empty;
                this.RadWindow1_Chk_IsAnswer.Checked = false;
                this.RadWindow1_RadEditor_Answer.Content = "";
            }            
        }

        void RadAjaxPanel1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            string[] Tmp;
            string CommandName = string.Empty;
            Int32 ItemIndex = -1;
            try
            {
                Tmp = e.Argument.Split(',');
                CommandName = Tmp[0];

                switch (CommandName)
                {
                    case "Delete":
                        ItemIndex = Do_Methods.Convert_Int32(Tmp[1], -1);
                        break;
                }
            }
            catch { }

            switch (CommandName)
            {
                case "Dialog_Answer":
                    {
                        Int64 Key = Do_Methods.Convert_Int64(this.RadWindow1_Hid_TmpKey.Value);
                        ClsQuestion Obj = this.mObj;
                        ClsAnswer Obj_Answer = null;
                        DataRow Dr_QA = null;

                        bool IsNew = false;
                        if (Key == 0)
                        { IsNew = true; }
                        else
                        {
                            DataRow[] Arr_Dr = Obj.pDt_QuestionAnswer.Select("TmpKey = " + Key);
                            if (Arr_Dr.Length > 0)
                            {
                                Dr_QA = Arr_Dr[0];
                                Obj_Answer = Obj.pObj_Answer_Get(Key);
                            }
                            else
                            { IsNew = true; }
                        }

                        if (IsNew)
                        {
                            Dr_QA = Obj.pBL_QuestionAnswer.Add_Item();
                            Obj_Answer = Obj.pObj_Answer_Get(Do_Methods.Convert_Int64(Dr_QA["TmpKey"]));
                        }

                        Obj_Answer.pDr["Answer"] = this.RadWindow1_RadEditor_Answer.Content;
                        Dr_QA["IsAnswer"] = this.RadWindow1_Chk_IsAnswer.Checked;
                        Dr_QA["IsFixed"] = this.RadWindow1_Chk_IsFixed.Checked;
                        Dr_QA["OrderIndex"] = Do_Methods.Convert_Int32(this.RadWindow1_Txt_OrderIndex.Text);
                        Dr_QA["Lkp_RecruitmentTestAnswersID_Desc"] = Methods_Web.ClearHTMLTags(Do_Methods.Convert_String(Obj_Answer.pDr["Answer"], ""));

                        Obj.FixOrderIndex();

                        break;
                    }
                case "Delete":
                    {
                        Int64 Key = Do_Methods.Convert_Int64(this.Grid_Answers.pGrid.MasterTableView.Items[ItemIndex].GetDataKeyValue("TmpKey").ToString());
                        ClsQuestion Obj = this.mObj;
                        DataRow[] Arr_Dr = Obj.pDt_QuestionAnswer.Select("TmpKey = " + Key);
                        if (Arr_Dr.Length > 0)
                        { Arr_Dr[0].Delete(); }
                        break;
                    }
            }

            this.Grid_Answers.RebindGrid();
        }

        void Btn_Approve_Click(object sender, EventArgs e)
        {
            this.mSave_IsApprove = true;
            this.Begin_Save();
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            DataTable Dt_Category = Do_Methods_Query.GetQuery("LookupCategory", "", "", "[Desc]");
            Methods_Web.BindCombo(ref this.Cbo_Category, Dt_Category, "LookupCategoryID", "Desc");

            DataTable Dt_QuestionType = Do_Methods_Query.GetQuery("LookupQuestionType", "", "", "LookupQuestionTypeID");
            Methods_Web.BindCombo(ref this.Cbo_QuestionType, Dt_QuestionType, "LookupQuestionTypeID", "Desc");

            //[-]

            ClsQuestion Obj_Question = this.mObj;
            this.RadEditor_Question.Content = (string)Do_Methods.IsNull(Obj_Question.pDr["Question"], "");

            try { this.Cbo_Category.SelectedValue = Do_Methods.Convert_Int64(Obj_Question.pDr["LookupCategoryID"], 0).ToString(); }
            catch { }

            try { this.Cbo_QuestionType.SelectedValue = Do_Methods.Convert_Int64(Obj_Question.pDr["LookupQuestionTypeID"], 0).ToString(); }
            catch { }

            //[-]

            this.BindGrid();

            //[-]

            this.Lbl_Title.Text = this.pIsNew ? "New Question" : "Question Details: ID #" + Request.QueryString["ID"];
            this.Page.Title = "Exam | " + this.Lbl_Title.Text;
        }
        
        void SetupPage_Rights()
        {
            /*
            Custom Rights for Question Module
            A Question record can be edited by:
                The user who created the module and has the Edit Rights and the Question Record is not yet approved
                A user who has both the Edit and View Rights and the Question Record is not yet approved
                A user who has the Edit, View, and Approved Rights and the Question Record is approved
            
            A Question Record can be approved by:
                A user that has the Approved Rights and passed on the Editing conditions
            */

            Int64 UserID = Do_Methods.Convert_Int64(this.pCurrentUser.pDrUser["RecruitmentTestUserID"], 0);
            Int64 UserID_CreatedBy = Do_Methods.Convert_Int64(this.mObj.pDr["RecruitmentTestUserID_CreatedBy"], 0);
            bool IsApproved = Convert.ToBoolean(Do_Methods.IsNull(this.mObj.pDr["IsApproved"], false));

            this.pIsReadOnly = false;
            if (!
                    (
                        (this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View) && this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit) && (!IsApproved))
                        || (this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View) && this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit) && this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Approve))
                        || ((UserID == UserID_CreatedBy) && (!IsApproved) && (this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit)))
                        || (this.pIsNew)
                    )
                )
            {
                if (!
                        (
                            (this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit) && (UserID == UserID_CreatedBy))
                            || this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View)
                        )
                    )
                { throw new CustomException(this.pNoAccessMessage); }
                this.pIsReadOnly = true;
            }

            this.Btn_Approve.Visible = this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Approve) && (!this.pIsReadOnly);
        }

        protected override void SetupPage_ControlAttributes()
        {
            base.SetupPage_ControlAttributes();
            
            this.Btn_Approve.Attributes.Add("onclick", "TempReleaseSave();");
            this.Btn_New.Attributes.Add("onclick", "Btn_New_Click(); return false;");
        }

        public override void SetupPage_Redirected(Hashtable Ht)
        {
            bool IsSave = false;
            try { IsSave = (bool)Ht["IsSave"]; }
            catch { }

            bool IsApprove = false;
            try { IsApprove = (bool)Ht["IsApprove"]; }
            catch { }

            if (IsSave) 
            {
                if (!IsApprove) { this.Show_EventMsg("Question has been saved.", ClsBaseMain_Master.eStatus.Event_Info); }
                else { this.Show_EventMsg("Question has been approved.", ClsBaseMain_Master.eStatus.Event_Info); }                
            }
        }

        void BindGrid()
        {
            ClsQuestion Obj = this.mObj;

            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Lkp_RecruitmentTestAnswersID_Desc", "Choices", 200));
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("IsAnswer", "Is Correct Answer?", 80, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Checkbox, true, false));
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("IsFixed", "Is Fixed?", 80, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Checkbox, true, false));
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("OrderIndex", "Order Index", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Static, true, false));

            if (!this.pIsReadOnly)
            {
                ClsBindGridColumn_Web_Telerik Gc_Button = new ClsBindGridColumn_Web_Telerik("", "Edit", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button);
                Gc_Button.mCommandName = "Edit";
                Gc_Button.mFieldText = "Edit";
                Gc_Button.mColumnName = "Edit";
                Gc_Button.mButtonType = ButtonColumnType.LinkButton;
                List_Gc.Insert(0, Gc_Button);

                Gc_Button = new ClsBindGridColumn_Web_Telerik("", "Delete", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button);
                Gc_Button.mCommandName = "Delete";
                Gc_Button.mFieldText = "Delete";
                Gc_Button.mColumnName = "Delete";
                Gc_Button.mButtonType = ButtonColumnType.LinkButton;
                List_Gc.Add(Gc_Button);
            }

            this.Grid_Answers.pGrid.ClientSettings.ClientEvents.OnCommand = "Grid_Command";
            this.Grid_Answers.Setup_FromDataTable(this.pCurrentUser, Obj.pDt_QuestionAnswer, List_Gc, "TmpKey");
        }

        //[-]

        protected override void Save()
        { this.Save(this.mSave_IsApprove); }

        void Save(bool IsApprove = false)
        {
            StringBuilder Sb_ErrorMsg = new StringBuilder();
            if (!this.Save_Validate(Sb_ErrorMsg, IsApprove))
            {
                this.Show_EventMsg(Sb_ErrorMsg.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
                return;
            }

            //[-]

            ClsQuestion Obj_Question = this.mObj;
            Obj_Question.pDr["Question"] = this.RadEditor_Question.Content;
            Obj_Question.pDr["LookupCategoryID"] = Do_Methods.Convert_Int64(this.Cbo_Category.SelectedValue, 0);
            Obj_Question.pDr["LookupQuestionTypeID"] = Do_Methods.Convert_Int64(this.Cbo_QuestionType.SelectedValue, 0);
            Obj_Question.FixOrderIndex();
            Obj_Question.Save(IsApprove);
        }

        protected override Hashtable Save_ReloadPage_Prepare()
        {
            Hashtable Ht = base.Save_ReloadPage_Prepare();
            Ht.Add("IsApprove", this.mSave_IsApprove);
            return Ht;
        }

        bool Save_Validate(System.Text.StringBuilder Sb_Msg, bool IsApprove = false)
        {
            if (this.pIsReadOnly)
            {
                Sb_Msg.Append("Question is read only." + "<br />");
                return false; 
            }

            //[-]

            WebControl Wc;
            bool IsValid = true;

            Wc = this.RadEditor_Question;
            if (Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (this.RadEditor_Question.Content != "")
                , "Question is required." + "<br />"))
            {
                QueryCondition Qc = Do_Methods.CreateQueryCondition();
                Qc.Add("Question", "= " + this.RadEditor_Question.Content, typeof(string).ToString());
                Qc.Add("IsDeleted", "0", typeof(bool).ToString(), "0");
                Qc.Add("RecruitmentTestQuestionsID", "<> " + this.mObj.pID.ToString(), typeof(Int64).ToString(), "0");

                DataTable Dt = Do_Methods_Query.GetQuery("RecruitmentTestQuestions", "", Qc);
                bool IsValid_Question = !(Dt.Rows.Count > 0);

                Wc = this.RadEditor_Question;
                Methods_Web.ControlValidation(
                    ref Sb_Msg
                    , ref Wc
                    , ref IsValid
                    , Layer01_Constants_Web.CnsCssTextbox
                    , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                    , IsValid_Question
                    , "Question must be unique." + "<br />");
            }

            if (this.Cbo_QuestionType.SelectedValue == ((Int32)Layer02_Constants.eLookupQuestionType.Single_Answer).ToString())
            {
                DataRow[] ArrDr = (this.mObj as ClsQuestion).pDt_QuestionAnswer.Select("IsAnswer = 1");
                bool IsValid_Answers = !(ArrDr.Length > 1);
                if (!IsValid_Answers)
                {
                    IsValid = false;
                    Sb_Msg.Append("There should only be one correct answer.");
                }
            }

            if (IsApprove)
            {
                Int64 NoRequiredAnswers = Do_Methods.Convert_Int64(Do_Methods_Query.GetSystemParameter(Configuration.CnsExam_NoRequiredAnswers));
                if ((this.mObj as ClsQuestion).pDt_QuestionAnswer.Rows.Count < NoRequiredAnswers)
                {
                    IsValid = false;
                    Sb_Msg.Append("Answer choices must be " + NoRequiredAnswers.ToString() + " or more.");
                }
            }

            return IsValid;
        }

        #endregion
    }
}