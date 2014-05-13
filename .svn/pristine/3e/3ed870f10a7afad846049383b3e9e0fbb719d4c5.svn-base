using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.Objects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Layer01_Common;
using Layer01_Common.Objects;
//using Layer01_Common.Connection;
using Layer01_Common.Common;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer02_Objects;
using Layer02_Objects._System;
//using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using Layer02_Objects.Modules_Objects.Exam;

namespace WebApplication_Exam.Page
{
    public partial class Exam : ClsBaseMain_Page
    {
        #region _Variables

        const string CnsCurrentPage = "CnsCurrentPage";
        Int64 mCurrentPage;

        ClsExam mObj_Exam;
        List<ClsExam_Questions> mList_Questions;

        bool mIsSession = false;
        bool mIsReadOnly = false;

        Int64 mExamID = 0;
        Int64 mLimit = 10;

        const string CnsPage = "Exam";
        const string CnsObjID = "CnsObjID";
        string mObjID = "";

        public string mTitle = "";

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            try { this.mExamID = Do_Methods.Convert_Int64(this.Request.QueryString["ID"]); }
            catch { }

            try { this.mLimit = Do_Methods.Convert_Int64(this.Request.QueryString["Limit"], 10); }
            catch { }

            this.mIsReadOnly = this.mExamID != 0;

            if (!this.mIsReadOnly)
            { this.Master.Setup(false, false, Layer02_Constants.eSystem_Modules.None, false, false, false); }
            else
            { this.Master.Setup(false, true, Layer02_Constants.eSystem_Modules.ExamReport); }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            //[-]

            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);

            this.Btn_First.Click += new EventHandler(Btn_First_Click);
            this.Btn_Previous.Click += new EventHandler(Btn_Previous_Click);
            this.Btn_Next.Click += new EventHandler(Btn_Next_Click);
            this.Btn_Last.Click += new EventHandler(Btn_Last_Click);
            this.Cbo_Page.SelectedIndexChanged += new EventHandler(Cbo_Page_SelectedIndexChanged);
            this.Btn_Submit.Click += new EventHandler(Btn_Submit_Click);
            this.Btn_BackToExamReport.Click += new EventHandler(Btn_BackToExamReport_Click);
            this.Btn_Export.Click += new EventHandler(Btn_Export_Click);

            //[-]

            // Check if session exists, if not warn the user or redirect page to ExamStart.aspx
            try { mIsSession = (bool)this.Session[Layer01_Constants_Web.CnsSession_Exam_IsSession]; }
            catch { }

            if (!this.mIsReadOnly)
            {
                if (!mIsSession)
                {
                    //Exam is not in progress
                    this.Response.Redirect("ExamStart.aspx");
                }
                this.mObj_Exam = (ClsExam)this.Session[Layer01_Constants_Web.CnsSession_Exam_Obj];
            }

            try { this.mCurrentPage = Do_Methods.Convert_Int64(this.ViewState[CnsCurrentPage].ToString()); }
            catch { this.mCurrentPage = 1; }

            //[-]

