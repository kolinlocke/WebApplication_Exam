using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Layer02_Objects.DataAccess;
using Layer02_Objects._System;
using Layer02_Objects.Modules_Base;
using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Telerik.Web.UI;

using System.Data;

namespace WebApplication_Exam.Page
{
    public partial class Question_DetailsEx2 : ClsBaseMain_Page
    {
        #region _Variables

        const string CnsSession_Obj = "CnsSession_Obj";
        const string CnsObjID = "CnsObjID";

        ClsBase mObj_Base;
        protected string mObjID = "";

        bool mIsReadOnly = false;
        bool mIsNew = false;

        #endregion

        #region _EventHandlers

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            try
            {
                this.RadAjaxPanel_AnswerWindow.AjaxRequest += new Telerik.Web.UI.RadAjaxControl.AjaxRequestDelegate(RadAjaxPanel_AnswerWindow_AjaxRequest);
                this.RadAjaxPanel1.AjaxRequest += new Telerik.Web.UI.RadAjaxControl.AjaxRequestDelegate(RadAjaxPanel1_AjaxRequest);
                this.RadWindow1_RadComboBox1.ItemsRequested += new Telerik.Web.UI.RadComboBoxItemsRequestedEventHandler(RadComboBox1_ItemsRequested);

                this.Btn_Save.Click += new EventHandler(Btn_Save_Click);
                this.Btn_Approve.Click += new EventHandler(Btn_Approve_Click);

                if (!this.IsPostBack)
                {
                    this.SetupPage();
                    this.SetupPage_Redirected();
                }
                else
                {
                    this.mObjID = (string)this.ViewState[CnsObjID];
                    this.mObj_Base = (ClsBase)this.Session[this.mObjID];
                }
            }
            catch
            { }
        }

        protected void RadAjaxPanel_AnswerWindow_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            Int32 Index = -1;
            Int64 Key = 0;

            if (e.Argument != string.Empty)
            {
                Index = Layer01_Common.Common.Methods.Convert_Int32(e.Argument);
                try
                {                    
                    Key = Layer01_Common.Common.Methods.Convert_Int64(this.RadGrid1.MasterTableView.Items[Index].GetDataKeyValue("TmpKey").ToString()); }
                catch { }
            }


            bool IsNew = false;

            if (Key != 0)
            {
                Layer02_Objects.Modules_Objects.ClsQuestion Obj = (Layer02_Objects.Modules_Objects.ClsQuestion)this.mObj_Base;
                Layer02_Objects.Modules_Objects.ClsAnswer Obj_Answer = null;
                System.Data.DataRow Dr_QA = null;

                System.Data.DataRow[] Arr_Dr = Obj.pDt_QuestionAnswer.Select("TmpKey = " + Key);

                if (Arr_Dr.Length > 0)
                {
                    Dr_QA = Arr_Dr[0];
                    Obj_Answer = (Layer02_Objects.Modules_Objects.ClsAnswer)Obj.pBO_Answer[Key.ToString()];

                    this.RadWindow1_Hid_TmpKey.Value = Key.ToString();

                    Telerik.Web.UI.RadComboBoxItem Ci =
                        new Telerik.Web.UI.RadComboBoxItem(
                            (string)Layer01_Common.Common.Methods.IsNull(Obj_Answer.pDr["Answer"], "")
                            , Convert.ToInt64(Layer01_Common.Common.Methods.IsNull(Obj_Answer.pDr["RecruitmentTestAnswersID"], 0)).ToString());

                    this.RadWindow1_RadComboBox1.Items.Clear();
                    this.RadWindow1_RadComboBox1.Items.Add(Ci);
                    this.RadWindow1_RadComboBox1.SelectedIndex = 0;
                    this.RadWindow1_RadComboBox1.Text = Ci.Text;

                    this.RadWindow1_Chk_IsAnswer.Checked = (bool)Layer01_Common.Common.Methods.IsNull(Dr_QA["IsAnswer"], false);
                }
                else
                { IsNew = true; }
            }
            else
            { IsNew = true; }

