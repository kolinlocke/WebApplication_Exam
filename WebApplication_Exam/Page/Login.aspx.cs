using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer02_Objects;
using Layer02_Objects._System;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace WebApplication_Exam.Page
{
    public partial class Login : ClsBaseMain_Page
    {
        #region _Variables

        Int32 mType = 0;

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(true);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {            
            base.Page_Load(sender, e);

            this.Btn_Login.Click += this.Btn_Login_Click;
            
            this.mType = Do_Methods.Convert_Int32(this.Request.QueryString["Type"], 0);
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        { this.Method_Login(); }

        #endregion

        #region _Methods

        void Method_Login()
        {
            this.Master.pCurrentUser_New();
            ClsSysCurrentUser CurrentUser = this.Master.pCurrentUser;

            switch (CurrentUser.Login(this.Txt_Username.Text, this.Txt_Password.Text))
            {
                case ClsSysCurrentUser.eLoginResult.LoggedIn:
                case ClsSysCurrentUser.eLoginResult.Administrator:
                    if (!CurrentUser.CheckAccess(Layer02_Constants.eSystem_Modules.Sys_Login, Layer02_Constants.eAccessLib.eAccessLib_Access))
                    {
                        CurrentUser.Logoff();
                        throw new ClsCustomException("You have no access."); 
                    }

                    this.Response.Redirect("~/Page/Default.aspx");

                    break;
                default:
                    this.Show_EventMsg("User Name or Password is incorrect. Please try again.", ClsBaseMain_Master.eStatus.Event_Error);
                    break;
            }
        }

        #endregion
    }
}