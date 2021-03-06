﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web.Common;
using Layer01_Common_Web_Telerik.Common;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects._System;
using Telerik.Web.UI;

namespace WebApplication_Exam.UserControl
{
    public partial class Control_Selection : System.Web.UI.UserControl
    {
        #region _Events

        public delegate void Ds_SelectedSingle(string ControlID, Int64 KeyID);
        public delegate void Ds_SelectedMultiple(string ControlID, List<Int64> List_KeyID);

        public event Ds_SelectedSingle EvSelectedSingle;
        public event Ds_SelectedMultiple EvSelectedMultiple;

        #endregion

        #region _Variables

        const string CnsProperties = "CnsProperties";
        Control_Selection_Properties mProperties;

        ClsSysCurrentUser mCurrentUser;

        #endregion

        #region _Constructor

        public Control_Selection()
        {
            this.Load += new EventHandler(Page_Load);
            this.PreRender += new EventHandler(Page_PreRender);
        }

        public void Setup(ClsSysCurrentUser CurrentUser)
        {
            this.mProperties = new Control_Selection_Properties();
            this.ViewState[CnsProperties] = this.mProperties;

            this.mCurrentUser = CurrentUser;
            this.Grid_Selection.Setup(CurrentUser);
        }

        public void Setup_AddHandlers(
            WebControl Wc
            , RadAjaxPanel RadAjaxPanel_Target
            , ClsBindDefinition BindDefinition
            , bool IsMultipleSelect = false
            , string Window_Title = ""
            , double Window_Height = 450
            , double Window_Width = 500)
        {

            Control_Selection_DataSource So = new Control_Selection_DataSource();
            So.Source_ControlID = Wc.ID;
            So.RadAjaxPanel_TargetID = RadAjaxPanel_Target.ClientID;
            So.BindDefinition = BindDefinition;
            So.IsMultipleSelect = IsMultipleSelect;
            So.Window_Title = Window_Title == "" ? "Selection" : Window_Title;
            So.Window_Height = Window_Height < 450 ? 450 : Window_Height;
            So.Window_Width = Window_Width;

            this.mProperties.List_DataSource.Add(So);

            Wc.Attributes.Add(@"onclick", @"ShowSelection('" + Wc.ID + "','" + RadAjaxPanel_Target.ClientID + "', '" + So.Window_Title + "', " + So.Window_Height + ", " + So.Window_Width + "); return false;");
        }

        #endregion

        #region _EventHandlers

        void Page_Load(object sender, EventArgs e)
        {
            this.RadWindow_RadAjaxPanel_Selection.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(RadWindow_RadAjaxPanel_Selection_AjaxRequest);

            if (this.IsPostBack)
            {
                this.mCurrentUser = (ClsSysCurrentUser)this.Session[Layer01_Constants_Web.CnsSession_CurrentUser];
                this.mProperties = (Control_Selection_Properties)this.ViewState[CnsProperties];
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            { return; }

            if (this.RadWindow_Selection_Hid_ControlID.Value == "")
            { return; }

            Control_Selection_DataSource DataSource = this.mProperties.List_DataSource.Find(item => item.Source_ControlID == this.RadWindow_Selection_Hid_ControlID.Value);
            if (DataSource != null)
            {
                if (DataSource.IsMultipleSelect) 
                {
                    if (EvSelectedMultiple != null)
                    { EvSelectedMultiple(DataSource.Source_ControlID, this.Process_SelectedMultple(DataSource)); }
                }
                else 
                {
                    if (EvSelectedSingle != null)
                    { EvSelectedSingle(DataSource.Source_ControlID, this.Process_SelectedSingle(DataSource)); }
                }
            }
        }

        void RadWindow_RadAjaxPanel_Selection_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string[] Tmp;
            string Command = "";
            string ControlID = "";            
            try
            {
                Tmp = e.Argument.Split(',');
                Command = Tmp[0];
                ControlID = Tmp[1];
            }
            catch { }

            switch (Command)
            { 
                case "SelectStart":
                    this.BindGrid(ControlID);
                    break;
            }            
        }

        #endregion

        #region _Methods

        public void SetupPage()
        {
            this.mProperties = new Control_Selection_Properties();
            this.ViewState[CnsProperties] = this.mProperties;
        }

        void BindGrid(string ControlID)
        {
            Control_Selection_DataSource DataSource = this.mProperties.List_DataSource.Find(item => item.Source_ControlID == ControlID);
            DataTable Dt_DataSource = Do_Methods_Query.GetQuery(DataSource.BindDefinition.DataSourceName);

            this.Grid_Selection.pGrid.Height = new Unit((DataSource.Window_Height - 100).ToString() + "px");

            List<ClsBindGridColumn_Web_Telerik> List_Gcwt = new List<ClsBindGridColumn_Web_Telerik>();
            foreach (ClsBindGridColumn Inner_Gc in DataSource.BindDefinition.List_Gc)
            { List_Gcwt.Add((ClsBindGridColumn_Web_Telerik)Inner_Gc); }

            this.Grid_Selection.Setup_FromDataTable(
                this.mCurrentUser
                , Dt_DataSource
                , List_Gcwt
                , DataSource.BindDefinition.KeyName
                , true
                , false 
                , DataSource.IsMultipleSelect ? Methods_Web_Telerik.eSelectorType.Multiple : Methods_Web_Telerik.eSelectorType.Single
                , ""
                , false);
        }

        List<Int64> Process_SelectedMultple(Control_Selection_DataSource DataSource)
        {
            List<Int64> Selected = new List<Int64>();
            foreach (GridItem Gi in this.Grid_Selection.pGrid.SelectedItems)
            { Selected.Add((Int64)(Gi as GridDataItem).GetDataKeyValue(DataSource.BindDefinition.KeyName)); }

            return Selected;
        }

        Int64 Process_SelectedSingle(Control_Selection_DataSource DataSource)
        {
            Int64 SelectedID = 0;
            if (this.Grid_Selection.pGrid.SelectedItems.Count > 0)
            { SelectedID = Do_Methods.Convert_Int64((this.Grid_Selection.pGrid.SelectedItems[0] as GridDataItem).GetDataKeyValue(DataSource.BindDefinition.KeyName)); }

            return SelectedID;
        }

        #endregion
    }

    [Serializable()]
    public class Control_Selection_Properties
    { 
        public List<Control_Selection_DataSource> List_DataSource = new List<Control_Selection_DataSource>();        
    }

    [Serializable()]
    public class Control_Selection_DataSource
    {
        public string Source_ControlID;
        public string RadAjaxPanel_TargetID;
        public ClsBindDefinition BindDefinition;
        public bool IsMultipleSelect;
        public string Window_Title;
        public double Window_Height;
        public double Window_Width;
    }

}