using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common.Objects;
using Layer02_Objects._System;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.Objects;
using WebApplication_Exam.UserControl;

namespace WebApplication_Exam.Base
{
    public abstract class ClsBaseList_Master : MasterPage
    {
        #region _Events

        public delegate void Ds_Generic();
        public delegate void Ds_GenericKeyID(Int64 KeyID);

        public event Ds_Generic EvSetupPage;
        public event Ds_Generic EvSetupPage_Rights;
        public event Ds_GenericKeyID EvDeleteRecord;

        #endregion

        #region _Variables

        const string CnsBase = "CnsBase";
        protected ClsBase mObj_Base;

        const string CnsDataSource = "CnsDataSource";
        protected DataTable mDt_Datasource;

        const string CnsProperties = "CnsProperties";
        ClsBaseList_Master_Properties mProperties;

        public enum eDataSourceType
        {
            FromBase,
            FromDataTable
        }

        bool mIsPageLoaded = false;

        protected bool mRights_IsNew = false;
        protected bool mRights_IsSelect = false;

        #endregion

        #region _Constructor

        public ClsBaseList_Master()
        { this.Load += new EventHandler(Page_Load); }

        public void Setup(
            Layer02_Constants.eSystem_Modules System_ModulesID
            , ClsBase Obj_Base
            , ClsBindDefinition BindDefinition
            , bool IsSelectDetails = true
            , bool IsDelete = true
            )
        {
            this.Setup(System_ModulesID, BindDefinition, eDataSourceType.FromBase, IsSelectDetails, IsDelete);
            this.mObj_Base = Obj_Base;
        }

        public void Setup(
            Layer02_Constants.eSystem_Modules System_ModulesID
            , DataTable Dt_DataSource
            , ClsBindDefinition BindDefinition
            , bool IsSelectDetails = true
            , bool IsDelete = true)
        {
            this.Setup(System_ModulesID, BindDefinition, eDataSourceType.FromDataTable, IsSelectDetails, IsDelete);
            this.mDt_Datasource = Dt_DataSource;
        }

        void Setup(
            Layer02_Constants.eSystem_Modules System_ModulesID
            , ClsBindDefinition BindDefinition
            , eDataSourceType DataSourceType
            , bool IsSelectDetails = true
            , bool IsDelete = true)
        {
            this.Master.Setup(false, true, System_ModulesID);
            this.mProperties = new ClsBaseList_Master_Properties();
            this.mProperties.BindDefinition = BindDefinition;
            this.mProperties.DataSourceType = DataSourceType;
            this.mProperties.IsSelectDetails = IsSelectDetails;
            this.mProperties.IsDelete = IsDelete;

            string DetailsPage = "";
            ClsBase Base = new ClsBase();
            ClsQueryCondition Qc = Base.pDa.CreateQueryCondition();
            Qc.Add("System_ModulesID", ((long)this.pSystem_ModulesID).ToString(), typeof(Int64).Name);

            DataTable Dt = new ClsBase().pDa.GetQuery("System_Modules", "", Qc);
            if (Dt.Rows.Count > 0)
            { DetailsPage = @"~/Page/" + Do_Methods.Convert_String(Dt.Rows[0]["Module_Details"]); }

            this.mProperties.DetailsPage = DetailsPage;

            this.ViewState[CnsProperties] = this.mProperties;
        }

        #endregion

        #region _EventHandlers

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.mIsPageLoaded) { this.mIsPageLoaded = true; }
            else { return; }

            this.Master.Raise_Page_Load();

            if (!this.IsPostBack)
            { this.Raise_SetupPage(); }
            else
            {
                this.mProperties = (ClsBaseList_Master_Properties)this.ViewState[CnsProperties];

                switch (this.mProperties.DataSourceType)
                {
                    case eDataSourceType.FromBase:
                        this.mObj_Base = (ClsBase)this.Session[this.pObjID + CnsBase];
                        break;
                    case eDataSourceType.FromDataTable:
                        this.mDt_Datasource = (DataTable)this.Session[this.pObjID + CnsDataSource];
                        break;
                }
            }
        }

        #endregion

        #region _Methods

        public virtual void SetupPage()
        {
            switch (this.mProperties.DataSourceType)
            { 
                case  eDataSourceType.FromBase:
                    this.Session[this.pObjID + CnsBase] = this.mObj_Base;
                    break;
                case eDataSourceType.FromDataTable:
                    this.Session[this.pObjID + CnsDataSource] = this.mDt_Datasource;
                    break;
            }

            this.Raise_SetupPage_Rights();
        }

        public void SetupPage_Rights()
        {
            this.mRights_IsNew = this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_New);
            this.mRights_IsSelect = this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit) || this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View);
        }

        public void Raise_Page_Load()
        { this.Page_Load(null, null); }

        protected void Raise_SetupPage()
        {
            if (EvSetupPage != null)
            { EvSetupPage(); }
        }

        protected void Raise_SetupPage_Rights()
        {
            if (EvSetupPage_Rights != null)
            { EvSetupPage_Rights(); }
        }

        public void DeleteRecord(Int64 KeyID)
        {
            if (this.mProperties.DataSourceType != eDataSourceType.FromBase)
            { return; }

            if (!this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Delete))
            { throw new ClsCustomException("You have no rights to delete this record."); }

            ClsKeys ClsKey = new ClsKeys();
            ClsKey.Add(this.pProperties.BindDefinition.KeyName, Convert.ToInt64(KeyID));

            ClsBase Obj_Base = (ClsBase)Activator.CreateInstance(this.mObj_Base.GetType(), new object[] { this.pCurrentUser });
            Obj_Base.Load(ClsKey);
            Obj_Base.Delete();

            this.pGridList.RebindGrid(this.pFilterList.pQc);
        }
            
        protected void Raise_DeleteRecord(Int64 KeyID)
        {
            if (EvDeleteRecord != null)
            { EvDeleteRecord(KeyID); }
        }

        public void Show_EventMsg(string Msg, ClsBaseMain_Master.eStatus Status)
        { this.Master.Show_EventMsg(Msg, Status); }

        #endregion

        #region _Properties

        public new ClsBaseMain_Master Master
        {
            get { return (ClsBaseMain_Master)base.Master; }
        }

        public ClsSysCurrentUser pCurrentUser
        {
            get { return this.Master.pCurrentUser; }
        }

        public Layer02_Constants.eSystem_Modules pSystem_ModulesID
        {
            get { return this.Master.pSystem_ModulesID; }
        }

        public string pObjID
        {
            get { return this.Master.pObjID; }
        }

        public ClsBase pObj_Base
        {
            get { return this.mObj_Base; }
        }

        public DataTable pDt_DataSource
        {
            get { return this.mDt_Datasource; }
        }

        public ClsBaseList_Master_Properties pProperties
        {
            get { return this.mProperties; }
        }

        public ClsBindDefinition pBindDefinition
        {
            get { return this.mProperties.BindDefinition; }
        }

        public abstract Control_Filter pFilterList { get; }

        public abstract Control_GridList pGridList { get; }

        public abstract Button pBtn_New { get; }

        public bool pIsPageLoaded
        {
            get { return this.mIsPageLoaded; }
        }

        #endregion
    }

    [Serializable()]
    public class ClsBaseList_Master_Properties
    {
        public ClsBindDefinition BindDefinition;
        public ClsBaseList_Master.eDataSourceType DataSourceType;
        public bool IsSelectDetails;
        public bool IsDelete;
        public string DetailsPage;
    }
}