using System;
using System.Collections;
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
using Layer02_Objects._System;
//using Layer02_Objects.DataAccess;
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

namespace WebApplication_Exam.Page
{
    public partial class ChangePassword : ClsBaseMain_Page
    {

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, true);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {            
            base.Page_Load(sender, e);
            this.Btn_Save.Click += new EventHandler(Btn_Save_Click);
            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            { this.SetupPage_Redirected(); }
        }

        void Btn_Save_Click(object sender, EventArgs e)
        { this.Save(); }

        void SetupPage_Redirected()
        {
            Hashtable Ht = null;
            if (!(this.Session[Layer01_Constants_Web.CnsSession_TmpObj] is Hashtable))
            { return; }

            try
            { Ht = (Hashtable)this.Session[Layer01_Constants_Web.CnsSession_TmpObj]; }
            catch
            { return; }

            if (Ht != null)
            {
                this.Session.Remove(Layer01_Common_Web.Common.Layer01_Constants_Web.CnsSession_TmpObj);
                bool IsSave = false;

                try { IsSave = (bool)Ht["IsSave"]; }
                catch { }

                if (IsSave)
                { this.Show_EventMsg("Password has been changed succesfully.", ClsBaseMain_Master.eStatus.Event_Info); }
            }
        }

        void Save()
        {
            ClsUser Obj_User = new ClsUser();
            Keys Key = new Keys();
            Key.Add(Obj_User.pHeader_TableKey, this.Master.pCurrentUser.pUserID);
            Obj_User.Load(Key);

            Obj_User.pDr["Password"] = this.Txt_Password.Text;

            Obj_User.Save();

            //[-]

            Hashtable Ht = new System.Collections.Hashtable();
            Ht.Add("IsSave", true);

            this.Session[Layer01_Constants_Web.CnsSession_TmpObj] = Ht;

            string Url = this.Request.Url.AbsolutePath;
            this.Response.Redirect(Url);
        }

    }
}