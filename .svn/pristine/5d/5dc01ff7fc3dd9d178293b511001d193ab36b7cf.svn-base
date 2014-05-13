using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Layer01_Common;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer02_Objects;
using Layer02_Objects.Modules_Objects;
using Layer02_Objects.Modules_Objects.Exam;

namespace WebApplication_Exam.Page
{
    public partial class ExamFinish : ClsBaseMain_Page
    {
        #region _Variables

        ClsExam mObj_Exam;

        #endregion

        #region _Constructor

        public ExamFinish()
        { this.Page.Load += new EventHandler(Page_Load); }

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, false, Layer02_Objects._System.Layer02_Constants.eSystem_Modules.None, false, false, false);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);
            
            //[-]

            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);

            bool IsFinish = false;
            try
            { IsFinish = (bool)this.Session[Layer01_Constants_Web.CnsSession_Exam_IsFinish]; }
            catch { }

            if (!IsFinish)
            { this.Response.Redirect("~/Page/ExamStart.aspx"); }

            this.Session[Layer01_Constants_Web.CnsSession_Exam_IsFinish] = false;
            this.mObj_Exam = (ClsExam)this.Session[Layer01_Constants_Web.CnsSession_Exam_Obj];
            this.Lbl_ApplicantName.Text = (string)Do_Methods.IsNull(this.mObj_Exam.pObj_Applicant.pDr["Name"], "");
            this.Lbl_Score.Text = Do_Methods.IsNull(this.mObj_Exam.pDr_Exam["Score"], 0).ToString() + " out of " + Do_Methods.IsNull(this.mObj_Exam.pDr_Exam["TotalItems"], 0).ToString() + " Items.";
        }

        void Page_LoadComplete(object sender, EventArgs e)
        { this.pCurrentUser.Logoff(); }
        
        #endregion

    }
}