            if (!this.IsPostBack)
            { this.SetupPage(); }
            else
            {
                if (this.mIsReadOnly)
                {
                    this.mObjID = (string)this.ViewState[CnsObjID];
                    this.mObj_Exam = (ClsExam)this.Session[CnsPage + this.mObjID];
                }

                this.mList_Questions = this.SetupPage_Questions();
            }
        }

        void Cbo_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PostAnswers();
            this.mCurrentPage = Do_Methods.Convert_Int64(this.Cbo_Page.SelectedValue);
            this.ViewState[CnsCurrentPage] = this.mCurrentPage;
            this.SetupPage();
        }

        void Btn_First_Click(object sender, EventArgs e)
        {
            this.PostAnswers();
            this.mCurrentPage = 1;
            this.ViewState[CnsCurrentPage] = this.mCurrentPage;
            this.SetupPage();
        }

        void Btn_Previous_Click(object sender, EventArgs e)
        {
            this.PostAnswers();
            this.mCurrentPage--;
            if (this.mCurrentPage <= 1)
            { this.mCurrentPage = 1; }
            this.ViewState[CnsCurrentPage] = this.mCurrentPage;
            this.SetupPage();
        }

        void Btn_Next_Click(object sender, EventArgs e)
        {
            this.PostAnswers();
            this.mCurrentPage++;

            if (this.mCurrentPage >= this.mObj_Exam.pPages)
            { this.mCurrentPage = this.mObj_Exam.pPages; }

            this.ViewState[CnsCurrentPage] = this.mCurrentPage;
            this.SetupPage();
        }

        void Btn_Last_Click(object sender, EventArgs e)
        {
            this.PostAnswers();
            this.mCurrentPage = this.mObj_Exam.pPages;
            this.ViewState[CnsCurrentPage] = this.mCurrentPage;
            this.SetupPage();
        }

        void Btn_Submit_Click(object sender, EventArgs e)
        {
            this.PostAnswers();
            this.PostExam();
        }

        void Btn_BackToExamReport_Click(object sender, EventArgs e)
        { this.Response.Redirect(@"~/Page/ExamReport.aspx"); }

        void Btn_Export_Click(object sender, EventArgs e)
        { this.ExportReport(); }

        #endregion

        #region _Methods

        void SetupPage()
        {
            if (this.mIsReadOnly)
            {
                if (!this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View))
                { throw new ClsCustomException("Access Denied."); }
                
                this.mObj_Exam = new ClsExam();
                this.mObj_Exam.LoadExam(this.mExamID, this.mLimit);

                this.mObjID = this.Master.pCurrentUser.GetNewPageObjectID();
                this.ViewState[CnsObjID] = this.mObjID;
                this.Session[CnsPage + this.mObjID] = this.mObj_Exam;

                //[-]

                this.Panel_Title.Visible = true;
                this.Lbl_Applicant.Text = Do_Methods.Convert_String(this.mObj_Exam.pObj_Applicant.pDr["Name"], "");
                DateTime? DateTaken = Do_Methods.Convert_DateTime(this.mObj_Exam.pDr_Exam["DateTaken"]);
                this.Lbl_DateTaken.Text = DateTaken != null ? DateTaken.Value.ToShortDateString() : "";

                this.mTitle = "Exam Report of " + this.Lbl_Applicant.Text;
            }
            else
            { this.mTitle = "Applicant Exam"; }

            //[-]

            this.SetupPage_Questions();

            //[-]

            this.Lbl_Page.Text = "Page " + this.mCurrentPage + " of " + this.mObj_Exam.pPages;

            this.Btn_First.Enabled = true;
            this.Btn_Previous.Enabled = true;
            this.Btn_Next.Enabled = true;
            this.Btn_Last.Enabled = true;
            this.Btn_Submit.Visible = false;
            this.Panel_Report.Visible = false;
            
            if (this.mCurrentPage <= 1)
            {
                this.Btn_First.Enabled = false;
                this.Btn_Previous.Enabled = false;
            }

            if (this.mCurrentPage >= this.mObj_Exam.pPages)
            { 
                this.Btn_Next.Enabled = false;
                this.Btn_Last.Enabled = false;

                this.Btn_Submit.Visible = !this.mIsReadOnly;
            }

            this.Panel_Report.Visible = this.mIsReadOnly;
            
            DataTable Dt_Page = new DataTable();
            
            Dt_Page.Columns.Add("Page", typeof(Int64));
            
            for (Int64 Ct = 1; Ct <= this.mObj_Exam.pPages; Ct++)
            {
                List<Do_Constants.Str_Parameters> List_Sp = new List<Do_Constants.Str_Parameters>();
                List_Sp.Add(new Do_Constants.Str_Parameters("Page", Ct));
                Do_Methods.AddDataRow(ref Dt_Page, List_Sp);
            }

            Methods_Web.BindCombo(ref this.Cbo_Page, Dt_Page, "Page", "Page");

            try { this.Cbo_Page.SelectedValue = this.mCurrentPage.ToString(); }
            catch { }
        }

        List<ClsExam_Questions> SetupPage_Questions()
        {
            this.Panel_Questions.Controls.Clear();

            List<ClsExam_Questions> List_Questions = this.mObj_Exam.Get_Questions(this.mCurrentPage);
            Int64 Ct = 0;

            foreach (ClsExam_Questions Q in List_Questions)
            {
                Ct++;

                string CssQuestion = "";
                if (Q.pIsCorrect)
                { CssQuestion = "ClsQuestionCorrect"; }
                else
                { CssQuestion = "ClsQuestion"; }

                Label Lbl_Question = new Label();
                Lbl_Question.ID = @"Lbl_Question_" + Q.pCt.ToString();
                Lbl_Question.Text = Q.pCt + @". " + Q.pQuestion;
                
                //[-]

                Table Tb_Question = new Table();
                Tb_Question.BorderStyle = BorderStyle.None;
                Tb_Question.CellPadding = 0;

                TableCell Tbc_Question = new TableCell();
                Tbc_Question.VerticalAlign = VerticalAlign.Top;
                Tbc_Question.CssClass = CssQuestion;
                Tbc_Question.Controls.Add(Lbl_Question);

                TableRow Tbr_Question = new TableRow();
                Tbr_Question.Cells.Add(Tbc_Question);

                Tb_Question.Rows.Add(Tbr_Question);

                this.Panel_Questions.Controls.Add(Tb_Question);

                //[-]

                Table Tb_Answer = new Table();
                Tb_Answer.BorderStyle = BorderStyle.None;
                Tb_Answer.CellPadding = 1;
                
                WebControl Inner_Wc = null;

                foreach (ClsExam_Questions_Answers Qa in Q.pList_Answers)
                {
                    switch (Q.pQuestionType)
                    {
                        case Layer02_Constants.eLookupQuestionType.Single_Answer:
                            {
                                RadioButton Rdo_Answer = new RadioButton();
                                Inner_Wc = Rdo_Answer;
                                Rdo_Answer.ID = "Rdo_Answer_" + Qa.pCt.ToString();
                                Rdo_Answer.CssClass = "";
                                Rdo_Answer.InputAttributes.CssStyle.Add("margin", "0px");
                                Rdo_Answer.Checked = Qa.pIsAnswered;
                                Rdo_Answer.GroupName = Q.pCt.ToString();
                                Rdo_Answer.Enabled = !this.mIsReadOnly;
                                break;
                            }
                        case Layer02_Constants.eLookupQuestionType.Multiple_Answer:
                            {
                                CheckBox Chk_Answer = new CheckBox();
                                Inner_Wc = Chk_Answer;
                                Chk_Answer.ID = "Chk_Answer_" + Qa.pCt.ToString();
                                Chk_Answer.CssClass = "";
                                Chk_Answer.InputAttributes.CssStyle.Add("margin", "0px");
                                Chk_Answer.Checked = Qa.pIsAnswered;
                                Chk_Answer.Enabled = !this.mIsReadOnly;
                                break;
                            }
                    }

                    const string CnsCssAnswerCorrect = "ClsAnswerCorrect";

                    Label Inner_Lbl = new Label();
                    Inner_Lbl.Text = Qa.pAnswer;

                    TableCell Tbc_Rdo = new TableCell();
                    Tbc_Rdo.VerticalAlign = VerticalAlign.Top;
                    Tbc_Rdo.Width = 20;
                    Tbc_Rdo.Controls.Add(Inner_Wc);

                    TableCell Tbc_Lbl = new TableCell();
                    Tbc_Lbl.VerticalAlign = VerticalAlign.Top;
                    Tbc_Lbl.Controls.Add(Inner_Lbl);

                    if (!Q.pIsCorrect && this.mIsReadOnly && Qa.pIsAnswer)
                    {
                        Tbc_Rdo.CssClass = CnsCssAnswerCorrect;
                        Tbc_Lbl.CssClass = CnsCssAnswerCorrect;
                    }

                    TableRow Tbr = new TableRow();
                    Tbr.Cells.Add(Tbc_Rdo);
                    Tbr.Cells.Add(Tbc_Lbl);

                    Tb_Answer.Rows.Add(Tbr);
                }

                this.Panel_Questions.Controls.Add(Tb_Answer);
                this.Panel_Questions.Controls.Add(new Literal() { Text = "<hr />" });
            }

            return List_Questions;
        }

        void PostAnswers()
        {
            List<ClsExam_Questions> List_Questions = this.mList_Questions;
            foreach (ClsExam_Questions Q in List_Questions)
            {
                foreach (ClsExam_Questions_Answers Qa in Q.pList_Answers)
                {
                    try
                    {
                        switch (Q.pQuestionType)
                        {
                            case Layer02_Constants.eLookupQuestionType.Single_Answer:
                                {
                                    RadioButton Rdo_Answer = (RadioButton)this.Panel_Questions.FindControl("Rdo_Answer_" + Qa.pCt);
                                    Qa.pIsAnswered = Rdo_Answer.Checked;
                                    break;
                                }
                            case Layer02_Constants.eLookupQuestionType.Multiple_Answer:
                                {
                                    CheckBox Chk_Answer = (CheckBox)this.Panel_Questions.FindControl("Chk_Answer_" + Qa.pCt);
                                    Qa.pIsAnswered = Chk_Answer.Checked;
                                    break;
                                }
                        }
                    }
                    catch { }
                }
            }
        }

        void PostExam()
        {
            this.mObj_Exam.Post();
            this.Session[Layer01_Constants_Web.CnsSession_Exam_IsSession] = false;
            this.Session[Layer01_Constants_Web.CnsSession_Exam_IsFinish] = true;
            this.Response.Redirect(@"~/Page/ExamFinish.aspx");
        }

        void ExportReport()
        {
            ClsConnection_SqlServer Cn = new ClsConnection_SqlServer();
            try
            {
                Cn.Connect();
                List<Do_Constants.Str_Parameters> List_Sp = new List<Do_Constants.Str_Parameters>();
                List_Sp.Add(new Do_Constants.Str_Parameters("ExamID", Do_Methods.Convert_Int64(this.mObj_Exam.pDr_Exam["RecruitmentTestExamsID"])));

                DataTable Dt = Cn.ExecuteQuery("usp_LoadExam_Detailed", List_Sp).Tables[0];

                Layer01_Common.Objects.ClsExcel_Columns Col = new Layer01_Common.Objects.ClsExcel_Columns();
                Col.Add("Desc");
                Col.Add("Correct_Desc");

                System.IO.StringWriter Sw = Methods_Excel.CreateExcel_HTML(Dt, Col);
                this.Response.Clear();
                this.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Report.xls"));
                this.Response.ContentType = "application/ms-excel";
                this.Response.ContentEncoding = System.Text.Encoding.Default;
                this.Response.Charset = string.Empty;
                this.Response.Write(Sw.ToString());
                this.Response.End();
            }
            catch { }
            finally
            { Cn.Close(); }
        }

        #endregion
    }
}