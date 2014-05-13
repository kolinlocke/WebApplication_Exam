using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
//using Layer02_Objects.Modules_Base.Abstract;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace WebApplication_Exam.Page
{
    public partial class ErrorPage : ClsBaseMain_Page
    {
        protected override void Page_Init(object sender, EventArgs e)
        { this.Setup(false, false); }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Exception Ex = Global_Web.gEx;
            Global_Web.gEx = null;
            if (Ex == null)
            { this.Response.Redirect("~/Default.aspx"); }

            if (Ex is ClsCustomException)
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("An error has occured with the following messages:" + "<br /><br />");
                Sb.Append(Ex.Message + "<br /><br />");
                Sb.Append("Please inform your System Administrator regarding this error.");
                this.Show_EventMsg(Sb.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
            }
            else if (Ex is HttpException)
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("You are lost. Click the link to continue." + "<br /><br />");
                this.Show_EventMsg(Sb.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
            }
            else
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("An unspecified error has occured." + "<br /><br />");
                Sb.Append("Please inform your System Administrator regarding this error.");
                this.Show_EventMsg(Sb.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
            }
        }
    }
}