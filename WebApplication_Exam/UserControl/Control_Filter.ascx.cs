using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects_Framework;
using DataObjects_Framework.Base;
using DataObjects_Framework.Common;
using DataObjects_Framework.Connection;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Objects;
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
using Layer02_Objects.Modules_Objects;
using Telerik.Web.UI;
using WebApplication_Exam.Base;

namespace WebApplication_Exam.UserControl
{
    public partial class Control_Filter : System.Web.UI.UserControl
    {
        #region _Events

        public delegate void DsFiltered(ClsQueryCondition Qc);

        public event DsFiltered EvFiltered;

        #endregion

        #region _Variables

        const string CnsProperties = "CnsProperties";
        Control_Filter_Properties mProperties;

        const string CnsState_Filter = "CnsState_Filter";
        Control_Filter_State mState;

        #endregion

        #region _Constructor

        public Control_Filter()
        { 
            this.Load += new EventHandler(Page_Load);
            this.PreRender += new EventHandler(Page_PreRender);
            this.Attributes.Add(Layer01_Constants_Web.CnsCustomAttribute_Exempted, "1");
        }

        public void Setup(
            ClsSysCurrentUser CurrentUser
            , List<ClsBindGridColumn> BindDefinition
            , DataTable Dt_Source
            , RadAjaxPanel RadAjaxPanel_Target
            , string ModuleName = ""
            , bool IsPersistent = true)
        {
            this.mProperties = new Control_Filter_Properties();
            this.mProperties.BindDefinition = BindDefinition;
            this.mProperties.Dt_Filter = new DataTable();
            this.mProperties.RadAjaxPanel_Target_ClientID = RadAjaxPanel_Target.ClientID;
            this.mProperties.ModuleName = ModuleName != "" ? CnsState_Filter +  ModuleName : CnsState_Filter + this.Page.Request.Url.AbsolutePath;
            this.mProperties.IsPersistent = IsPersistent;
            this.ViewState[CnsProperties] = this.mProperties;

            //[-]

            this.mProperties.Dt_Filter.Columns.Add("Desc", typeof(string));
            this.mProperties.Dt_Filter.Columns.Add("Field", typeof(string));
            this.mProperties.Dt_Filter.Columns.Add("DataType", typeof(string));

            foreach (ClsBindGridColumn Gc in this.mProperties.BindDefinition)
            {
                if (Gc.mIsFilter && (Gc.mFieldName != ""))
                {
                    try
                    {
                        Do_Methods.AddDataRow(
                        ref this.mProperties.Dt_Filter
                        , new string[] { "Field", "Desc", "DataType" }
                        , new object[] { Gc.mFieldName, Gc.mFieldDesc, Dt_Source.Columns[Gc.mFieldName].DataType.ToString() });
                    }
                    catch { }
                }
            }

            Methods_Web.BindCombo(ref this.Cbo_Filter, this.mProperties.Dt_Filter, "Field", "Desc");

            //[-]

            if (this.mProperties.IsPersistent)
            {
                this.mState = (Control_Filter_State)this.Session[this.mProperties.ModuleName];
                if (this.mState == null)
                {
                    this.mState = new Control_Filter_State();
                    this.Session[this.mProperties.ModuleName] = this.mState;
                }
            }
            else
            { 
                this.mState = new Control_Filter_State();
                this.ViewState[CnsState_Filter] = this.mState;
            }

            //[-]

            this.UpdateFilterLabel();
        }

        #endregion

        #region _EventHandlers

        void Page_Load(object sender, EventArgs e)
        {
            this.RadAjaxPanel_FilterLabel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(RadAjaxPanel_FilterLabel_AjaxRequest);

            if (!this.IsPostBack)
            { this.SetupPage_Attributes(); }
            else
            {
                this.mProperties = (Control_Filter_Properties)this.ViewState[CnsProperties];
                if (this.mProperties.IsPersistent) { this.mState = (Control_Filter_State)this.Session[this.mProperties.ModuleName]; }
                else { this.mState = (Control_Filter_State)this.ViewState[CnsState_Filter]; }
            }

            this.SetupPage_AjaxPanelEventHandler();
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Hid_IsFilter.Value == "1")
            {
                if (EvFiltered != null)
                { EvFiltered(this.pQc); }
            }
        }

        void RadAjaxPanel_FilterLabel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string[] Tmp;
            string ControlID = "";
            string FilterText = "";
            try
            {
                Tmp = e.Argument.Split(',');
                ControlID = Tmp[0];
                FilterText = Tmp[1];
            }
            catch { }

            if (ControlID == this.Txt_Filter.ID
                || ControlID == this.Btn_FilterNew.ID)
            {
                this.FilterClear();
                this.FilterAdd(FilterText);
            }
            else if (ControlID == this.Btn_FilterAdd.ID)
            { this.FilterAdd(FilterText); }
            else if (ControlID == this.Btn_FilterClear.ID)
            { this.FilterClear(); }

