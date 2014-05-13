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
//using Layer01_Common.Connection;
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
using System.Collections;
using DataObjects_Framework.PreparedQueryObjects;

namespace WebApplication_Exam.Page
{
    public partial class Configuration : ClsBaseMain_Page
    {
        #region _Variables

        Base mBase;
        Interface_DataAccess mDa;

        public const string CnsExam_NoItemsTotal = Layer02_Constants.CnsExam_NoItemsTotal;
        public const string CnsExam_NoItemsPerPage = Layer02_Constants.CnsExam_NoItemsPerPage;
        public const string CnsExam_NoRequiredAnswers = Layer02_Constants.CnsExam_NoRequiredAnswers;
        public const string CnsExam_DefaultContributor_RightsIDs = Layer02_Constants.CnsExam_DefaultContributor_RightsIDs;

        Int64 mNoItemsTotal;
        Int64 mNoItemsPerPage;
        Int64 mNoRequiredAnswers;
        DataTable mDt_DefaultContributor_RightsIDs;
        
        const string CnsIsReadOnly = "CnsIsReadOnly";
        bool mIsReadOnly = false;

        #endregion

        #region _Constructor

        public Configuration()
        {
            this.mBase = new Base();
            this.mDa = this.mBase.pDa;
        }

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, true, Layer02_Constants.eSystem_Modules.Configuration);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);
            
            this.Btn_Save.Click += new EventHandler(Btn_Save_Click);
            this.Grid_RightsIDs.pGrid.ItemCommand += new GridCommandEventHandler(pGrid_ItemCommand);
            this.Selection.EvSelectedMultiple += new UserControl.Control_Selection.Ds_SelectedMultiple(Selection_EvSelectedMultiple);

            if (!this.IsPostBack)
            { this.SetupPage(); }
            else
            {
                this.mIsReadOnly = Do_Methods.Convert_Boolean(this.Session[CnsIsReadOnly + this.pObjID]);
                this.mDt_DefaultContributor_RightsIDs = (DataTable)this.Session[CnsExam_DefaultContributor_RightsIDs + this.pObjID];
            }
        }

        void pGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    {
                        Int64 KeyID = this.Grid_RightsIDs.GetKey(Do_Methods.Convert_Int32(e.Item.ItemIndex));
                        DataRow[] ArrDr = this.mDt_DefaultContributor_RightsIDs.Select("RightsID = " + KeyID);
                        if (ArrDr.Length > 0)
                        { ArrDr[0].Delete(); }
                        break;
                    }
            }

            this.Grid_RightsIDs.RebindGrid();
        }

        void Selection_EvSelectedMultiple(string ControlID, List<long> List_KeyID)
        {
            if (ControlID == this.Btn_AddContributorDefaultRole.ID)
            {
                List<Layer01_Constants.Str_AddSelectedFields> List_Fields = new List<Layer01_Constants.Str_AddSelectedFields>();
                List_Fields.Add(new Layer01_Constants.Str_AddSelectedFields("Name", "Name"));

                Layer02_Common.AddSelected(
                    this.mDt_DefaultContributor_RightsIDs
                    , List_KeyID
                    , "RecruitmentTestRights"
                    , "RecruitmentTestRightsID"
                    , "RightsID"
                    , false
                    , List_Fields);

                this.Grid_RightsIDs.RebindGrid();
            }
        }

        void Btn_Save_Click(object sender, EventArgs e)
        { 
            this.Save();
            this.Save_ReloadPage();
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            bool IsReadOnly = !this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit);
            this.mIsReadOnly = IsReadOnly;
            this.Session[CnsIsReadOnly + this.pObjID] = this.mIsReadOnly;
            WebApplication_Exam.Master.Details.SetupPage_ControlAttributes(this, this.Panel_Details, IsReadOnly);
            this.Btn_Save.Visible = !IsReadOnly;

            //[-]

            this.LoadConfig(this.Master);
            this.BindGrid();

            this.Txt_NoItemsTotal.Text = this.mNoItemsTotal != 0 ? this.mNoItemsTotal.ToString() : "";
            this.Txt_NoItemsPerPage.Text = this.mNoItemsPerPage != 0 ? this.mNoItemsPerPage.ToString() : "";
            this.Txt_NoAnswersRequired.Text = this.mNoRequiredAnswers != 0 ? this.mNoRequiredAnswers.ToString() : "";

            //[-]

            this.SetupPage_Selection();
            this.SetupPage_ControlAttributes();
        }

        void SetupPage_Redirected()
        {
            if (!(this.Session[Layer01_Constants_Web.CnsSession_TmpObj] is Hashtable))
            { return; }

            Hashtable Ht = null;
            try { Ht = (Hashtable)this.Session[Layer01_Constants_Web.CnsSession_TmpObj]; }
            catch { return; }

            if (Ht == null)
            { return; }

            this.Session.Remove(Layer01_Constants_Web.CnsSession_TmpObj);
            this.SetupPage_Redirected(Ht);
        }

        void SetupPage_Redirected(Hashtable Ht)
        {
            bool IsSave = false;
            //IsSave = Do_Methods.Con
            try { IsSave = (bool)Ht["IsSave"]; }
            catch { }

            if (IsSave) 
            { this.Show_EventMsg("Configuration has been saved.", ClsBaseMain_Master.eStatus.Event_Info); }
        }

        void SetupPage_Selection()
        {
            this.Selection.Setup(this.pCurrentUser);

            ClsBindDefinition BindDef = new ClsBindDefinition();
            BindDef.DataSourceName = "RecruitmentTestRights";
            BindDef.KeyName = "RecruitmentTestRightsID";

            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Name", "Role Name", 300));

            BindDef.List_Gc = new List<ClsBindGridColumn>(List_Gc);

            this.Selection.Setup_AddHandlers(this.Btn_AddContributorDefaultRole, this.Grid_RightsIDs.pAjaxPanel, BindDef, true, "Select Role",  450, 400 );
        }

        void SetupPage_ControlAttributes()
        {
            this.Btn_Save.Attributes.Add("onclick", "TempReleaseSave();");
        }

        public void LoadConfig(ClsBaseMain_Master Master)
        {
            Interface_DataAccess Da = this.mDa;
            try
            {
                Da.Connect();
                Interface_Connection Cn = Da.Connection;
                this.mNoItemsTotal = Do_Methods.Convert_Int64(Da.GetSystemParameter(Cn, CnsExam_NoItemsTotal));
                this.mNoItemsPerPage = Do_Methods.Convert_Int64(Da.GetSystemParameter(Cn, CnsExam_NoItemsPerPage));
                this.mNoRequiredAnswers = Do_Methods.Convert_Int64(Da.GetSystemParameter(Cn, CnsExam_NoRequiredAnswers));

                //[-]

                this.mDt_DefaultContributor_RightsIDs = new DataTable();
                this.mDt_DefaultContributor_RightsIDs.Columns.Add("RightsID", typeof(Int64));
                this.mDt_DefaultContributor_RightsIDs.Columns.Add("Name", typeof(string));

                //PreparedQuery Pq = new ClsPreparedQuery((ClsConnection_SqlServer)Cn);
                PreparedQuery Pq = Do_Methods.CreatePreparedQuery();
                Pq.pQuery = "Select Name From RecruitmentTestRights Where RecruitmentTestRightsID = @RightsID";
                Pq.Add_Parameter("RightsID", Do_Constants.eParameterType.Long);
                Pq.Prepare();

                string[] mArrTmp = Do_Methods.Convert_String(Da.GetSystemParameter(Cn, CnsExam_DefaultContributor_RightsIDs)).Split(',');
                foreach (string Tmp in mArrTmp)
                {
                    Int64 RightsID = Do_Methods.Convert_Int64(Tmp);
                    Pq.pParameters.GetParameter("RightsID").Value = RightsID;
                    DataTable InnerDt = Pq.ExecuteQuery().Tables[0];
                    if (InnerDt.Rows.Count > 0)
                    { Do_Methods.AddDataRow(ref this.mDt_DefaultContributor_RightsIDs, new string[] { "RightsID", "Name" }, new object[] { RightsID, InnerDt.Rows[0][0] }); }
                }

                this.Session[CnsExam_DefaultContributor_RightsIDs + Master.pObjID] = this.mDt_DefaultContributor_RightsIDs;
            }
            catch (Exception Ex)
            { throw Ex; }
            finally
            { Da.Close(); }
        }

        void BindGrid()
        {
            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Name", "Role", 250));

            if (!this.mIsReadOnly)
            {
                ClsBindGridColumn_Web_Telerik Gc_Button = new ClsBindGridColumn_Web_Telerik("", "", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button);
                Gc_Button.mCommandName = "Delete";
                Gc_Button.mFieldText = "Delete";
                Gc_Button.mColumnName = "Delete";
                Gc_Button.mButtonType = ButtonColumnType.LinkButton;
                List_Gc.Add(Gc_Button);
            }

            this.Grid_RightsIDs.Setup_FromDataTable(this.pCurrentUser, this.mDt_DefaultContributor_RightsIDs, List_Gc, "RightsID", true, false, Methods_Web_Telerik.eSelectorType.None, "", false);
        }

        void Save()
        {
            if (this.mIsReadOnly)
            { throw new CustomException("Access Denied."); }

            Interface_DataAccess Da = this.mDa;
            try
            {
                Da.Connect();

                Interface_Connection Cn = Da.Connection;

                Int64 NoItemsTotal = Do_Methods.Convert_Int64(this.Txt_NoItemsTotal.Text);
                Int64 NoItemsPerPage = Do_Methods.Convert_Int64(this.Txt_NoItemsPerPage.Text);
                Int64 NoRequiredAnswers = Do_Methods.Convert_Int64(this.Txt_NoAnswersRequired.Text);

                Da.SetSystemParameter(Cn, CnsExam_NoItemsTotal, NoItemsTotal.ToString());
                Da.SetSystemParameter(Cn, CnsExam_NoItemsPerPage, NoItemsPerPage.ToString());
                Da.SetSystemParameter(Cn, CnsExam_NoRequiredAnswers, NoRequiredAnswers.ToString());

                List<string> List_RightsIDs = new List<string>();
                foreach (DataRow Dr in this.mDt_DefaultContributor_RightsIDs.Select("", "", DataViewRowState.CurrentRows))
                { List_RightsIDs.Add(Do_Methods.Convert_Int64(Dr["RightsID"], 0).ToString()); }

                Da.SetSystemParameter(Cn, CnsExam_DefaultContributor_RightsIDs, string.Join(",", List_RightsIDs.ToArray()));

            }
            catch (Exception Ex)
            { throw Ex; }
            finally
            { Da.Close(); }

            this.Show_EventMsg("Configuration has been saved.", ClsBaseMain_Master.eStatus.Event_Info);
        }

        void Save_ReloadPage()
        {
            Hashtable Ht = this.Save_ReloadPage_Prepare();
            this.Session[Layer01_Constants_Web.CnsSession_TmpObj] = Ht;

            String Url = this.Request.Url.AbsolutePath;
            this.Response.Redirect(Url);
        }

        Hashtable Save_ReloadPage_Prepare()
        {
            Hashtable Ht = new Hashtable();
            Ht.Add("IsSave", true);
            return Ht;
        }

        #endregion

        #region _Properties

        public Int64 pNoItemsTotal
        {
            get { return this.mNoItemsTotal; }
        }

        public Int64 pNoItemsPerPage
        {
            get { return this.mNoItemsPerPage; }
        }

        public Int64 pNoRequiredAnswers
        {
            get { return this.mNoRequiredAnswers; }
        }

        #endregion
    }
}