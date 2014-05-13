using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Layer01_Common;
//using Layer01_Common.Connection;
using Layer01_Common.Common;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer02_Objects;
//using Layer02_Objects.DataAccess;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using Layer02_Objects.Modules_Objects.Exam;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace WebApplication_Exam.Page
{
    public partial class ExamStart : ClsBaseMain_Page
    {
        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(false, true, Layer02_Objects._System.Layer02_Constants.eSystem_Modules.None, false, false, false);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {            
            base.Page_Load(sender, e);
            this.Btn_Start.Click += new EventHandler(Btn_Start_Click);
            this.Btn_Back.Click += new EventHandler(Btn_Back_Click);

            if (!this.IsPostBack) { this.SetupPage(); }
        }

        void Btn_Start_Click(object sender, EventArgs e)
        { this.Process_StartExam(); }

        void Btn_Back_Click(object sender, EventArgs e)
        { this.Response.Redirect(@"~/Page/Default.aspx"); }

        #endregion

        #region _Methods

        void SetupPage()
        {
            ClsBase Base = new ClsBase();
            DataTable Dt = Base.pDa.GetQuery("LookupCategory", "", "", "[Desc]");
            Methods_Web.BindCombo(ref this.Cbo_Category, Dt, "LookupCategoryID", "Desc");
        }

        void Process_StartExam()
        {
            Configuration Cfg = new Configuration();
            Cfg.LoadConfig(this.Master);

            Int64 NoItemsTotal = Cfg.pNoItemsTotal;
            Int64 NoItemsPerPage = Cfg.pNoItemsPerPage;

            ClsApplicant Obj_Applicant = new ClsApplicant();
            Obj_Applicant.Load();
            Obj_Applicant.pDr["Name"] = this.Txt_Name.Text;
            Obj_Applicant.pDr["Email"] = this.Txt_Email.Text;
            
            ClsExam Obj_Exam = new ClsExam();
            Obj_Exam.GenerateExam(NoItemsTotal, NoItemsPerPage, Obj_Applicant, Do_Methods.Convert_Int64(this.Cbo_Category.SelectedValue));
            this.Session[Layer01_Constants_Web.CnsSession_Exam_Obj] = Obj_Exam;
            this.Session[Layer01_Constants_Web.CnsSession_Exam_IsSession] = true;

            this.pCurrentUser.Logoff();

            this.Response.Redirect(@"~/Page/Exam.aspx");
        }

        bool Validation(ref System.Text.StringBuilder Sb_Msg)
        {
            WebControl Wc;
            bool IsValid = true;

            Wc = this.Txt_Name;
            Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (this.Txt_Name.Text.Trim() != "")
                , "Name is required." + "<br />");

            Wc = this.Txt_Email;
            Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (this.Txt_Email.Text.Trim() != "")
                , "Email is required." + "<br />");

            return IsValid;
        }

        #endregion
    }
}