            if (IsNew)
            {
                this.RadWindow1_Hid_TmpKey.Value = string.Empty;
                this.RadWindow1_RadComboBox1.Items.Clear();
                this.RadWindow1_RadComboBox1.Text = string.Empty;
                this.RadWindow1_Chk_IsAnswer.Checked = false;
            }
        }

        protected void RadComboBox1_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            Layer02_Objects.DataAccess.Interface_DataAccess Da = this.mObj_Base.pDa;
            Layer01_Common.Objects.ClsQueryCondition Qc = Da.CreateQueryCondition();
            Qc.Add("Answer", @"Like '" + e.Text + "%'", typeof(string).ToString());
            //Qc.Add("Answer", @"Begins '" + e.Text + "'", typeof(string).ToString());

            System.Data.DataTable Dt = Da.GetQuery("RecruitmentTestAnswers", "", Qc);
            Dt.DefaultView.Sort = "Answer";

            this.RadWindow1_RadComboBox1.DataSource = Dt;
            this.RadWindow1_RadComboBox1.DataTextField = "Answer";
            this.RadWindow1_RadComboBox1.DataValueField = "RecruitmentTestAnswersID";
            this.RadWindow1_RadComboBox1.DataBind();
        }

        protected void RadAjaxPanel1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            string[] Tmp;
            string CommandName = string.Empty;
            Int32 ItemIndex = -1;
            string ComboText = "";
            try
            {
                Tmp = e.Argument.Split(',');
                CommandName = Tmp[0];

                switch (CommandName)
                { 
                    case "Dialog_Answer":
                        ComboText = Tmp[1];
                        break;
                    case "Delete":
                        ItemIndex = Methods.Convert_Int32(Tmp[1], -1);
                        break;
                }
            }
            catch { }

            switch (CommandName)
            {
                case "Dialog_Answer":
                    {
                        Int64 Key = Methods.Convert_Int64(this.RadWindow1_Hid_TmpKey.Value);
                        Layer02_Objects.Modules_Objects.ClsQuestion Obj = (Layer02_Objects.Modules_Objects.ClsQuestion)this.mObj_Base;
                        Layer02_Objects.Modules_Objects.ClsAnswer Obj_Answer = null;
                        System.Data.DataRow Dr_QA = null;

                        bool IsNew = false;
                        if (Key == 0)
                        { IsNew = true; }
                        else
                        {
                            System.Data.DataRow[] Arr_Dr = Obj.pDt_QuestionAnswer.Select("TmpKey = " + Key);
                            if (Arr_Dr.Length > 0)
                            {
                                Dr_QA = Arr_Dr[0];
                                Obj_Answer = (Layer02_Objects.Modules_Objects.ClsAnswer)Obj.pBO_Answer[Key.ToString()];
                            }
                            else
                            { IsNew = true; }
                        }

                        if (IsNew)
                        {
                            Dr_QA = Obj.pDt_QuestionAnswer.NewRow();
                            Dr_QA["TmpKey"] = Layer02_Objects.Modules_Base.Abstract.ClsBase.GetNewTmpKey(Obj.pDt_QuestionAnswer);
                            Obj.pDt_QuestionAnswer.Rows.Add(Dr_QA);
                        }

                        bool IsNewAnswer = false;
                        if (this.RadWindow1_RadComboBox1.SelectedValue == "")
                        { IsNewAnswer = true; }

                        if (IsNewAnswer || IsNew)
                        {
                            Obj_Answer = new Layer02_Objects.Modules_Objects.ClsAnswer();
                            if (IsNewAnswer)
                            {
                                Obj_Answer.Load();
                                //Obj_Answer.pDr["Answer"] = this.RadWindow1_RadComboBox1.Text;
                                Obj_Answer.pDr["Answer"] = ComboText;
                            }

                            Obj.pBO_Answer.Add(Dr_QA["TmpKey"].ToString(), Obj_Answer);
                        }

                        if (!IsNewAnswer)
                        {
                            Layer02_Objects._System.ClsKeys Inner_Key = null;
                            Inner_Key = new Layer02_Objects._System.ClsKeys();
                            Inner_Key.Add("RecruitmentTestAnswersID", Layer01_Common.Common.Methods.Convert_Int64(this.RadWindow1_RadComboBox1.SelectedValue));
                            Obj_Answer.Load(Inner_Key);
                        }

                        Dr_QA["IsAnswer"] = this.RadWindow1_Chk_IsAnswer.Checked;
                        Dr_QA["Lkp_RecruitmentTestAnswersID_Desc"] = (string)Layer01_Common.Common.Methods.IsNull(Obj_Answer.pDr["Answer"], "");

                        break;
                    }
                case "Delete":
                    {
                        Int64 Key = Methods.Convert_Int64(this.RadGrid1.MasterTableView.Items[ItemIndex].GetDataKeyValue("TmpKey").ToString());
                        ClsQuestion Obj = (ClsQuestion)this.mObj_Base;
                        DataRow[] Arr_Dr = Obj.pDt_QuestionAnswer.Select("TmpKey = " + Key);
                        if (Arr_Dr.Length > 0)
                        { Arr_Dr[0].Delete(); }
                        break;
                    }                    
            }

            this.BindGrid();
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        void Btn_Approve_Click(object sender, EventArgs e)
        {
            this.Save(true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Int64 Key = Convert.ToInt64(Request.QueryString["ID"].ToString());
            ClsKeys ClsKey = new ClsKeys();
            ClsKey.Add("RecruitmentTestQuestionsID", Key);

            ClsQuestion objQuestion = new ClsQuestion(this.Master.pCurrentUser);
            objQuestion.Load(ClsKey);
            objQuestion.Delete();

            Response.Redirect("Question.aspx");
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            ClsSysCurrentUser CurrentUser = this.Master.pCurrentUser;
            Int64 ID = Methods.Convert_Int64(this.Request.QueryString["ID"]);
            ClsKeys Key = null;

            if (ID != 0)
            {
                Key = new Layer02_Objects._System.ClsKeys();
                Key.Add("RecruitmentTestQuestionsID", ID);
            }
            else
            { this.mIsNew = true; }

            this.mObj_Base = new ClsQuestion(this.Master.pCurrentUser);
            this.mObj_Base.Load(Key);

            this.mObjID = CurrentUser.GetNewPageObjectID();
            this.ViewState[CnsObjID] = this.mObjID;
            this.Session[this.mObjID] = this.mObj_Base;

            //[-]
            
            Int64 UserID = Methods.Convert_Int64(CurrentUser.pDrUser["RecruitmentTestUserID"], 0);
            Int64 UserID_CreatedBy = Methods.Convert_Int64(this.mObj_Base.pDr["RecruitmentTestUserID_CreatedBy"], 0);
            bool IsApproved = Convert.ToBoolean(Methods.IsNull(this.mObj_Base.pDr["IsApproved"], false));

            if (!
                    (
                        (CurrentUser.pIsAdmin || ((UserID == UserID_CreatedBy) && (!IsApproved)))
                        || (this.mIsNew)
                    )
                )
            { this.mIsReadOnly = true; }

            //[-]
            
            if ((bool)Methods.IsNull(CurrentUser.pDrUser["IsAdministrator"], false))
            {
                this.Btn_Approve.Enabled = true;
                //this.Btn_Approve2.Enabled = true;
            }

            //[-]

            ClsQuestion Obj_Question = (ClsQuestion)this.mObj_Base;

            this.Txt_Question.Text = (string)Methods.IsNull(Obj_Question.pDr["Question"], "");
            this.Chk_IsMultipleAnswer.Checked = Convert.ToBoolean(Methods.IsNull(Obj_Question.pDr["IsMultipleAnswer"], false));

            //[-]

            this.SetupPage_ControlAttributes();
            this.BindGrid();
        }

        void SetupPage_Redirected()
        {
            System.Collections.Hashtable Pc = null;
            if (!(this.Session[Layer01_Common_Web.Common.Constants_Web.CnsSession_TmpObj] is System.Collections.Hashtable))
            { return; }

            try
            { Pc = (System.Collections.Hashtable)this.Session[Layer01_Common_Web.Common.Constants_Web.CnsSession_TmpObj]; }
            catch
            { return; }

            if (Pc != null)
            {
                this.Session.Remove(Constants_Web.CnsSession_TmpObj);
                bool IsSave = false;
                bool IsApprove = false;

                try { IsSave = (bool)Pc["IsSave"]; }
                catch { }

                try { IsApprove = (bool)Pc["IsApprove"]; }
                catch { }

                if (IsSave)
                {
                    if (!IsApprove)
                    { this.Show_EventMsg("Question has been saved."); }
                    else
                    { this.Show_EventMsg("Question has been approved."); }
                }
            }
        }
        
        void SetupPage_ControlAttributes(ref Control C)
        {
            WebControl Wc = null;
            if (C is WebControl)
            { Wc = (WebControl)C; }
            else
            {
                foreach (Control Ic in C.Controls)
                {
                    Control Inner_Ic = Ic;
                    this.SetupPage_ControlAttributes(ref Inner_Ic);
                }
                return;
            }

            if (Wc.GetType().ToString() == typeof(System.Web.UI.WebControls.TextBox).ToString())
            {
                if ((Wc as TextBox).TextMode != TextBoxMode.MultiLine)
                { Wc.Attributes.Add("onkeypress", "return noenter(event)"); }

                if (!(Wc as TextBox).ReadOnly)
                { (Wc as TextBox).ReadOnly = this.mIsReadOnly; }
            }
            else if (
                Wc.GetType().ToString() == typeof(System.Web.UI.WebControls.CheckBox).ToString()
                || Wc.GetType().ToString() == typeof(System.Web.UI.WebControls.DropDownList).ToString()
                || Wc.GetType().ToString() == typeof(System.Web.UI.WebControls.RadioButton).ToString())
            {
                if (Wc.Attributes["onchange"] == "")
                { 
                    //Wc.Attributes.Add("onchange", "RequireSave()"); 
                }
                Wc.Enabled = !this.mIsReadOnly;
            }
            else if (
                Wc.GetType().ToString() == typeof(System.Web.UI.WebControls.LinkButton).ToString()
                || Wc.GetType().ToString() == typeof(System.Web.UI.WebControls.Button).ToString())
            {
                if (Wc.Enabled)
                { Wc.Enabled = !this.mIsReadOnly; }
            }
            else
            {
                foreach (Control Ic in C.Controls)
                {
                    Control Inner_Ic = Ic;
                    this.SetupPage_ControlAttributes(ref Inner_Ic);
                }
            }
        }

        void SetupPage_ControlAttributes()
        {
            Control Obj = this.Panel_Details;
            this.SetupPage_ControlAttributes(ref Obj);

            if (!this.mIsReadOnly)
            { this.Btn_New.Attributes.Add("onclick", "Btn_New_Click(); return false;"); }

            if (this.mIsReadOnly)
            {
                this.Btn_Save.Enabled = false;
                this.Btn_Approve.Enabled = false;
                this.Btn_Delete.Enabled = false;
            }
        }

        void Show_EventMsg(string Msg)
        {
            this.Lbl_EventMsg.Text = Msg;
            this.Panel1.Visible = true;
        }

        void BindGrid()
        {
            ClsQuestion Obj = (ClsQuestion)this.mObj_Base;

            List<ClsBindGridColumn_Telerik> List_Gc = new List<ClsBindGridColumn_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Telerik("Lkp_RecruitmentTestAnswersID_Desc", "Answer", 200));
            List_Gc.Add(new ClsBindGridColumn_Telerik("IsAnswer", "Is Correct Answer?", 80, "", Constants.eSystem_Lookup_FieldType.FieldType_Checkbox, true, false));

            if (!this.mIsReadOnly)
            {
                ClsBindGridColumn_Telerik Gc_Button = new ClsBindGridColumn_Telerik("", "Edit", 100, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
                Gc_Button.mCommandName = "Edit";
                Gc_Button.mFieldText = "Edit";
                Gc_Button.mColumnName = "Edit";
                Gc_Button.mButtonType = ButtonColumnType.LinkButton;
                List_Gc.Add(Gc_Button);

                Gc_Button = new ClsBindGridColumn_Telerik("", "Delete", 100, "", Constants.eSystem_Lookup_FieldType.FieldType_Button);
                Gc_Button.mCommandName = "Delete";
                Gc_Button.mFieldText = "Delete";
                Gc_Button.mColumnName = "Delete";
                Gc_Button.mButtonType = ButtonColumnType.LinkButton;
                List_Gc.Add(Gc_Button);
            }

            this.RadGrid1.ClientSettings.ClientEvents.OnCommand = "Grid_Command";            
            Methods_Web_Telerik.BindTelerikGrid(ref this.RadGrid1, Obj.pDt_QuestionAnswer, List_Gc, "TmpKey");
        }

        void Save(bool IsApprove = false)
        {
            StringBuilder Sb_ErrorMsg = new StringBuilder();
            if (!this.Validation(ref Sb_ErrorMsg, IsApprove))
            {
                this.Show_EventMsg(Sb_ErrorMsg.ToString());
                return;
            }

            //[-]

            ClsQuestion Obj_Question = (ClsQuestion)this.mObj_Base;
            Obj_Question.pDr["Question"] = this.Txt_Question.Text;
            Obj_Question.pDr["IsMultipleAnswer"] = this.Chk_IsMultipleAnswer.Checked;
            Obj_Question.Save(IsApprove);

            //[-]

            this.Session.Remove(this.mObjID);

            System.Collections.Hashtable Pc = new System.Collections.Hashtable();
            Pc.Add("IsSave", true);
            if (IsApprove)
            { Pc.Add("IsApprove", true); }

            this.Session[Layer01_Common_Web.Common.Constants_Web.CnsSession_TmpObj] = Pc;

            //[-]

            string Url = "~" + this.Request.Url.LocalPath + "?ID=" + this.mObj_Base.pID;
            this.Response.Redirect(Url);
        }

        bool Validation(ref System.Text.StringBuilder Sb_Msg, bool IsApprove = false)
        {
            WebControl Wc;
            bool IsValid = true;

            Interface_DataAccess Da = this.mObj_Base.pDa;

            if (this.Txt_Question.Text != "")
            {
                ClsQueryCondition Qc = this.mObj_Base.pDa.CreateQueryCondition();
                Qc.Add("Question", this.Txt_Question.Text, typeof(string).ToString());
                Qc.Add("IsDeleted", "0", typeof(bool).ToString(), "0");
                Qc.Add("RecruitmentTestQuestionsID", "<> " + this.mObj_Base.pID.ToString(), typeof(Int64).ToString(), "0");

                DataTable Dt = Da.GetQuery("RecruitmentTestQuestions", "", Qc);
                bool IsValid_Question = !(Dt.Rows.Count > 0);

                Wc = this.Txt_Question;
                Methods_Web.ControlValidation(
                    ref Sb_Msg
                    , ref Wc
                    , ref IsValid
                    , Constants_Web.CnsCssTextbox
                    , Constants_Web.CnsCssTextbox_ValidateHighlight
                    , IsValid_Question
                    , "Question must be unique." + "<br />");
            }
            else
            {
                IsValid = false;
                Sb_Msg.Append("Question is required.");
            }

            if (!this.Chk_IsMultipleAnswer.Checked)
            {                
                DataRow[] ArrDr = (this.mObj_Base as ClsQuestion).pDt_QuestionAnswer.Select("IsAnswer = 1");
                bool IsValid_Answers = !(ArrDr.Length > 1);
                if (!IsValid_Answers)
                {
                    IsValid = false;
                    Sb_Msg.Append("There should only be one correct answer.");
                }
            }

            if (IsApprove)
            {
                Int64 NoRequiredAnswers = Methods.Convert_Int64(Da.GetSystemParameter(Configuration.CnsExam_NoRequiredAnswers));
                if ((this.mObj_Base as ClsQuestion).pDt_QuestionAnswer.Rows.Count < NoRequiredAnswers)
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