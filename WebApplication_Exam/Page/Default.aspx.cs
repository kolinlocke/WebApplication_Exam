using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication_Exam;
using WebApplication_Exam.Base;

namespace WebApplication_Exam.Page
{
    public partial class Default : ClsBaseMain_Page 
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            this.Master.Setup(true);
            base.Page_Load(sender, e);
        }
    }
}