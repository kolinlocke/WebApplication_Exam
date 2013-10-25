using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
//using Layer02_Objects.DataAccess;
using Layer02_Objects._System;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Objects;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using WebApplication_Exam;
using WebApplication_Exam.Base;
using Telerik.Web.UI;

namespace WebApplication_Exam.Page
{
    public partial class Rights_Details : ClsBaseDetails_Page
    {
        #region _Variables

        ClsRights mObj;

        #endregion

        #region _EventHandlers

        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            this.Setup(Layer02_Constants.eSystem_Modules.Rights, new ClsRights(this.pCurrentUser));
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);

            this.mObj = (ClsRights)this.pObj_Base;

            this.Grid_Details.pGrid.ItemCommand += new GridCommandEventHandler(pGrid_ItemCommand);
            this.Grid_Details.pAjaxPanel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(pAjaxPanel_AjaxRequest);
            this.Filter_Details.EvFiltered += new UserControl.Control_Filter.DsFiltered(Filter_Details_EvFiltered);

            if (!this.IsPostBack) { this.SetupPage(); }
        }

        void Filter_Details_EvFiltered(ClsQueryCondition Qc)
        { this.Grid_Details.RebindGrid(Qc); }

        void pGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "IsAllowed":
                    {
                        Int64 KeyID = this.Grid_Details.GetKey(Do_Methods.Convert_Int32(e.CommandArgument));
                        DataRow[] ArrDr = this.mObj.pDt_Details.Select("TmpKey = " + KeyID, "", DataViewRowState.CurrentRows);
                        if (ArrDr.Length > 0)
                        { this.Process_IsAllowed(ArrDr[0], !Do_Methods.Convert_Boolean(ArrDr[0]["IsAllowed"])); }
                        break;
                    }
            }

            this.Grid_Details.RebindGrid(this.Filter_Details.pQc);
        }

        void pAjaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string Command = "";
            bool IsTrue = false;

            try
            {
                string[] Tmp = e.Argument.Split(',');
                Command = Tmp[0];
                IsTrue = Do_Methods.Convert_Boolean(Tmp[1]);
            }
            catch { }

            if (Command != "SetIsAllowed")
            { return; }

            DataRow[] ArrDr = this.mObj.pDt_Details.Select(this.mObj.pDt_Details.DefaultView.RowFilter, "", DataViewRowState.CurrentRows);
            foreach (DataRow Dr in ArrDr)
            { this.Process_IsAllowed(Dr, IsTrue); }

            this.Grid_Details.RebindGrid(this.Filter_Details.pQc);
        }

        #endregion

        #region _Methods

        void SetupPage()
        {
            this.Lbl_Title.Text = this.pIsNew ? "New Role" : "Role Details: " + Do_Methods.Convert_String(this.mObj.pDr["Name"]);
            this.Txt_Name.Text = Do_Methods.Convert_String(this.mObj.pDr["Name"]);
            this.BindGrid();
        }

        void BindGrid()
        {
            DataTable Dt_Details = this.mObj.pDt_Details;
            Dt_Details.Columns.Add("IsAllowed_Desc", typeof(string));
            foreach (DataRow Dr in Dt_Details.Rows)
            { this.Process_IsAllowed(Dr, Do_Methods.Convert_Boolean(Dr["IsAllowed"])); }

            List<ClsBindGridColumn_Web_Telerik> List_Gc = new List<ClsBindGridColumn_Web_Telerik>();
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Module_Parent_Name", "Parent Module", 100));
            List_Gc.Add( new ClsBindGridColumn_Web_Telerik("Module_Name", "Module", 150));
            List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Access_Desc", "Access Type", 100));

            if (this.pIsReadOnly)
            { List_Gc.Add(new ClsBindGridColumn_Web_Telerik("IsAllowed_Desc", "Is Allowed?", 100)); }
            else
            { List_Gc.Add(new ClsBindGridColumn_Web_Telerik("IsAllowed_Desc", "Is Allowed?", 100, "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button) { mCommandName = "IsAllowed" }); }

            this.Filter_Details.Setup(this.pCurrentUser, new List<ClsBindGridColumn>(List_Gc), this.mObj.pDt_Details.Clone(), this.Grid_Details.pAjaxPanel, "", false);

            this.Grid_Details.pGrid.ClientSettings.ClientEvents.OnCommand = "Grid_OnCommand";
            this.Grid_Details.Setup_FromDataTable(this.pCurrentUser, this.mObj.pDt_Details, List_Gc, "TmpKey", false, false, Methods_Web_Telerik.eSelectorType.None, "", false);
        }

        void Process_IsAllowed(DataRow Dr, bool IsTrue)
        {
            Dr["IsAllowed"] = IsTrue;
            Dr["IsAllowed_Desc"] = IsTrue ? "Allowed" : "Not Allowed"; 
        }

        protected override void Save()
        {
            StringBuilder Sb_ErrorMsg = new StringBuilder();
            if (!this.Validation(ref Sb_ErrorMsg))
            {
                this.Show_EventMsg(Sb_ErrorMsg.ToString(), ClsBaseMain_Master.eStatus.Event_Error);
                return;
            }

            this.mObj.pDr["Name"] = this.Txt_Name.Text;
            base.Save();
        }

        bool Validation(ref StringBuilder Sb_Msg)
        {
            if (this.pIsReadOnly)
            {
                Sb_Msg.Append("Record is read only." + "<br /");
                return false;
            }

            WebControl Wc;
            bool IsValid = true;

            Wc = this.Txt_Name;
            if (Methods_Web.ControlValidation(
                ref Sb_Msg
                , ref Wc
                , ref IsValid
                , Layer01_Constants_Web.CnsCssTextbox
                , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                , (this.Txt_Name.Text != "")
                , "Role Name is required." + "<br />"))
            {
                ClsQueryCondition Qc = this.mObj.pDa.CreateQueryCondition();
                Qc.Add("Name", "= " + this.Txt_Name.Text, typeof(string).ToString());
                Qc.Add("IsDeleted", "0", typeof(bool).ToString(), "0");
                Qc.Add("RecruitmentTestUserID", "<> " + this.mObj.pID.ToString(), typeof(Int64).ToString(), "0");
                
                DataTable Dt = this.mObj.pDa.GetQuery("RecruitmentTestUser", "", Qc);
                bool IsValid_Ex = !(Dt.Rows.Count > 0);

                Wc = this.Txt_Name;
                Methods_Web.ControlValidation(
                    ref Sb_Msg
                    , ref Wc
                    , ref IsValid
                    , Layer01_Constants_Web.CnsCssTextbox
                    , Layer01_Constants_Web.CnsCssTextbox_ValidateHighlight
                    , IsValid_Ex
                    , "Role Name must be unique." + "<br />");
            }

            return IsValid;
        }

        #endregion
    }
}