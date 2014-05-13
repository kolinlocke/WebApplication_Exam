using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using WebApplication_Exam._Base;
using Telerik.Web.UI;

namespace WebApplication_Exam.Page
{
    public partial class ContributorApproval : ClsBaseMain_Page
    {
        #region _Variables

        ClsContributorRegistration mObj;
        
        #endregion

        #region _EventHandlers

        protected override void Page_Load(object sender, EventArgs e)
        {
            this.Master.Setup(false, true, Layer02_Constants.eSystem_Modules.ContributorApproval, true);
            base.Page_Load(sender, e);
            
            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.Btn_Approve.Click += new EventHandler(Btn_Approve_Click);

            if (this.IsPostBack)
            { this.mObj = (ClsContributorRegistration)this.Session[this.pObjID]; }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.SetupPage();
                this.SetupPage_Redirected();
            }
        }

        void Btn_Approve_Click(object sender, EventArgs e)
        { this.Process_Approve(); }

        #endregion

        #region _Methods

        void SetupPage()
        {
            this.mObj = new ClsContributorRegistration(this.Master.pCurrentUser);
            this.Session[this.pObjID] = this.mObj;

            this.mObj.List();

            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Name", "Contributor Name", 400));
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Email", "Email", 400));
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("DateRequested", "Date Requested", 120,"{0:MMM dd, yyyy}"));
            this.GridList.Setup_FromDataTable(this.Master.pCurrentUser, this.mObj.pDt_List, List_Gc, "TmpKey", true, true, Methods_Web_Telerik.eSelectorType.Multiple);
        }

        void SetupPage_Redirected()
        {
            Hashtable Ht = null;
            if (!(this.Session[Layer01_Constants_Web.CnsSession_TmpObj] is System.Collections.Hashtable))
            { return; }

            try
            { Ht = (Hashtable)this.Session[Layer01_Constants_Web.CnsSession_TmpObj]; }
            catch
            { return; }

            if (Ht != null)
            {
                this.Session.Remove(Layer01_Constants_Web.CnsSession_TmpObj);
                bool IsSave = false;
                bool IsEmailFailed = false;

                try { IsSave = (bool)Ht["IsSave"]; }
                catch { }

                try { IsEmailFailed = (bool)Ht["IsEmailFailed"]; }
                catch { }

                if (IsSave)
                {
                    StringBuilder Sb = new StringBuilder();
                    Sb.Append("Selected Contributor(s) have been approved." + "<br />");

                    if (IsEmailFailed)
                    { Sb.Append("Email sending to contributors has failed. Please contact your system administrator." + "<br />"); }

                    this.Show_EventMsg(Sb.ToString(), ClsBaseMain_Master.eStatus.Event_Info);
                }
            }
        }

        void Process_Approve()
        {
            foreach (GridItem Gi in this.GridList.pGrid.SelectedItems)
            {
                GridDataItem Gdi = (GridDataItem)Gi;
                DataRow[] ArrDr = this.mObj.pDt_List.Select("TmpKey = " + Gdi.GetDataKeyValue("TmpKey"));
                if (ArrDr.Length > 0)
                { ArrDr[0]["IsSelected"] = true; }
                else
                { break; }
            }

            BaseObjs Obj_Users = this.mObj.Approve();

            //[-]

            bool IsEmailFailed = false;
            try
            {
                SmtpClient Sc = new SmtpClient();
                Sc.Host = "smtp.gmail.com";
                Sc.Port = 587;
                Sc.EnableSsl = true;
                Sc.Credentials = new NetworkCredential("Pti.ExamManagement@gmail.com", "Administrator");

                foreach (BaseObjs.Str_Obj Obj in Obj_Users.pList_Obj)
                {
                    ClsUser Obj_User = (ClsUser)Obj.Obj;                    
                    string Email = Do_Methods.Convert_String(Obj_User.pDr["Email"]);

                    StringBuilder Sb_Email = new StringBuilder();
                    Sb_Email.Append("Dear " + Do_Methods.Convert_String(Obj_User.pDr["Name"]) + ",<br /><br />");
                    Sb_Email.Append("You have been approved as a contributor.<br />");
                    Sb_Email.Append("Your temporary password is: " + Do_Methods.Convert_String(Obj_User.pDr["Password"]) + " <br />");
                    Sb_Email.Append("Use your temporary password to login and changed it using the password change page.<br />");

                    MailMessage Message = new MailMessage();
                    Message.Subject = "Contributor Approval";
                    Message.IsBodyHtml = true;
                    Message.Body = Sb_Email.ToString();
                    Message.From = new MailAddress("Pti.ExamManagement@gmail.com");
                    Message.To.Add(Email);

                    Sc.Send(Message);
                }
            }
            catch
            { IsEmailFailed = true; }
            
            //[-]

            this.Session.Remove(this.pObjID);
            System.Collections.Hashtable Ht = new System.Collections.Hashtable();
            Ht.Add("IsSave", true);
            Ht.Add("IsEmailFailed", IsEmailFailed);
            this.Session[Layer01_Constants_Web.CnsSession_TmpObj] = Ht;

            string Url = this.Request.Url.AbsolutePath;
            this.Response.Redirect(Url);
        }

        #endregion
    }
}