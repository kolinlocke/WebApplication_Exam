using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
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
    public abstract class ClsBaseDetails_Page : System.Web.UI.Page
    {
        #region _Variables

        bool mIsPageLoaded = false;

        #endregion

        #region _Constructor

        public ClsBaseDetails_Page()
        {
            this.Init += new EventHandler(Page_Init);
            this.Load += new EventHandler(Page_Load); 
        }

        public void Setup(Layer02_Constants.eSystem_Modules System_ModulesID, ClsBase Obj_Base, string NoAccessMessage = "")
        { this.Master.Setup(System_ModulesID, Obj_Base, NoAccessMessage); }

        #endregion

        #region _EventHandlers

        protected virtual void Page_Init(object sender, EventArgs e) { }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!this.mIsPageLoaded) { this.mIsPageLoaded = true; }
            else { return; }

            this.Master.pBtn_Save.Click += new EventHandler(pBtn_Save_Click);
            this.Master.pBtn_Delete.Click += new EventHandler(pBtn_Delete_Click);
            this.Master.EvSetupPage_ControlAttributes += new ClsBaseDetails_Master.DsGeneric(SetupPage_ControlAttributes);
            this.Master.EvSetupPage_Rights += new ClsBaseDetails_Master.DsGeneric(SetupPage_Rights);

            this.Master.Raise_Page_Load();

            if (!this.IsPostBack) { this.SetupPage_Redirected(); }
        }

        void pBtn_Save_Click(object sender, EventArgs e) 
        { this.Begin_Save(); }

        void pBtn_Delete_Click(object sender, EventArgs e) 
        { this.Begin_Delete(); }

        #endregion

        #region _Methods

        protected virtual void SetupPage_ControlAttributes()
        { this.Master.SetupPage_ControlAttributes(); }

        void SetupPage_Rights()
        { this.Master.SetupPage_Rights(); }

        void SetupPage_Redirected()
        {
            if (!(this.Session[Layer01_Constants_Web.CnsSession_TmpObj] is Hashtable)) { return; }

            Hashtable Ht = null;
            try { Ht = (Hashtable)this.Session[Layer01_Constants_Web.CnsSession_TmpObj]; }
            catch { return; }

            if (Ht == null) { return; }

            this.Session.Remove(Layer01_Constants_Web.CnsSession_TmpObj);
            this.SetupPage_Redirected(Ht);
        }

        public virtual void SetupPage_Redirected(Hashtable Ht)
        {
            bool IsSave = false;
            try { IsSave = (bool)Ht["IsSave"]; }
            catch { }

            if (IsSave) { this.Show_EventMsg("Record has been saved.", ClsBaseMain_Master.eStatus.Event_Info); }
        }

        //[-]

        protected void Begin_Save()
        {
            if (this.pIsReadOnly)
            {
                this.Show_EventMsg(this.pNoAccessMessage, ClsBaseMain_Master.eStatus.Event_Error);
                return;
            }

            this.Save();
            this.Save_ReloadPage();
        }

        protected virtual void Save()
        { this.pObj_Base.Save(); }

        protected void Begin_Delete()
        {
            if (this.pIsReadOnly)
            {
                this.Show_EventMsg(this.pNoAccessMessage + "<br />", ClsBaseMain_Master.eStatus.Event_Error);
                return;
            }

            this.Delete();

            Response.Redirect(this.pListPage);
        }

        protected virtual void Delete()
        {
            Int64 KeyID = this.pObj_Base.pID;
            ClsKeys ClsKey = new ClsKeys();
            ClsKey.Add(this.pObj_Base.pHeader_TableKey, KeyID);

            ClsBase Obj_Base = (ClsBase)Activator.CreateInstance(this.pObj_Base.GetType(), new object[] { this.pCurrentUser });
            Obj_Base.Load(ClsKey);
            Obj_Base.Delete();
        }
        
        protected virtual Hashtable Save_ReloadPage_Prepare()
        {
            Hashtable Ht = new Hashtable();
            Ht.Add("IsSave", true);
            return Ht;            
        }
        
        void Save_ReloadPage()
        {
            Hashtable Ht = this.Save_ReloadPage_Prepare();
            this.Session[Layer01_Constants_Web.CnsSession_TmpObj] = Ht;
            this.Session.Remove(ClsBaseDetails_Master.CnsBase + this.Master.pObjID);

            string Url = this.Request.Url.AbsolutePath + "?ID=" + this.pObj_Base.pID;
            this.Response.Redirect(Url);
        }

        //[-]

        protected void Show_EventMsg(string Msg, ClsBaseMain_Master.eStatus Status)
        { this.Master.Show_EventMsg(Msg, Status); }

        #endregion

        #region _Properties

        public new ClsBaseDetails_Master Master
        {
            get { return (ClsBaseDetails_Master)base.Master; }
        }

        public ClsBase pObj_Base
        {
            get { return this.Master.pObj_Base; }
        }

        public ClsSysCurrentUser pCurrentUser
        {
            get { return this.Master.pCurrentUser; }
            set { this.Master.pCurrentUser = value; }
        }

        public Layer02_Constants.eSystem_Modules pSystem_ModulesID
        {
            get { return this.Master.pSystem_ModulesID; }
        }

        public string pServerRoot
        {
            get { return this.Master.pServerRoot; }
        }

        public bool pIsPageLoaded
        {
            get { return this.mIsPageLoaded; }
        }

        //[-]

        public bool pIsNew
        {
            get { return this.Master.pIsNew; }
            set { this.Master.pIsNew = value; }
        }

        public bool pIsReadOnly
        {
            get { return this.Master.pIsReadOnly; }
            set { this.Master.pIsReadOnly = value; }
        }

        public string pNoAccessMessage
        {
            get { return this.Master.pNoAccessMessage; }
        }

        public string pListPage
        {
            get { return this.Master.pListPage; }
        }

        #endregion
    }
}