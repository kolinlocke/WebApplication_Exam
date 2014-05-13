using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects_Framework;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.Common;
using DataObjects_Framework.Connection;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Objects;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects;
using Layer02_Objects._System;
using WebApplication_Exam;
using WebApplication_Exam._Base;

namespace WebApplication_Exam.Master
{
    public partial class List : ClsBaseList_Master
    {
        #region _EventHandlers

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (this.pIsPageLoaded) { return; }
            base.Page_Load(sender, e);

            this.Btn_New.Click += new EventHandler(Btn_New_Click);
            this.GridList.pGrid.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler(pGrid_ItemCommand);
            this.FilterList.EvFiltered += new UserControl.Control_Filter.DsFiltered(FilterList_EvFiltered);
        }

        void Btn_New_Click(object sender, EventArgs e)
        { this.Response.Redirect(this.pProperties.DetailsPage); }

        void FilterList_EvFiltered(QueryCondition Qc)
        { this.GridList.RebindGrid(Qc); }

        void pGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    {
                        Int64 KeyID = this.GridList.GetKey(Do_Methods.Convert_Int32(e.Item.ItemIndex));
                        this.Raise_DeleteRecord(KeyID);
                        break;
                    }
            }
        }

        #endregion

        #region _Methods

        public override void SetupPage()
        {
            base.SetupPage();

            //[-]

            this.Btn_New.Visible = this.mRights_IsNew;

            //[-]

            List<ClsBindGridColumn_Web_Telerik> List_Gcwt = new List<ClsBindGridColumn_Web_Telerik>();
            foreach (ClsBindGridColumn Inner_Gc in this.pBindDefinition.List_Gc)
            { List_Gcwt.Add((ClsBindGridColumn_Web_Telerik)Inner_Gc); }

            if (this.pProperties.IsSelectDetails && this.mRights_IsSelect)
            {
                ClsBindGridColumn_Web_Telerik Gc = new ClsBindGridColumn_Web_Telerik("", "", new Unit("50px"), "", Layer01_Common.Common.Layer01_Constants.eSystem_Lookup_FieldType.FieldType_HyperLink);
                Gc.mFieldText = ">>";
                Gc.mFieldNavigateUrl_Text = this.pProperties.DetailsPage + "?ID={0}";
                Gc.mFieldNavigateUrl_Field = this.pBindDefinition.KeyName;
                List_Gcwt.Insert(0, Gc);
            }

            if (this.pProperties.IsDelete && (this.pCurrentUser.CheckAccess(this.pSystem_ModulesID, Layer02_Constants.eAccessLib.eAccessLib_Delete)))
            {
                ClsBindGridColumn_Web_Telerik Gc = new ClsBindGridColumn_Web_Telerik("", "", new Unit("100px"), "", Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Button);
                Gc.mCommandName = "Delete";
                Gc.mFieldText = "Delete";
                List_Gcwt.Add(Gc);
            }

            this.GridList.pGrid.ClientSettings.ClientEvents.OnCommand = "Grid_OnCommand";

            switch (this.pProperties.DataSourceType)
            {
                case eDataSourceType.FromBase:
                    {
                        this.FilterList.Setup(this.pCurrentUser, this.pBindDefinition.List_Gc, this.pObj_Base.List_Empty(), this.GridList.pAjaxPanel);
                        this.GridList.Setup_FromBase(
                            this.pCurrentUser
                            , this.pObj_Base
                            , List_Gcwt
                            , this.pBindDefinition.KeyName
                            , this.pBindDefinition.AllowSort
                            , this.pBindDefinition.AllowPaging);
                        break;
                    }
                case eDataSourceType.FromDataTable:
                    {
                        this.FilterList.Setup(this.pCurrentUser, this.pBindDefinition.List_Gc, this.pDt_DataSource.Clone(), this.GridList.pAjaxPanel);
                        this.GridList.Setup_FromDataTable(
                            this.pCurrentUser
                            , this.pDt_DataSource
                            , List_Gcwt
                            , this.pBindDefinition.KeyName
                            , this.pBindDefinition.AllowSort
                            , this.pBindDefinition.AllowPaging);
                        break;
                    }
            }
        }
        
        #endregion

        #region _Properties

        public override UserControl.Control_Filter pFilterList
        {
            get { return this.FilterList; }
        }

        public override UserControl.Control_GridList pGridList
        {
            get { return this.GridList; }
        }

        public override Button pBtn_New
        {
            get { return this.Btn_New; }
        }

        #endregion        
    }
}