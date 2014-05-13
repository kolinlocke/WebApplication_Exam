using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ContributorRegistration : ClsBaseMain_Page
    {
        #region _Variables

        public const string CnsContributorRegistration = "CnsContributorRegistration";

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, false);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            this.Btn_Register.Click += new EventHandler(Btn_Register_Click);
            this.Btn_Back.Click += new EventHandler(Btn_Back_Click);
        }

        void Btn_Register_Click(object sender, EventArgs e)
        { this.Process_Register(); }

        void Btn_Back_Click(object sender, EventArgs e)
        { this.Response.Redirect("~/Page/Default.aspx"); }

        #endregion

        #region _Methods

        void Process_Register()
        {
            StringBuilder Sb_ErrorMsg = new StringBuilder();
            if (!this.Process_Validation(ref Sb_ErrorMsg))
            {
                this.Show_EventMsg(Sb_ErrorMsg.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
                return;
            }

            ClsContributorRegistration Obj_Cr = new ClsContributorRegistration(this.Master.pCurrentUser);
            Obj_Cr.Load();
            Obj_Cr.pDr["Name"] = this.Txt_UserName.Text;
            Obj_Cr.pDr["Email"] = this.Txt_Email.Text;
            Obj_Cr.Save();

            //[-]

            System.Collections.Hashtable Ht = new System.Collections.Hashtable();
            Ht.Add(CnsContributorRegistration, true);
            this.Session[Layer01_Constants_Web.CnsSession_TmpObj] = Ht;
            this.Response.Redirect("~/Page/ContributorRegistrationFinish.aspx");
        }

        bool Process_Validation(ref System.Text.StringBuilder Sb_Msg)
        {
            WebControl Wc;
            bool IsValid = true;

            Wc = this.Txt_UserName;
            if (Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (this.Txt_UserName.Text != "")
                , "User Name is required." + "<br />"))
            {
                QueryCondition Qc = Do_Methods.CreateQueryCondition();
                Qc.Add("Name", " = " + this.Txt_UserName.Text, typeof(string).ToString());
                Qc.Add("IsDeleted", "0", typeof(bool).ToString(), "0");

                DataTable Dt = Do_Methods_Query.GetQuery("RecruitmentTestUser", "", Qc);
                bool IsValid_User = !(Dt.Rows.Count > 0);

                Wc = this.Txt_UserName;
                Methods_Web.ControlValidation(
                    ref Sb_Msg
                    , ref Wc
                    , ref IsValid
                    , Layer01_Constants_Web.CnsCssTextbox
                    , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                    , IsValid_User
                    , "User Name already exists. Please choose a different User Name." + "<br />");
            }

            Wc = this.Txt_UserName;
            Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (!Regex.Match(this.Txt_UserName.Text, @"[^A-Za-z0-9_.]").Success)
                , "User Name must only contain alphanumeric characters." + "<br />");

            Wc = this.Txt_Email;
            Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (this.Txt_Email.Text.Trim() != "")
                , "Email is required.");

            Wc = this.Txt_Email;
            Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , Regex.Match(this.Txt_Email.Text, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Success
                , "Email address must be in the correct format.");

            return IsValid;
        }

        #endregion
    }
}