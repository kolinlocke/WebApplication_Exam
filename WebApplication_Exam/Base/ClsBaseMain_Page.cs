using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Layer02_Objects;
using Layer02_Objects.Modules_Objects;
using Layer02_Objects._System;

namespace WebApplication_Exam._Base
{
    public class ClsBaseMain_Page:  System.Web.UI.Page
    {
        #region _Variables

        const string CnsIsLoaded = "CnsIsLoaded";
        bool mIsLoaded = false;
        bool mIsPageLoaded = false;

        #endregion

        #region _Constructor

        public ClsBaseMain_Page()
        {
            this.Page.Init += new EventHandler(Page_Init);
            this.Page.Load += new EventHandler(Page_Load); 
        }

        public void Setup(
            bool pIsPageLogin = false
            , bool pCheckLogin = true
            , Layer02_Constants.eSystem_Modules pSystem_ModulesID = Layer02_Constants.eSystem_Modules.None
            , bool pIsAdminPage = false
            , bool pIsContributorPage = false
            , bool pIsShowLoginPanel = true)
        {
            this.Master.Setup(
            pIsPageLogin
            , pCheckLogin
            , pSystem_ModulesID
            , pIsAdminPage
            , pIsContributorPage
            , pIsShowLoginPanel);
        }

        #endregion

        #region _EventHandlers

        protected virtual void Page_Init(object sender, EventArgs e) { }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.mIsPageLoaded) { this.mIsPageLoaded = true; }
            else { return; }

            this.Master.Raise_Page_Load(); 
        }

        #endregion

        #region _Methods

        protected bool Check_IsLoaded()
        {
            bool Rv = this.mIsLoaded;
            if (!Rv)
            { this.mIsLoaded = true; }
            return Rv;
        }

        protected void Show_EventMsg(string Msg, ClsBaseMain_Master.eStatus Status)
        { this.Master.Show_EventMsg(Msg, Status); }

        #endregion

        #region _Properties

        public new ClsBaseMain_Master Master
        {
            get { return (ClsBaseMain_Master)base.Master; }
        }

        public string pObjID
        {
            get { return this.Master.pObjID; }
        }

        public ClsSysCurrentUser pCurrentUser
        {
            get { return this.Master.pCurrentUser; }
        }

        public Layer02_Constants.eSystem_Modules pSystem_ModulesID
        {
            get { return this.Master.pSystem_ModulesID; }
        }

        public bool pIsPageLoaded
        {
            get { return this.mIsPageLoaded; }
        }

        #endregion
    }
}