using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Telerik.Web.UI;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Common;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects;
using Layer02_Objects._System;
//using Layer02_Objects.DataAccess;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using Layer02_Objects.Modules_Objects.ExamReport;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace WebApplication_Exam.Page
{
    public partial class ExamReport : ClsBaseMain_Page
    {
        #region _Variables

        ClsExamReport mObj_ExamReport;
        string mObjID = "";
        protected const String CnsObjID = "CnsObjID";

        const string CnsLastFieldSort = "CnsLastFieldSort";
        const string CnsLastFieldSort_IsDesc = "CnsLastFieldSort_IsDesc";

        const string CnsDt_Source = "CnsDt_Source";
        DataTable mDt_Source;

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, true, Layer02_Constants.eSystem_Modules.ExamReport);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);
            
            this.ListFilter.EvFiltered += new UserControl.Control_Filter.DsFiltered(ListFilter_EvFiltered);

            if (!this.IsPostBack)
            { this.SetupPage(); }
            else
            {
                try { this.mObjID = (string)this.ViewState[CnsObjID]; }
                catch { }

                try { this.mObj_ExamReport = (ClsExamReport)this.Session[this.mObjID]; }
                catch { }

                try { this.mDt_Source = (DataTable)this.Session[this.mObjID + CnsDt_Source]; }
                catch { }
            }
        }

        void ListFilter_EvFiltered(ClsQueryCondition Qc)
        { this.ReportGrid.RebindGrid(Qc); }

        #endregion

        #region _Methods

        void SetupPage()
        {
            this.mObj_ExamReport = new ClsExamReport();

            this.mObjID = this.Master.pCurrentUser.GetNewPageObjectID();
            this.ViewState[CnsObjID] = this.mObjID;
            this.Session[this.mObjID] = this.mObj_ExamReport;

            this.mDt_Source = this.mObj_ExamReport.GetReport();
            this.Session[this.mObjID + CnsDt_Source] = this.mDt_Source;

            this.BindGrid();
        }

        void BindGrid()
        {
            ClsBase Base = new ClsBase();
            Interface_DataAccess Da = Base.pDa;
            string Limit = Da.GetSystemParameter(Configuration.CnsExam_NoItemsPerPage);

            List<ClsBindGridColumn_Web_Telerik> List_Gct = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("DateTaken", "Date Taken", 200));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("RecruitmentTestApplicant_Name", "Applicant Name", 300, ""));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("RecruitmentTestApplicant_Email", "Applicant Email", 300));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Computed_Score", "Score", 100));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Computed_TotalItems", "Total Items", 100));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Time", "Time Taken", 200, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Static, true, true, false));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("Time_Value", "Time Taken", 0, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Static, false, false, true));
            List_Gct.Add(new ClsBindGridColumn_Web_Telerik("ScoreChanged_Desc", "Score Changed?", 100));

            if (this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View))
            {
                ClsBindGridColumn_Web_Telerik Gc_Btn = new ClsBindGridColumn_Web_Telerik("", "", 50, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_HyperLink);
                Gc_Btn.mFieldNavigateUrl_Text = this.ResolveUrl(@"~/Page/Exam.aspx?ID={0}&Limit=" + Limit);
                Gc_Btn.mFieldNavigateUrl_Field = "RecruitmentTestExamsID";
                Gc_Btn.mFieldText = "Details";
                List_Gct.Add(Gc_Btn);
            }

            ReportGrid.Setup_FromDataTable(this.Master.pCurrentUser, this.mDt_Source, List_Gct, "RecruitmentTestExamsID", true, true);

            this.ListFilter.Setup(this.pCurrentUser, new List<ClsBindGridColumn>(List_Gct), this.mDt_Source.Clone(), this.ReportGrid.pAjaxPanel);
        }

        #endregion        
    }
}