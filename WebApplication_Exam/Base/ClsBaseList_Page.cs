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
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using WebApplication_Exam.UserControl;

namespace WebApplication_Exam.Base
{
    public class ClsBaseList_Page : System.Web.UI.Page
    {
        #region _Variables

        bool mIsPageLoaded = false;

        #endregion

        #region _Constructor

        public ClsBaseList_Page()
        {
            this.Page.Init += new EventHandler(Page_Init);
            this.Page.Load += new EventHandler(Page_Load); 
        }

        public void Setup(
            Layer02_Constants.eSystem_Modules System_ModulesID
            , ClsBase Obj_Base
            , ClsBindDefinition BindDefinition
            , bool IsSelectDetails = true
            , bool IsDelete = true)
        { this.Master.Setup(System_ModulesID, Obj_Base, BindDefinition, IsSelectDetails, IsDelete); }

        public void Setup(
            Layer02_Constants.eSystem_Modules System_ModulesID
            , DataTable Dt_DataSource
            , ClsBindDefinition BindDefinition
            , bool IsSelectDetails = true
            , bool IsDelete = true)
        { this.Master.Setup(System_ModulesID, Dt_DataSource, BindDefinition, IsSelectDetails, IsDelete); }

        #endregion

        #region _EventHandlers

        protected virtual void Page_Init(object sender, EventArgs e) { }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.mIsPageLoaded) { this.mIsPageLoaded = true; }
            else { return; }

            this.Master.EvSetupPage += new ClsBaseList_Master.Ds_Generic(SetupPage);
            this.Master.EvSetupPage_Rights += new ClsBaseList_Master.Ds_Generic(SetupPage_Rights);
            this.Master.EvDeleteRecord += new ClsBaseList_Master.Ds_GenericKeyID(DeleteRecord);

            this.Master.Raise_Page_Load();

            if (!this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Access))
            { throw new ClsCustomException("You have no access to this page."); }
        }

        #endregion

        #region _Methods

        protected virtual void SetupPage()
        { this.Master.SetupPage(); }

        protected virtual void SetupPage_Rights()
        { this.Master.SetupPage_Rights(); }

        protected virtual void DeleteRecord(long KeyID)
        { this.Master.DeleteRecord(KeyID); }

        protected void Show_EventMsg(string Msg, ClsBaseMain_Master.eStatus Status)
        { this.Master.Show_EventMsg(Msg, Status); }

        #endregion

        #region _Properties

        public new ClsBaseList_Master Master
        {
            get { return (ClsBaseList_Master)base.Master; }
        }

        public Layer02_Constants.eSystem_Modules pSystem_ModulesID
        {
            get { return this.Master.pSystem_ModulesID; }
        }

        public string pObjID
        {
            get { return this.Master.pObjID; }
        }

        public ClsSysCurrentUser pCurrentUser
        {
            get { return this.Master.pCurrentUser; }
        }

        public ClsBaseList_Master_Properties pProperties
        {
            get { return this.Master.pProperties; }
        }

        public Control_Filter pFilterList
        {
            get { return this.Master.pFilterList; }
        }

        public Control_GridList pGridList
        {
            get { return this.Master.pGridList; }
        }

        public Button pBtn_New
        {
            get { return this.Master.pBtn_New; }
        }

        public bool pIsPageLoaded
        {
            get { return this.mIsPageLoaded; }
        }

        #endregion
    }
}