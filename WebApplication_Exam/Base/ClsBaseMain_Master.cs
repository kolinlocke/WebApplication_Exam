using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.VisualBasic;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
//using Layer02_Objects.Modules_Base.Objects;
using Layer02_Objects._System;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace WebApplication_Exam.Base
{
    public abstract class ClsBaseMain_Master : System.Web.UI.MasterPage
    {
        #region _Variables

        protected string mPageTitle = "";
        protected string mServerRoot = "";
        protected ClsSysCurrentUser mCurrentUser;
        protected bool mIsPageLogin = false;
        protected bool mCheckLogin = true;
        protected bool mIsAdminPage = false;
        protected bool mIsContributorPage = false;
        protected bool mIsShowLoginPanel = true;

        const string CnsProperties = "CnsProperties";
        protected ClsBaseMain_Master_Properties mProperties;

        const string CnsObjID = "CnsObjID";
        string mObjID = "";

        protected delegate void Delegate_Page_Init(object sender, System.EventArgs e);

        public enum eStatus : int
        {
            Event_Info
            , Event_Error
        }

        bool mIsPageLoaded = false;

        #endregion

        #region _Constructor

        public ClsBaseMain_Master()
        {
            this.Init += Page_Init;
            this.Load += Page_Load;
        }

        public void Setup(
            bool pIsPageLogin = false
            , bool pCheckLogin = true
            , Layer02_Constants.eSystem_Modules pSystem_ModulesID = Layer02_Constants.eSystem_Modules.None
            , bool pIsAdminPage = false
            , bool pIsContributorPage = false
            , bool pIsShowLoginPanel = true)
        {
            this.mIsPageLogin = pIsPageLogin;
            this.mCheckLogin = pCheckLogin;
            this.mIsAdminPage = pIsAdminPage;
            this.mIsContributorPage = pIsContributorPage;
            this.mIsShowLoginPanel = pIsShowLoginPanel;

            this.mProperties = new ClsBaseMain_Master_Properties();
            this.mProperties.System_ModulesID = pSystem_ModulesID;
            this.ViewState[CnsProperties] = this.mProperties;
        }

        #endregion

        #region _EventHandlers

        protected void Page_Init(object sender, System.EventArgs e)
        {
            this.mServerRoot = this.ResolveUrl(@"~/");
            ClsSysCurrentUser CurrentUser = this.pCurrentUser;
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.mIsPageLoaded)
            { this.mIsPageLoaded = true; }
            else
            { return; }

            ClsSysCurrentUser CurrentUser = this.pCurrentUser;
            if (this.mCheckLogin)
            {
                if (!CurrentUser.pIsLoggedIn)
                {
                    if (!this.mIsPageLogin)
                    {
                        this.Session.Clear();
                        this.pCurrentUser_New();
                        this.Response.Redirect("~/Page/Default.aspx");
                        return;
                    }
                }
                else
                {
                    if (this.mIsPageLogin)
                    { this.Response.Redirect("~/Page/Menu.aspx"); }
                }
            }

            //[-]

            if (!this.IsPostBack)
            { this.SetupPage_ObjID(); }
            else
            {
                this.mObjID = (string)this.ViewState[CnsObjID];
                this.mProperties = (ClsBaseMain_Master_Properties)this.ViewState[CnsProperties];
            }
        }

        #endregion

        #region _Methods

        public void Raise_Page_Load()
        { this.Page_Load(null, null); }
        
        public void SetupPage_ObjID()
        {
            this.mObjID = this.pCurrentUser.GetNewPageObjectID();
            this.ViewState[CnsObjID] = this.mObjID;
        }

        public void pCurrentUser_New()
        {
            this.mCurrentUser = new ClsSysCurrentUser();
            this.Session[Layer01_Constants_Web.CnsSession_CurrentUser] = this.mCurrentUser;
        }

        #endregion

        #region _AbstractMethods

        public abstract void Show_EventMsg(string Msg, eStatus Status);

        #endregion

        #region _Properties

        public ClsSysCurrentUser pCurrentUser
        {
            get
            {
                try { this.mCurrentUser = (ClsSysCurrentUser)this.Session[Layer01_Constants_Web.CnsSession_CurrentUser]; }
                catch { }

                if (this.mCurrentUser == null) this.pCurrentUser_New();
                return this.mCurrentUser;
            }
            set
            { this.mCurrentUser = value; }

        }

        public string pServerRoot
        {
            get { return this.mServerRoot; }
        }

        //[-]

        public Layer02_Constants.eSystem_Modules pSystem_ModulesID
        {
            get { return this.mProperties.System_ModulesID; }
        }

        public string pObjID
        {
            get { return this.mObjID; }
        }

        public abstract Panel pPanel_Login { get; }

        public bool pIsPageLoaded
        {
            get { return this.mIsPageLoaded; }
        }

        #endregion
    }

    [Serializable()]
    public class ClsBaseMain_Master_Properties
    {
        public Layer02_Constants.eSystem_Modules System_ModulesID = Layer02_Constants.eSystem_Modules.None;
    }
}