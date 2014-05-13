using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using WebApplication_Exam;
using WebApplication_Exam._Base;

namespace WebApplication_Exam.Page
{
    public partial class ContributorRegistrationFinish : ClsBaseMain_Page
    {
        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, false);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {            
            base.Page_Load(sender, e);

            bool IsRedirect = false;

            Hashtable Ht = null;
            if (!(this.Session[Layer01_Constants_Web.CnsSession_TmpObj] is Hashtable))
            { IsRedirect = true; }

            try
            { Ht = (Hashtable)this.Session[Layer01_Constants_Web.CnsSession_TmpObj]; }
            catch
            { return; }

            if (Ht != null)
            {
                this.Session.Remove(Layer01_Constants_Web.CnsSession_TmpObj);
                bool IsValid = false;
                try
                { IsValid = (bool)Ht[ContributorRegistration.CnsContributorRegistration]; }
                catch { }
                if (!IsValid)
                { IsRedirect = true; }
            }

            if (IsRedirect)
            { this.Response.Redirect("~/Page/Default.aspx"); }
        }
    }
}