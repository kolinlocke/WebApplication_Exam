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

namespace WebApplication_Exam._Base
{
    public abstract class ClsBaseDetails_Master : System.Web.UI.MasterPage
    {
        #region _Variables

        public const string CnsProperties = "CnsProperties";
        protected ClsBaseDetails_Master_Properties mProperties;
        
        public const string CnsBase = "CnsBase";
        protected Base mObj_Base;

        bool mIsPageLoaded = false;

        #endregion

        #region _Events

        public delegate void DsGeneric();
        public event DsGeneric EvSetupPage_ControlAttributes;
        public event DsGeneric EvSetupPage_Rights;
        
        #endregion

        #region _Constructor

        public ClsBaseDetails_Master()
        { this.Load += new EventHandler(Page_Load); }

        public void Setup(Layer02_Constants.eSystem_Modules System_ModulesID, Base Obj_Base, string NoAccessMessage = "")
        {
            this.Master.Setup(false, true, System_ModulesID);
            this.mObj_Base = Obj_Base;
            this.mProperties = new ClsBaseDetails_Master_Properties();
            this.mProperties.NoAccessMessage = NoAccessMessage == "" ? "Access Denied." : NoAccessMessage;

            DataTable Dt = Do_Methods_Query.GetQuery("System_Modules", "", "System_ModulesID = " + (long)System_ModulesID);
            if (Dt.Rows.Count > 0)
            { this.mProperties.ListPage = Do_Methods.Convert_String(Dt.Rows[0]["Module_List"]); }
        }

        #endregion

        #region _EventHandlers

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.mIsPageLoaded) { this.mIsPageLoaded = true; }
            else { return; }
            
            this.Master.Raise_Page_Load();

            if (!this.IsPostBack)
            { this.SetupPage(); }
            else
            {
                this.mObj_Base = (Base)this.Session[CnsBase + this.pObjID];
                this.mProperties = (ClsBaseDetails_Master_Properties)this.Session[this.pObjID + CnsProperties];
            }
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            this.SetupPage_Properties();

            //[-]

            Int64 ID = Do_Methods.Convert_Int64(this.Request.QueryString["ID"]);
            this.pIsNew = ID == 0;

            this.Raise_SetupPage_Rights();

            //[-]

            this.Session[CnsBase + this.pObjID] = this.mObj_Base;

            ClsSysCurrentUser CurrentUser = this.Master.pCurrentUser;
            
            Keys Key = null;

            if (ID != 0)
            {
                Key = new Keys();
                Key.Add(this.mObj_Base.pHeader_TableKey, ID);
            }

            this.mObj_Base.Load(Key);
        }

        public void SetupPage_Rights()
        {
            /*
            Document Base Rights
            A New Record can be created if:
                The user has the New Rights.
            A Record can be viewed if:
                The user has Edit or View Rights
            A Record can be edited if:
                The User has the Edit Rights
            */

            if (
                    !(
                        (this.pCurrentUser.CheckAccess(pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_New) && this.pIsNew)
                        || (this.pCurrentUser.CheckAccess(pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit))
                        || (this.pCurrentUser.CheckAccess(pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_View))
                    )
                )
            { throw new CustomException(this.mProperties.NoAccessMessage); }

            this.pIsReadOnly = !(this.pIsNew || this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Edit));
        }
        
        public void SetupPage_Properties()
        { this.Session[this.pObjID + CnsProperties] = this.mProperties; }

        public void Raise_Page_Load()
        { this.Page_Load(null, null); }

        public void Raise_SetupPage_Rights()
        {
            if (EvSetupPage_Rights != null)
            { EvSetupPage_Rights(); }
        }

        protected void Raise_SetupPage_ControlAttributes()
        {
            if (EvSetupPage_ControlAttributes != null)
            { EvSetupPage_ControlAttributes(); }
        }

        public void Show_EventMsg(string Msg, ClsBaseMain_Master.eStatus Status)
        { this.Master.Show_EventMsg(Msg, Status); }

        #endregion

        #region _AbstractMethods

        public abstract void SetupPage_ControlAttributes();

        #endregion

        #region _Properties

        public new ClsBaseMain_Master Master
        {
            get { return (ClsBaseMain_Master)base.Master; }
        }

        public ClsSysCurrentUser pCurrentUser
        {
            get { return this.Master.pCurrentUser; }
            set { this.Master.pCurrentUser = value; }
        }

        public string pServerRoot
        {
            get { return this.Master.pServerRoot; }
        }

        public Layer02_Constants.eSystem_Modules pSystem_ModulesID
        {
            get { return this.Master.pSystem_ModulesID; }
        }

        public string pObjID
        {
            get { return this.Master.pObjID; }
        }

        public Base pObj_Base
        {
            get { return this.mObj_Base; }
        }

        //[-]

        public bool pIsNew
        {
            get { return this.mProperties.IsNew; }
            set { this.mProperties.IsNew = value; }
        }

        public bool pIsReadOnly
        {
            get { return this.mProperties.IsReadOnly; }
            set { this.mProperties.IsReadOnly = value; }
        }

        public string pNoAccessMessage
        {
            get { return this.mProperties.NoAccessMessage; }
        }

        public string pListPage
        {
            get { return this.mProperties.ListPage; }
        }

        //[-]

        public abstract Button pBtn_Save { get; }

        public abstract Button pBtn_Delete { get; }

        #endregion
    }

    public class ClsBaseDetails_Master_Properties
    {
        public bool IsNew;
        public bool IsReadOnly;
        public string NoAccessMessage;
        public string ListPage;
    }
}