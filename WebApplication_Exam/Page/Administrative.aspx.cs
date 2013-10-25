using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer02_Objects._System;
using WebApplication_Exam.Base;

namespace WebApplication_Exam.Page
{
    public partial class Administrative : ClsBaseMain_Page
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            this.Master.Setup(false, true, Layer02_Constants.eSystem_Modules.Sys_Login, true);
            base.Page_Load(sender, e);
        }
    }
}