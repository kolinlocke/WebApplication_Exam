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
    public partial class Control_GridList : System.Web.UI.UserControl
    {
        #region _Variables
        
        ClsSysCurrentUser mCurrentUser;
        ClsBase mBase;
        DataTable mDataSource;
        string mTableName;

        const string CnsBase = "CnsBase";
        const string CnsDataSource = "CnsDataSource";
        const string CnsTableName = "CnsTableName";

        const string CnsProperties = "CnsProperties";
        Control_GridList_Properties mProperties;

        const string CnsObjID = "CnsObjID";
        string mObjID;

        const string CnsState_GridList = "CnsState_GridList";
        Control_GridList_State mState;

        public enum eSourceType
        { 
            Base = 1,
            DataTable = 2,
            Table = 3
        }

        #endregion

        #region _Constructor

        public Control_GridList()
        { this.Load += new EventHandler(Page_Load); }

        public void Setup_FromBase(
            ClsSysCurrentUser CurrentUser
            , ClsBase Base
            , List<ClsBindGridColumn_Web_Telerik> BindDefinition
            , string Key = ""
            , bool AllowSort = false
            , bool AllowPaging = false
            , Methods_Web_Telerik.eSelectorType SelectorType = Methods_Web_Telerik.eSelectorType.None
            , string ModuleName = ""
            , bool IsPersistent = true)
        {
            this.mBase = Base;
            this.Setup(CurrentUser, BindDefinition, Key, AllowSort, AllowPaging, eSourceType.Base, SelectorType, ModuleName, IsPersistent, true);
            this.Session[CnsBase + this.mObjID] = this.mBase;
        }

        public void Setup_FromDataTable(
            ClsSysCurrentUser CurrentUser
            , DataTable DataSource
            , List<ClsBindGridColumn_Web_Telerik> BindDefinition
            , string Key = ""
            , bool AllowSort = false
            , bool AllowPaging = false
            , Methods_Web_Telerik.eSelectorType SelectorType = Methods_Web_Telerik.eSelectorType.None
            , string ModuleName = ""
            , bool IsPersistent = true)
        {
            this.mDataSource = DataSource;
            this.Setup(CurrentUser, BindDefinition, Key, AllowSort, AllowPaging, eSourceType.DataTable, SelectorType, ModuleName, IsPersistent, true);
            this.Session[CnsDataSource + this.mObjID] = this.mDataSource;
        }

        public void Setup_FromTable(
            ClsSysCurrentUser CurrentUser
            , string TableName
            , List<ClsBindGridColumn_Web_Telerik> BindDefinition
            , string Key = ""
            , bool AllowSort = false
            , bool AllowPaging = false
            , Methods_Web_Telerik.eSelectorType SelectorType = Methods_Web_Telerik.eSelectorType.None
            , string ModuleName = ""
            , bool IsPersistent = true
            )
        {
            this.mTableName = TableName;
            this.Setup(CurrentUser, BindDefinition, Key, AllowSort, AllowPaging, eSourceType.Table, SelectorType, ModuleName, IsPersistent, true);
            this.Session[CnsTableName + this.mObjID] = this.mTableName;
        }

        public void Setup(
            ClsSysCurrentUser CurrentUser
            , List<ClsBindGridColumn_Web_Telerik> BindDefinition = null
            , string Key = ""
            , bool AllowSort = false
            , bool AllowPaging = false
            , eSourceType SourceType =  eSourceType.Base
            , Methods_Web_Telerik.eSelectorType SelectorType = Methods_Web_Telerik.eSelectorType.None
            , string ModuleName = ""
            , bool IsPersistent = true
            , bool IsBind = false)
        {
            if (SelectorType != Methods_Web_Telerik.eSelectorType.None && Key.Trim() == "")
            { throw new Exception("Key is required when using selectors."); }

            if (this.mObjID == null)
            {
                this.mCurrentUser = CurrentUser;
                this.mObjID = this.mCurrentUser.GetNewPageObjectID();
                this.ViewState[CnsObjID] = this.mObjID;
            }

            this.mProperties = new Control_GridList_Properties();
            this.ViewState[CnsProperties] = this.mProperties;

            this.mProperties.BindDefinition = BindDefinition;
            this.mProperties.Key = Key;
            this.mProperties.AllowSort = AllowSort;
            this.mProperties.AllowPaging = AllowPaging;
            //this.mProperties.IsRequery = IsRequery;
            this.mProperties.SourceType = SourceType;
            this.mProperties.SelectorType = SelectorType;
            this.mProperties.ModuleName = ModuleName != "" ? CnsState_GridList + ModuleName : CnsState_GridList + this.Page.Request.Url.AbsolutePath;
            this.mProperties.IsPersistent = IsPersistent;
            
            if (this.mProperties.IsPersistent)
            {
                this.mState = (Control_GridList_State)this.Session[this.mProperties.ModuleName];
                if (this.mState == null)
                {
                    this.mState = new Control_GridList_State();
                    this.Session[this.mProperties.ModuleName] = this.mState;
                }
            }
            else
            { 
                this.mState = new Control_GridList_State();
                this.ViewState[CnsState_GridList] = this.mState;
            }

            if (IsBind)
            { this.BindGrid(); }
        }

        #endregion

        #region _EventHandlers

        void Page_Load(object sender, EventArgs e)
        {
            this.Grid.SortCommand += new GridSortCommandEventHandler(Grid_SortCommand);
            this.Grid.PageSizeChanged += new GridPageSizeChangedEventHandler(Grid_PageSizeChanged);
            this.Grid.PageIndexChanged += new GridPageChangedEventHandler(Grid_PageIndexChanged);
            //this.Grid.ItemDataBound += new GridItemEventHandler(Grid_ItemDataBound);
            //this.Grid.PreRender += new EventHandler(Grid_PreRender);

            //this.SetupPage_Js();

            if (this.IsPostBack)
            {
                this.mObjID = (string)this.ViewState[CnsObjID];
                this.mProperties = (Control_GridList_Properties)this.ViewState[CnsProperties];

                if (this.mProperties.IsPersistent) { this.mState = (Control_GridList_State)this.Session[this.mProperties.ModuleName]; }
                else { this.mState = (Control_GridList_State)this.ViewState[CnsState_GridList]; }

                /*
                if (this.mProperties.IsRequery)
                { this.mBase = (ClsBase)this.Session[CnsBase + this.mObjID]; }
                else
                { this.mDataSource = (DataTable)this.Session[CnsDataSource + this.mObjID]; }
                */

                switch (this.mProperties.SourceType)
                { 
                    case eSourceType.Base:
                        this.mBase = (ClsBase)this.Session[CnsBase + this.mObjID];
                        break;
                    case eSourceType.DataTable:
                        this.mDataSource = (DataTable)this.Session[CnsDataSource + this.mObjID];
                        break;
                    case eSourceType.Table:
                        this.mTableName = (string)this.Session[CnsTableName + this.mObjID];
                        break;
                }
            }
        }

        void Grid_PreRender(object sender, EventArgs e)
        {
            if (this.mProperties.SelectorType == Methods_Web_Telerik.eSelectorType.None)
            { return; }

            foreach (GridDataItem Gi in this.Grid.Items)
            {
                Int64 Key = Do_Methods.Convert_Int64(Gi.GetDataKeyValue(this.mProperties.Key), 0);
                DataRow[] ArrDr = this.mProperties.Dt_Selected.Select("Key = " + Key);
                DataRow Dr = null;

                if (ArrDr.Length > 0)
                { Dr = ArrDr[0]; }
                else
                { continue; }
                
                Gi.Selected = (bool)Do_Methods.IsNull(Dr["IsSelected"], false);
            }
            this.Grid.Rebind();
        }

        void Grid_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.StoreSelection();

            DataTable Dt = null;
            string Sort = e.SortExpression + " " + (e.NewSortOrder == GridSortOrder.Ascending ? "" : "Desc");

            /*
            if (this.mProperties.IsRequery)
            { Dt = this.mBase.List(this.mState.Qc, Sort, this.Grid.PageSize, this.Grid.CurrentPageIndex + 1); }
            else
            { 
                Dt = this.mDataSource;
                if (this.mState.Qc != null)
                {
                    try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                    catch { }
                }
                else
                { Dt.DefaultView.RowFilter = ""; }
            }
            */

            switch (this.mProperties.SourceType)
            {
                case eSourceType.Base:
                    {
                        Dt = this.mBase.List(this.mState.Qc, Sort, this.Grid.PageSize, this.Grid.CurrentPageIndex + 1);
                        break;
                    }
                case eSourceType.DataTable:
                    {
                        Dt = this.mDataSource;
                        if (this.mState.Qc != null)
                        {
                            try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                            catch { }
                        }
                        else
                        { Dt.DefaultView.RowFilter = ""; }
                        break;
                    }
                case eSourceType.Table:
                    {
                        Dt = new ClsBase().pDa.List(this.mTableName, this.mState.Qc, Sort, this.Grid.PageSize, this.Grid.CurrentPageIndex + 1);
                        break;
                    }
            }

            Methods_Web.ClearHTMLTags(Dt);

            this.Grid.DataSource = Dt;
            this.Grid.Rebind();

            List<string> List_Sort = new List<string>();
            List_Sort.Add(Sort);
            this.SaveGridState(this.Grid.PageSize, this.Grid.CurrentPageIndex, List_Sort);
        }

        void Grid_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.StoreSelection();

            DataTable Dt = null;
            
            /*
            if (this.mProperties.IsRequery)
            {
                StringBuilder Sb_Sort = new StringBuilder();

                foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
                { Sb_Sort.Append(Se.FieldName + " " + Se.SortOrderAsString()); }
                Sort = Sb_Sort.ToString();

                Dt = this.mBase.List(this.mState.Qc, Sb_Sort.ToString(), e.NewPageSize, this.Grid.CurrentPageIndex + 1);
                this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc));
            }
            else
            { 
                Dt = this.mDataSource;
                if (this.mState.Qc != null)
                {
                    try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                    catch { }
                }
                else
                { Dt.DefaultView.RowFilter = ""; }
            }
            */

            switch (this.mProperties.SourceType)
            {
                case eSourceType.Base:
                case eSourceType.Table:
                    {
                        StringBuilder Sb_Sort = new StringBuilder();
                        foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
                        { Sb_Sort.Append(Se.FieldName + " " + Se.SortOrderAsString()); }
                        
                        switch (this.mProperties.SourceType)
                        {
                            case eSourceType.Base:
                                {
                                    Dt = this.mBase.List(this.mState.Qc, Sb_Sort.ToString(), e.NewPageSize, this.Grid.CurrentPageIndex + 1);
                                    this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc));
                                    break;
                                }
                            case eSourceType.Table:
                                {
                                    Dt = new ClsBase().pDa.List(this.mTableName, this.mState.Qc, Sb_Sort.ToString(), e.NewPageSize, this.Grid.CurrentPageIndex + 1);
                                    this.Grid.VirtualItemCount = Convert.ToInt32(new ClsBase().pDa.List_Count(this.mTableName, this.mState.Qc));
                                    break;
                                }                                
                        }
                        break;
                    }
                case eSourceType.DataTable:
                    {
                        Dt = this.mDataSource;
                        if (this.mState.Qc != null)
                        {
                            try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                            catch { }
                        }
                        else
                        { Dt.DefaultView.RowFilter = ""; }
                        break;
                    }
            }

            Methods_Web.ClearHTMLTags(Dt);

            this.Grid.DataSource = Dt;
            this.Grid.Rebind();

            List<string> List_Sort = new List<string>();
            foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
            { List_Sort.Add(Se.ToString()); }

            this.SaveGridState(e.NewPageSize, this.Grid.CurrentPageIndex, List_Sort);
        }

        void Grid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.StoreSelection();

            DataTable Dt = null;
            
            /*
            if (this.mProperties.IsRequery)
            {
                StringBuilder Sb_Sort = new StringBuilder();

                foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
                { Sb_Sort.Append(Se.FieldName + " " + Se.SortOrderAsString()); }
                Sort = Sb_Sort.ToString();

                Dt = this.mBase.List(this.mState.Qc, Sb_Sort.ToString(), this.Grid.PageSize, e.NewPageIndex + 1);
                this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc));
            }
            else
            { 
                Dt = this.mDataSource;
                if (this.mState.Qc != null)
                {
                    try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                    catch { }
                }
                else
                { Dt.DefaultView.RowFilter = ""; }
            }
            */

            switch (this.mProperties.SourceType)
            {
                case eSourceType.Base:
                case eSourceType.Table:
                    {
                        StringBuilder Sb_Sort = new StringBuilder();
                        foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
                        { Sb_Sort.Append(Se.FieldName + " " + Se.SortOrderAsString()); }
                        
                        switch (this.mProperties.SourceType)
                        {
                            case eSourceType.Base:
                                {
                                    Dt = this.mBase.List(this.mState.Qc, Sb_Sort.ToString(), this.Grid.PageSize, e.NewPageIndex + 1);
                                    this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc));
                                    break;
                                }
                            case eSourceType.Table:
                                {
                                    Dt = new ClsBase().pDa.List(this.mTableName, this.mState.Qc, Sb_Sort.ToString(), this.Grid.PageSize, e.NewPageIndex + 1);
                                    this.Grid.VirtualItemCount = Convert.ToInt32(new ClsBase().pDa.List_Count(this.mTableName, this.mState.Qc));
                                    break;
                                }
                        }
                        break;
                    }
                case eSourceType.DataTable:
                    {
                        Dt = this.mDataSource;
                        if (this.mState.Qc != null)
                        {
                            try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                            catch { }
                        }
                        else
                        { Dt.DefaultView.RowFilter = ""; }
                        break;
                    }
            }

            Methods_Web.ClearHTMLTags(Dt);

            this.Grid.DataSource = Dt;
            this.Grid.Rebind();

            List<string> List_Sort = new List<string>();
            foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
            { List_Sort.Add(Se.ToString()); }

            this.SaveGridState(this.Grid.PageSize, e.NewPageIndex, List_Sort);
        }

        #endregion

        #region _Methods

        void SetupPage_Js()
        {
            StringBuilder Sb = new StringBuilder();
            Sb.AppendLine(@"function RadAjaxPanel1_OnResponseEnd_" + this.ClientID + @"(Sender, Args) {");
            Sb.AppendLine(@"var TxtSelected = document.getElementById('" + this.Hid_Selected.ClientID + @"').value;");
            Sb.AppendLine(@"var ArrSelected = TxtSelected.split(',');");
            Sb.AppendLine(@"var Table = $find('" + this.Grid.ClientID + @"').get_masterTableView();");
            Sb.AppendLine(@"var Items = Table.get_dataItems();");
            Sb.AppendLine(@"for (Selected in ArrSelected) {");
            Sb.AppendLine(@"for(I in Items){");
            Sb.AppendLine(@"var Key = I.getDataKeyValue('TmpKey');");
            Sb.AppendLine(@"if (Key == Selected) {");
            Sb.AppendLine(@"I.set_selected(true);");
            Sb.AppendLine(@"break;");
            Sb.AppendLine(@"}");
            Sb.AppendLine(@"}");
            Sb.AppendLine(@"}");
            Sb.AppendLine(@"}");

            this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), this.ClientID, Sb.ToString(), true);
            this.RadAjaxPanel1.ClientEvents.OnResponseEnd = @"function RadAjaxPanel1_OnResponseEnd_" + this.ClientID;
        }

        void BindGrid()
        {
            DataTable Dt = null;

            /*
            if (this.mProperties.IsRequery)
            { Dt = this.mBase.List(this.mState.Qc, this.mState.Sort, this.mState.Top, this.mState.Page + 1); }
            else
            { 
                Dt = this.mDataSource;
                if (this.mState.Qc != null)
                {
                    try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                    catch { }
                }
                else
                { Dt.DefaultView.RowFilter = ""; }
            }
            */

            switch (this.mProperties.SourceType)
            {
                case eSourceType.Base:
                    {
                        Dt = this.mBase.List(this.mState.Qc, this.mState.Sort, this.mState.Top, this.mState.Page + 1);
                        break;
                    }
                case eSourceType.Table:
                    {
                        Dt = new ClsBase().pDa.List(this.mTableName, this.mState.Qc, this.mState.Sort, this.mState.Top, this.mState.Page + 1);
                        break;
                    }
                case eSourceType.DataTable:
                    {
                        Dt = this.mDataSource;
                        if (this.mState.Qc != null)
                        {
                            try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                            catch { }
                        }
                        else
                        { Dt.DefaultView.RowFilter = ""; }
                        break;
                    }
            }

            Methods_Web.ClearHTMLTags(Dt);

            this.Grid.AllowPaging = this.mProperties.AllowPaging;

            if (this.mProperties.AllowPaging)
            {
                switch (this.mProperties.SourceType)
                {
                    case eSourceType.Base:
                    case eSourceType.Table:
                        {
                            this.Grid.AllowCustomPaging = this.mProperties.AllowPaging;

                            if (this.mProperties.SourceType == eSourceType.Base)
                            { this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc)); }
                            else
                            { this.Grid.VirtualItemCount = Convert.ToInt32(new ClsBase().pDa.List_Count(this.mTableName, this.mState.Qc)); }

                            this.Grid.CurrentPageIndex = this.mState.Page;
                            this.Grid.PageSize = this.mState.Top == 0 ? this.Grid.PageSize : this.mState.Top;
                            break;
                        }
                }
            }
            else
            { 
                //this.Grid.ClientSettings.Scrolling.AllowScroll = true;
                //this.Grid.ClientSettings.Scrolling.UseStaticHeaders = true;
                //this.Grid.MasterTableView.TableLayout = GridTableLayout.Fixed;
                //this.Grid.MasterTableView.Width = new Unit("100%");
            }

            if (this.mState.List_Sort != null)
            {
                this.Grid.MasterTableView.SortExpressions.Clear();
                foreach (string Se in this.mState.List_Sort)
                { this.Grid.MasterTableView.SortExpressions.AddSortExpression(Se); }
            }

            Methods_Web_Telerik.BindTelerikGrid(
                ref this.Grid
                , Dt
                , this.mProperties.BindDefinition
                , this.mProperties.Key
                , this.mProperties.AllowSort
                , false
                , this.mProperties.SelectorType);
        }

        void StoreSelection()
        {
            /*
            foreach (GridDataItem Gi in this.Grid.Items)
            {

                Int64 Key = Methods.Convert_Int64(Gi.GetDataKeyValue(this.mInfo.mKey), 0);
                DataRow[] ArrDr = this.mInfo.mDt_Selected.Select("Key = " + Key);
                DataRow Dr = null;
                if (ArrDr.Length > 0)
                { Dr = ArrDr[0]; }
                else
                { 
                    Dr = this.mInfo.mDt_Selected.NewRow();
                    this.mInfo.mDt_Selected.Rows.Add(Dr);
                }

                Dr["Key"] = Key;
                Dr["IsSelected"] = Gi.Selected;
            }

            StringBuilder Sb = new StringBuilder();
            string Comma = "";
            bool IsStart = false;
            DataRow[] ArrDr_Selected = this.mInfo.mDt_Selected.Select("IsSelected = 1");
            foreach (DataRow Dr in ArrDr_Selected)
            {
                Sb.Append(Comma + Methods.Convert_Int64(Dr["Key"], 0).ToString());
                if (!IsStart)
                { 
                    Comma = ",";
                    IsStart = true;
                }
            }
            this.Hid_Selected.Value = Sb.ToString();
            */
        }

        public void RebindGrid(ClsQueryCondition Qc = null)
        {
            this.mState.Qc = Qc;

            DataTable Dt = null;

            /*
            if (this.mProperties.IsRequery)
            {
                StringBuilder Sb_Sort = new StringBuilder();
                foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
                { Sb_Sort.Append(Se.FieldName + " " + Se.SortOrderAsString()); }

                Dt = this.mBase.List(this.mState.Qc, Sb_Sort.ToString(), this.Grid.PageSize, this.Grid.CurrentPageIndex + 1);
                this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc));
                this.Grid.PagerStyle.Mode = (GridPagerMode)PagerMode.NextPrev;
            }
            else
            { 
                Dt = this.mDataSource;
                if (this.mState.Qc != null)
                {
                    try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                    catch { }
                }
                else
                { Dt.DefaultView.RowFilter = ""; }
            }
            */

            switch (this.mProperties.SourceType)
            {
                case eSourceType.Base:
                case eSourceType.Table:
                    {
                        StringBuilder Sb_Sort = new StringBuilder();
                        foreach (GridSortExpression Se in this.Grid.MasterTableView.SortExpressions)
                        { Sb_Sort.Append(Se.FieldName + " " + Se.SortOrderAsString()); }
                        
                        switch (this.mProperties.SourceType)
                        {
                            case eSourceType.Base:
                                {
                                    this.Grid.VirtualItemCount = Convert.ToInt32(this.mBase.List_Count(this.mState.Qc));

                                    try
                                    {
                                        if (this.Grid.VirtualItemCount / this.Grid.PageSize < (this.Grid.CurrentPageIndex + 1))
                                        { this.Grid.CurrentPageIndex = 0; }
                                    }
                                    catch { this.Grid.CurrentPageIndex = 0; }

                                    Dt = this.mBase.List(this.mState.Qc, Sb_Sort.ToString(), this.Grid.PageSize, this.Grid.CurrentPageIndex + 1);
                                    
                                    break;
                                }
                            case eSourceType.Table:
                                {
                                    this.Grid.VirtualItemCount = Convert.ToInt32(new ClsBase().pDa.List_Count(this.mTableName, this.mState.Qc));

                                    try
                                    {
                                        if (this.Grid.VirtualItemCount / this.Grid.PageSize < (this.Grid.CurrentPageIndex + 1))
                                        { this.Grid.CurrentPageIndex = 0; }
                                    }
                                    catch { this.Grid.CurrentPageIndex = 0; }

                                    Dt = new ClsBase().pDa.List(this.mTableName, this.mState.Qc, Sb_Sort.ToString(), this.Grid.CurrentPageIndex + 1);
                                    
                                    break;
                                }
                        }

                        break;
                    }
                case eSourceType.DataTable:
                    {
                        Dt = this.mDataSource;
                        if (this.mState.Qc != null)
                        {
                            try { Dt.DefaultView.RowFilter = this.mState.Qc.GetQueryCondition_String(); }
                            catch { }
                        }
                        else
                        { Dt.DefaultView.RowFilter = ""; }
                        break;
                    }
            }

            Methods_Web.ClearHTMLTags(Dt);

            this.Grid.DataSource = Dt;
            this.Grid.Rebind();
        }

        public Int64 GetKey(Int32 ItemIndex)
        {
            Int64 KeyID = 0;
            try { KeyID = Do_Methods.Convert_Int64(this.Grid.MasterTableView.Items[ItemIndex].GetDataKeyValue(this.mProperties.Key).ToString()); }
            catch { }
            
            return KeyID;
        }

        void SaveGridState(Int32 Top, Int32 Page, List<string> List_Sort)
        {
            this.mState.Page = Page;
            this.mState.Top = Top;
            this.mState.List_Sort = List_Sort;
        }

        #endregion

        #region _Properties

        public RadGrid pGrid
        {
            get { return this.Grid; }
        }

        public RadAjaxPanel pAjaxPanel
        {
            get { return this.RadAjaxPanel1; }
        }

        #endregion
    }

    [Serializable()]
    public class Control_GridList_Properties
    {
        public List<ClsBindGridColumn_Web_Telerik> BindDefinition;
        public string Key;
        public bool AllowSort;
        public bool AllowPaging;
        //public bool IsRequery;
        public Control_GridList.eSourceType SourceType;
        public DataTable Dt_Selected;
        public Methods_Web_Telerik.eSelectorType SelectorType;
        public string ModuleName;
        public bool IsPersistent = false;
    }

    [Serializable()]
    public class Control_GridList_State
    {
        public Int32 Top = 0;
        public Int32 Page = 0;
        public List<string> List_Sort;
        public ClsQueryCondition Qc;
        
        public string Sort 
        {
            get 
            {
                if (this.List_Sort == null)
                { return ""; }

                string Sort = "";
                string Comma = "";
                bool IsStart = false;                
                foreach (string Se in this.List_Sort)
                {
                    Sort += Comma + Se.ToString();
                    if (!IsStart)
                    { 
                        IsStart = true;
                        Comma = ",";
                    }
                }
                return Sort;
            }
        }
    }
}