            this.UpdateFilterLabel();
        }

        void RadAjaxPanel_Target_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument != "Filter")
            { return; }

            if (EvFiltered != null)
            { EvFiltered(this.pQc); }
        }
        
        #endregion

        #region _Methods

        void SetupPage_AjaxPanelEventHandler()
        {
            StringBuilder Sb_Js = new StringBuilder();
            Sb_Js.AppendLine(@"$('" + this.Txt_Filter.ClientID + @"').keypress(");
            Sb_Js.AppendLine(@"function (event) {");
            Sb_Js.AppendLine(@"if (event.which == 13) {");
            Sb_Js.AppendLine(@"ProcessFilter_" + this.ClientID + @"('" + this.Txt_Filter.ClientID + "');");
            Sb_Js.AppendLine(@"}})");

            Sb_Js.AppendLine(@"function ProcessFilter_" + this.ClientID + @"(ControlID) {");
            Sb_Js.AppendLine(@"var FilterText = document.getElementById('" + this.Txt_Filter.ClientID + @"').value;");
            Sb_Js.AppendLine(@"var Panel = $find('" + this.RadAjaxPanel_FilterLabel.ClientID + @"');");
            Sb_Js.AppendLine(@"Panel.ajaxRequest(ControlID + ',' + FilterText);");
            Sb_Js.AppendLine(@"}");

            Sb_Js.AppendLine(@"function RaiseEvent_Filter_" + this.ClientID + @"() {");
            Sb_Js.AppendLine(@"var Hid = document.getElementById('" + this.Hid_IsFilter.ClientID + "');");
            Sb_Js.AppendLine(@"Hid.value = '1';");
            Sb_Js.AppendLine(@"var Panel = $find('" + this.mProperties.RadAjaxPanel_Target_ClientID + @"');");
            Sb_Js.AppendLine(@"Panel.ajaxRequest('Filter');");
            Sb_Js.AppendLine(@"ClearFlags_" + this.ClientID + @"();");
            Sb_Js.AppendLine(@"}");

            Sb_Js.AppendLine(@"function RadAjaxPanel_FilterLabel_OnResponseEnd_" + this.ClientID + @"(Sender, Args) {");
            Sb_Js.AppendLine(@"RaiseEvent_Filter_" + this.ClientID + @"();");
            Sb_Js.AppendLine(@"}");

            Sb_Js.AppendLine(@"function ClearFlags_" + this.ClientID + @"() {");
            Sb_Js.AppendLine(@"var Hid = document.getElementById('" + this.Hid_IsFilter.ClientID + "');");
            Sb_Js.AppendLine(@"Hid.value = '';");
            Sb_Js.AppendLine(@"}");

            this.RadAjaxPanel_FilterLabel.ClientEvents.OnResponseEnd = "RadAjaxPanel_FilterLabel_OnResponseEnd_" + this.ClientID;
            
            this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), this.ClientID, Sb_Js.ToString(), true);
        }

        void SetupPage_Attributes()
        {
            this.Btn_FilterAdd.Attributes.Add("onclick", @"ProcessFilter_" + this.ClientID + @"('" + this.Btn_FilterAdd.ID + @"'); return false;");
            this.Btn_FilterClear.Attributes.Add("onclick", @"ProcessFilter_" + this.ClientID + @"('" + this.Btn_FilterClear.ID + @"'); return false;");
            this.Btn_FilterNew.Attributes.Add("onclick", @"ProcessFilter_" + this.ClientID + @"('" + this.Btn_FilterNew.ID + @"'); return false;");
        }

        void FilterAdd(string FilterText)
        {
            if (this.mState.Qc == null)
            { this.mState.Qc = (new ClsBase()).pDa.CreateQueryCondition(); }

            DataRow[] ArrDr = this.mProperties.Dt_Filter.Select(@"Field = '" + this.Cbo_Filter.SelectedValue + @"'");
            if (ArrDr.Length > 0)
            {
                this.mState.Qc.Add(
                    Do_Methods.Convert_String(ArrDr[0]["Field"])
                    , FilterText
                    , Do_Methods.Convert_String(ArrDr[0]["DataType"]));
                this.mState.FilterDesc += this.Cbo_Filter.SelectedItem.Text + @" by """ + FilterText + @"""<br />";
            }
        }

        void FilterClear()
        {
            this.mState.Qc = null;
            this.mState.FilterDesc = "";
        }

        void UpdateFilterLabel()
        { this.Lbl_Filter.Text = this.mState.FilterDesc == "" ? "" : @"Filtered By:<br />" + this.mState.FilterDesc; }

        #endregion

        #region _Properties

        public ClsQueryCondition pQc
        {
            get { return this.mState.Qc; }
        }

        public RadAjaxPanel pAjaxPanel
        {
            get { return this.RadAjaxPanel_FilterLabel; }
        }

        #endregion
    }

    [Serializable()]
    public class Control_Filter_Properties
    {
        public List<ClsBindGridColumn> BindDefinition;
        public DataTable Dt_Filter;
        public string RadAjaxPanel_Target_ClientID;
        public string ModuleName;
        public bool IsPersistent = false;
    }

    [Serializable()]
    public class Control_Filter_State
    {
        public ClsQueryCondition Qc = null;
        public string FilterDesc = "";
    }

}