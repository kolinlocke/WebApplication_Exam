using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DataObjects_Framework.Common;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Objects;
using Layer01_Common.Common;
using Layer01_Common_Web.Common;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects._System;
using Layer02_Objects.Modules_Objects;
using Telerik.Web.UI;
using WebApplication_Exam._Base;

namespace WebApplication_Exam.Page
{
    public partial class User_Details : ClsBaseDetails_Page
    {
        #region _Variables

        ClsUser mObj;
        
        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Master.Setup(Layer02_Constants.eSystem_Modules.User, new ClsUser(this.pCurrentUser)); 
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);

            this.Grid_Details.pGrid.ItemCommand += new GridCommandEventHandler(pGrid_ItemCommand);
            
            this.mObj = (ClsUser)this.pObj_Base;

            if (!this.IsPostBack) { this.SetupPage(); }
        }

        void pGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "IsActive":
                    {
                        Int64 KeyID = this.Grid_Details.GetKey(Do_Methods.Convert_Int32(e.CommandArgument));
                        DataRow[] ArrDr = this.mObj.pDt_Rights.Select("TmpKey = " + KeyID, "", DataViewRowState.CurrentRows);
                        if (ArrDr.Length > 0)
                        { this.Process_IsActive(ArrDr[0], !Do_Methods.Convert_Boolean(ArrDr[0]["IsActive"])); }
                        break;
                    }
            }

            this.Grid_Details.RebindGrid();
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            this.Lbl_Title.Text = this.pIsNew ? "New User" : "Edit User Details: " + this.mObj.pDr["Name"].ToString();

            this.Txt_UserName.Enabled = this.pIsNew;
            this.AddValidator();

            //[-]

            this.Txt_UserName.Text = Do_Methods.Convert_String(this.mObj.pDr["Name"], "");
            this.Txt_Password.Text = Do_Methods.Convert_String(this.mObj.pDr["Password"], "");
            this.Hid_Username.Value = Do_Methods.Convert_String(this.mObj.pDr["Name"], "");

            this.BindGrid();
        }

        void BindGrid()
        {
            DataTable Dt_Rights = this.mObj.pDt_Rights;
            Dt_Rights.Columns.Add("IsActive_Desc", typeof(string));
            foreach (DataRow Dr in Dt_Rights.Rows)
            { this.Process_IsActive(Dr, Do_Methods.Convert_Boolean(Dr["IsActive"])); }

            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Rights_Name", "Module", 150));
            
            if (this.pIsReadOnly)
            { List_Gc.Add(new ClsBindGridColumn_Web_Telerik("IsActive_Desc", "Is Set?", 100)); }
            else
            { List_Gc.Add(new ClsBindGridColumn_Web_Telerik("IsActive_Desc", "Is Set?", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button) { mCommandName = "IsActive" }); }

            this.Grid_Details.pGrid.ClientSettings.ClientEvents.OnCommand = "Grid_OnCommand";
            this.Grid_Details.Setup_FromDataTable(this.pCurrentUser, this.mObj.pDt_Rights, List_Gc, "TmpKey");
        }

        void Process_IsActive(DataRow Dr, bool IsTrue)
        {
            Dr["IsActive"] = IsTrue;
            Dr["IsActive_Desc"] = IsTrue ? "Set" : "Not Set";
        }

        protected override void Save()
        {
            StringBuilder Sb_ErrorMsg = new StringBuilder();
            if (!this.Save_Validate(ref Sb_ErrorMsg))
            {
                this.Show_EventMsg(Sb_ErrorMsg.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
                return;
            }

            //[-]

            ClsUser Obj = this.mObj;
            Obj.pDr["Name"] = this.Txt_UserName.Text;

            if (this.Txt_Password.Text != "") { Obj.pDr["Password"] = this.Txt_Password.Text; }

            base.Save();
        }

        bool Save_Validate(ref System.Text.StringBuilder Sb_Msg)
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
                , "User Name already exists. Please choose a different User Name." + "<br />"))
            {
                QueryCondition Qc = Do_Methods.CreateQueryCondition();
                Qc.Add("Name", " = " + this.Txt_UserName.Text, typeof(string).ToString());
                Qc.Add("IsDeleted", "0", typeof(bool).ToString(), "0");
                Qc.Add("RecruitmentTestUserID", "<> " + this.mObj.pID.ToString(), typeof(Int64).ToString(), "0");

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
            
            return IsValid;
        }

        private void AddValidator()
        {
            RequiredFieldValidator Req_Username = new RequiredFieldValidator();
            Req_Username.ID = "Req_Username";
            Req_Username.ErrorMessage = "Username is required.";
            Req_Username.ControlToValidate = "Txt_Username";
            Req_Username.Text = "*";
            Req_Username.ValidationGroup = "UserDetails";
            Label1.Controls.Add(Req_Username);

            if (this.pIsNew)
            {
                RequiredFieldValidator Req_Password = new RequiredFieldValidator();
                Req_Password.ID = "Req_Password";
                Req_Password.ErrorMessage = "Password is required.";
                Req_Password.ControlToValidate = "Txt_Password";
                Req_Password.Text = "*";
                Req_Password.ValidationGroup = "UserDetails";
                Label2.Controls.Add(Req_Password);

                RequiredFieldValidator Req_ConfirmPassword = new RequiredFieldValidator();
                Req_ConfirmPassword.ID = "Req_ConfirmPassword";
                Req_ConfirmPassword.ErrorMessage = "Confirm Password field is required.";
                Req_ConfirmPassword.ControlToValidate = "Txt_ConfirmPassword";
                Req_ConfirmPassword.Text = "*";
                Req_ConfirmPassword.ValidationGroup = "UserDetails";
                Label3.Controls.Add(Req_ConfirmPassword);
            }

            CompareValidator ComparePass = new CompareValidator();
            ComparePass.ID = "CompareID";
            ComparePass.ErrorMessage = "Password and Confirm Password fields must match.";
            ComparePass.ControlToCompare = "Txt_ConfirmPassword";
            ComparePass.ControlToValidate = "Txt_Password";
            ComparePass.Text = "*";
            ComparePass.ValidationGroup = "UserDetails";
            Label2.Controls.Add(ComparePass);
        }

        #endregion
    }
}