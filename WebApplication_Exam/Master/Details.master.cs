using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects_Framework.Common;
using Layer01_Common_Web.Common;
using Telerik.Web.UI;
using WebApplication_Exam.Base;

namespace WebApplication_Exam.Master
{
    public partial class Details : ClsBaseDetails_Master
    {
        #region _Variables

        //StringBuilder mSb_Script = new StringBuilder();

        #endregion

        #region _Constructor
        #endregion

        #region _EventHandlers

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.Btn_Back.Click += new EventHandler(Btn_Back_Click);
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            { this.SetupPage(); }
        }

        void Btn_Back_Click(object sender, EventArgs e)
        { this.Response.Redirect("~/Page/" + this.mProperties.ListPage); }

        #endregion

        #region _Methods

        void SetupPage()
        { this.Raise_SetupPage_ControlAttributes(); }

        void SetupPage_ControlAttributes(ref Control C)
        {
            WebControl Wc = null;
            if (C is WebControl)
            { Wc = (WebControl)C; }
            else
            {
                foreach (Control Ic in C.Controls)
                {
                    Control Inner_Ic = Ic;
                    this.SetupPage_ControlAttributes(ref Inner_Ic);
                }
                return;
            }

            if (Wc.Attributes[Layer01_Constants_Web.CnsCustomAttribute_Exempted] == "1")
            { return; }

            if (Wc is TextBox)
            {
                if ((Wc as TextBox).TextMode != TextBoxMode.MultiLine)
                { Wc.Attributes.Add("onkeypress", "return noenter(event)"); }

                if (Do_Methods.Convert_String(Wc.Attributes["onchange"], "") == "")
                { Wc.Attributes.Add("onchange", "RequireSave()"); }

                if (!(Wc as TextBox).ReadOnly) 
                { (Wc as TextBox).ReadOnly = this.pIsReadOnly; }
            }
            else if (Wc is RadEditor)
            {
                RadEditor Inner_Wc = (RadEditor)Wc;
                this.Page.ClientScript.RegisterStartupScript(typeof(string), this.ClientID + Wc.ClientID, "RequireSave_RadEditor('" + Wc.ClientID + "');", true);
                //this.pSb_Script.AppendLine("RequireSave_RadEditor('" + Wc.ClientID + "');");
                
                if (Inner_Wc.Enabled) 
                { Inner_Wc.Enabled = !this.pIsReadOnly; }
            }
            else if (
                Wc is DropDownList
                || Wc is RadioButton)
            {
                if (Do_Methods.Convert_String(Wc.Attributes["onchange"], "") == "")
                { Wc.Attributes.Add("onchange", "RequireSave()"); }
                Wc.Enabled = !this.pIsReadOnly;
            }
            else if (Wc is CheckBox)
            {
                if (Do_Methods.Convert_String(Wc.Attributes["onclick"], "") == "")
                { Wc.Attributes.Add("onclick", "RequireSave()"); }
            }
            else if (
                Wc is LinkButton
                || Wc is Button)
            {
                if (Wc.Enabled)
                { Wc.Enabled = !this.pIsReadOnly; }
            }
            else
            {
                foreach (Control Ic in C.Controls)
                {
                    Control Inner_Ic = Ic;
                    this.SetupPage_ControlAttributes(ref Inner_Ic);
                }
            }
        }

        public static void SetupPage_ControlAttributes(System.Web.UI.Page Page, Control C , bool IsReadOnly = false)
        {
            if (C is System.Web.UI.UserControl)
            {
                if ((C as System.Web.UI.UserControl).Attributes[Layer01_Constants_Web.CnsCustomAttribute_Exempted] == "1")
                { return; }
            }

            WebControl Wc = null;
            if (C is WebControl)
            { Wc = (WebControl)C; }
            else
            {
                foreach (Control Ic in C.Controls)
                {
                    Control Inner_Ic = Ic;
                    Details.SetupPage_ControlAttributes(Page, Inner_Ic, IsReadOnly);
                }
                return;
            }

            if (Wc.Attributes[Layer01_Constants_Web.CnsCustomAttribute_Exempted] == "1")
            { return; }

            if (Wc is TextBox)
            {
                if ((Wc as TextBox).TextMode != TextBoxMode.MultiLine)
                { Wc.Attributes.Add("onkeypress", "return noenter(event)"); }

                if (Do_Methods.Convert_String(Wc.Attributes["onchange"], "") == "")
                { Wc.Attributes.Add("onchange", "RequireSave()"); }

                if (!(Wc as TextBox).ReadOnly)
                { (Wc as TextBox).ReadOnly = IsReadOnly; }
            }
            else if (Wc is RadEditor)
            {
                RadEditor Inner_Wc = (RadEditor)Wc;
                Page.ClientScript.RegisterStartupScript(typeof(string), Page.ClientID + Wc.ClientID, "RequireSave_RadEditor('" + Wc.ClientID + "');", true);
                //this.pSb_Script.AppendLine("RequireSave_RadEditor('" + Wc.ClientID + "');");

                if (Inner_Wc.Enabled)
                { Inner_Wc.Enabled = !IsReadOnly; }
            }
            else if (
                Wc is DropDownList
                || Wc is RadioButton)
            {
                if (Do_Methods.Convert_String(Wc.Attributes["onchange"], "") == "")
                { Wc.Attributes.Add("onchange", "RequireSave()"); }
                Wc.Enabled = !IsReadOnly;
            }
            else if (Wc is CheckBox)
            {
                if (Do_Methods.Convert_String(Wc.Attributes["onclick"], "") == "")
                { Wc.Attributes.Add("onclick", "RequireSave()"); }
            }
            else if (
                Wc is LinkButton
                || Wc is Button)
            {
                if (Wc.Enabled)
                { Wc.Enabled = !IsReadOnly; }
            }
            else
            {
                foreach (Control Ic in C.Controls)
                {
                    Control Inner_Ic = Ic;
                    Details.SetupPage_ControlAttributes(Page, Inner_Ic, IsReadOnly);
                }
            }
        }

        public override void SetupPage_ControlAttributes()
        {
            Control Obj = this.Panel_Details;
            //this.SetupPage_ControlAttributes(ref Obj);
            Details.SetupPage_ControlAttributes(this.Page, Obj, this.pIsReadOnly);

            this.Btn_Save.Enabled = !this.pIsReadOnly;
            this.Btn_Delete.Enabled = !this.pIsReadOnly;

            this.Btn_Save.Attributes.Add("onclick", "TempReleaseSave();");
            this.Btn_Delete.Attributes.Add("onclick", "return CheckDelete();");
        }

        #endregion

        #region _Properties

        public override Button pBtn_Save
        {
            get { return this.Btn_Save; }
        }

        public override Button pBtn_Delete
        {
            get { return this.Btn_Delete; }
        }

        #endregion
    }
}