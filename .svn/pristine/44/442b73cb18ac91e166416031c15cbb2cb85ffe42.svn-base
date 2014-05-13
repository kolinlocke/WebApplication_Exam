using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik;
using Telerik.Web.UI;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web;
using Layer01_Common_Web.Common;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Objects;

namespace Layer01_Common_Web_Telerik.Common
{
    public class Methods_Web_Telerik
    {
        public enum eSelectorType
        { 
            None,
            Single,
            Multiple
        }
        
        public static void BindTelerikGrid(
            ref RadGrid Grid
            , DataTable Dt_Source
            , List<ClsBindGridColumn_Web_Telerik> List_Gc
            , string Key = ""
            , bool AllowSort = true
            , bool HasDelete = false
            , eSelectorType SelectorType = eSelectorType.None
            )
        {
            Grid.DataSource = Dt_Source;
            
            if (Grid.Columns.Count > 0)
            { Grid.Columns.Clear(); }

            Grid.AutoGenerateColumns = false;
            if (SelectorType != eSelectorType.None)
            { 
                Grid.ClientSettings.Selecting.AllowRowSelect = true;
                Grid.ClientSettings.Selecting.UseClientSelectColumnOnly = true; 
            }

            Grid.AllowMultiRowSelection = SelectorType == eSelectorType.Multiple;

            //Grid.ClientSettings.Resizing.AllowColumnResize = true;
            //Grid.ClientSettings.Resizing.AllowResizeToFit = true;
            
            TableItemStyle Tis  = null;
                       
            foreach (ClsBindGridColumn_Web_Telerik Inner_Gc in List_Gc)
            {
                if (!Inner_Gc.mIsVisible)
                { 
                    continue; 
                }

                switch (Inner_Gc.mFieldType)
                {
                    case Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Text:
                    {
                        GridBoundColumn Gc = new GridBoundColumn();
                        Grid.Columns.Add(Gc);

                        Gc.HeaderStyle.Width = Inner_Gc.mUnit_Width;
                        Gc.HeaderText = Inner_Gc.mFieldDesc;
                        Gc.DataField = Inner_Gc.mFieldName;
                        Gc.ReadOnly = !Inner_Gc.mEnabled;
                        Gc.DataFormatString = Inner_Gc.mDataFormat;

                        if (AllowSort)
                        { Gc.HeaderButtonType = GridHeaderButtonType.LinkButton; }
                        
                        Gc.SortExpression = Inner_Gc.mFieldName;
                        Tis = Gc.ItemStyle;
                        
                        break;
                    }
                    
                    case Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Checkbox:
                    {
                        GridCheckBoxColumn Gc = new GridCheckBoxColumn();
                        Grid.Columns.Add(Gc);

                        Gc.HeaderStyle.Width = Inner_Gc.mUnit_Width;
                        Gc.DataField = Inner_Gc.mFieldName;
                        Gc.HeaderText = Inner_Gc.mFieldDesc;
                        Gc.ReadOnly = !Inner_Gc.mEnabled;

                        if (AllowSort)
                        { Gc.HeaderButtonType = GridHeaderButtonType.LinkButton; }

                        Gc.SortExpression = Inner_Gc.mFieldName;
                        Tis = Gc.ItemStyle;
                            
                        break;
                    }
                    
                    case Layer01_Constants.eSystem_Lookup_FieldType.FieldType_DateTime:
                    {
                        GridDateTimeColumn Gc = new Telerik.Web.UI.GridDateTimeColumn();
                        Grid.Columns.Add(Gc);

                        Gc.HeaderStyle.Width = Inner_Gc.mUnit_Width;
                        Gc.DataField = Inner_Gc.mFieldName;
                        Gc.HeaderText = Inner_Gc.mFieldDesc;
                        Gc.ReadOnly = !Inner_Gc.mEnabled;
                        Gc.DataFormatString = Inner_Gc.mDataFormat;
                        

                        if (AllowSort)
                        { Gc.HeaderButtonType = GridHeaderButtonType.LinkButton; }

                        Gc.SortExpression = Inner_Gc.mFieldName;
                        Tis = Gc.ItemStyle;
                            
                        break;
                    }
                    
                    case Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button:
                    {
                        GridButtonColumn Gc = new GridButtonColumn();
                        Grid.Columns.Add(Gc);

                        Gc.HeaderStyle.Width = Inner_Gc.mUnit_Width;
                        Gc.HeaderText = Inner_Gc.mFieldDesc;
                        Gc.CommandName = Inner_Gc.mCommandName;

                        if (Inner_Gc.mFieldName == "")
                        { Gc.Text = Inner_Gc.mFieldText; }
                        else
                        { Gc.DataTextField = Inner_Gc.mFieldName; }
                        
                        switch (Inner_Gc.mButtonType)
                        {
                            case ButtonColumnType.PushButton:
                                Gc.ButtonType = Telerik.Web.UI.GridButtonColumnType.PushButton;
                                break;
                            case ButtonColumnType.LinkButton:
                                Gc.ButtonType = Telerik.Web.UI.GridButtonColumnType.LinkButton;
                                break;
                        }

                        Tis = Gc.ItemStyle;
                        break;                            
                    }
                    case Layer01_Constants.eSystem_Lookup_FieldType.FieldType_HyperLink:
                    {
                        GridHyperLinkColumn Gc = new GridHyperLinkColumn();
                        Grid.Columns.Add(Gc);

                        Gc.HeaderStyle.Width = Inner_Gc.mUnit_Width;
                        Gc.HeaderText = Inner_Gc.mFieldDesc;
                        Gc.Text = Inner_Gc.mFieldText;
                        Gc.DataNavigateUrlFormatString = Inner_Gc.mFieldNavigateUrl_Text;
                        Gc.DataNavigateUrlFields = new string[] { Inner_Gc.mFieldNavigateUrl_Field };

                        Tis = Gc.ItemStyle;
                        break;
                    }
                    default:
                    {
                        GridBoundColumn Gc = new GridBoundColumn();
                        Grid.Columns.Add(Gc);

                        Gc.HeaderStyle.Width = Inner_Gc.mUnit_Width;
                        Gc.HeaderText = Inner_Gc.mFieldDesc;
                        Gc.DataField = Inner_Gc.mFieldName;
                        Gc.ReadOnly = true;
                        Gc.DataFormatString = Inner_Gc.mDataFormat;

                        if (AllowSort)
                        { Gc.HeaderButtonType = GridHeaderButtonType.LinkButton; }
                        
                        Tis = Gc.ItemStyle;
                            
                        break;
                    }
                }

                //Tis.Width = Inner_Gc.mUnit_Width;
            }

            if (HasDelete) { }

            switch (SelectorType)
            {
                case eSelectorType.Single:
                case eSelectorType.Multiple:
                    {
                        GridClientSelectColumn Gc = new GridClientSelectColumn();
                        Grid.Columns.Insert(0, Gc);
                        Gc.UniqueName = "Select";
                        Gc.HeaderText = "";
                        Gc.HeaderStyle.Width = new Unit("40px");
                        break; 
                    }
            }

            if (Key != "")
            { Grid.MasterTableView.DataKeyNames = new string[] { Key }; }
            
            Grid.DataBind();
        }
    }
}