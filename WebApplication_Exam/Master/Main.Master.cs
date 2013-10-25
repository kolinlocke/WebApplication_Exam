using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using Layer01_Common;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Layer02_Objects;
using Layer02_Objects._System;

namespace WebApplication_Exam.Master
{
    public partial class Main : ClsBaseMain_Master
    {
        #region _EventHandlers

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);

            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.Btn_Logout.Click += new EventHandler(Btn_Logout_Click);
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (pCurrentUser.pIsLoggedIn)
            {
                this.Panel_Login.Visible = this.mIsShowLoginPanel;
                if (this.pCurrentUser.pIsSystemAdmin)
                { this.Lbl_User.Text = (String)Do_Methods.IsNull(pCurrentUser.pDrUser["Name"], ""); }
                else
                { this.Lbl_User.Text = "<a href='" + this.ResolveUrl("~/Page/ChangePassword.aspx") + "'>" + (String)Do_Methods.IsNull(pCurrentUser.pDrUser["Name"], "") + "</a>"; }
            }
            else
            { this.Panel_Login.Visible = false; }
        }

        void Btn_Logout_Click(object sender, EventArgs e)
        {
            this.pCurrentUser.Logoff();
            this.Session.Clear();
            this.Response.Redirect("~/Page/Default.aspx");
        }

        #endregion

        #region _Methods

        public override void  Show_EventMsg(string Msg, eStatus Status)
        {
            switch (Status)
            { 
                case eStatus.Event_Info:
                    this.Img_Event.ImageUrl = "~/Images/cp_Msgbox_Info.gif";
                    break;
                case eStatus.Event_Error:
                    this.Img_Event.ImageUrl = "~/Images/cp_error01.gif";
                    break;
            }

            this.Lbl_EventMsg.Text = Msg;
            this.Panel_Event.Visible = true;
        }

        #endregion

        #region _Properties

        public override Panel pPanel_Login
        {
            get { return this.Panel_Login; }
        }

        #endregion
    }
}