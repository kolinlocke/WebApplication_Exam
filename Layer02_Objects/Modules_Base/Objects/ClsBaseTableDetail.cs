using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Connection;
using Layer02_Objects;
using Layer02_Objects._System;
using Layer02_Objects.DataAccess;
using Layer02_Objects.Modules_Base;
using Layer02_Objects.Modules_Base.Abstract;
using Layer02_Objects.Modules_Base.Objects;

namespace Layer02_Objects.Modules_Base.Objects
{
    public class ClsBaseTableDetail
    {
        #region _Variables

        string mHeaderName;
        string mTableName;
        string mViewName;
        List<string> mList_Key = new List<string>();
        ClsBase mObj_Base;

        string mOtherLoadCondition;
        DataTable mDt;

        Interface_DataAccess mDa;

        #endregion

        #region _Constructor

        private ClsBaseTableDetail()
        { }

        public ClsBaseTableDetail(
            ClsBase pObj_Base
            , string pHeaderName
            , string pTableName
            , string pViewName = ""
            , string pOtherLoadCondition = "")
        {
            if (pViewName == "") pViewName = pTableName;

            this.mHeaderName = pHeaderName;
            this.mTableName = pTableName;
            this.mViewName = pViewName;
            this.mOtherLoadCondition = pOtherLoadCondition;
            this.mObj_Base = pObj_Base;

            DataTable Dt_Def = Methods_Query.GetTableDef(this.mTableName);
            DataRow[] ArrDr = Dt_Def.Select("IsPk = 1");
            foreach (DataRow Dr in ArrDr)
            { this.mList_Key.Add((string)Dr["ColumnName"]); }

            this.mDa = pObj_Base.pDa;
        }
        
        #endregion

        #region _Methods

        /*
        public void Load(Interface_DataAccess Da, string Condition)
        {
            string OtherCondition = "";
            if (this.mOtherLoadCondition != "") OtherCondition = " And " + this.mOtherLoadCondition;

            DataTable Dt;

            if (Condition == "") Dt = this.mDa.GetQuery(Da.Connection, this.mViewName, "*", "1 = 0");
            else Dt = this.mDa.GetQuery(Da.Connection, this.mViewName, "*", Condition + OtherCondition);

            this.mDt = Dt;
        }
        */

        /*
        public void Load(string Condition)
        {
            try
            {
                this.mDa.Connect();
                this.Load(this.mDa, Condition);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { this.mDa.Close(); }
        }
        */

        public void Load(Interface_DataAccess Da, ClsKeys Keys)
        { this.mDt = Da.Load_TableDetails(this.mViewName, Keys, this.mOtherLoadCondition); }

        public void Save(Interface_DataAccess Da)
        {
            DataRow[] ArrDr = this.mDt.Select("", "", DataViewRowState.CurrentRows);
            foreach (DataRow Dr in ArrDr)
            {
                if (Dr.RowState == DataRowState.Added || Dr.RowState == DataRowState.Modified)
                {
                    foreach (string Header_Key in this.mObj_Base.pHeader_Key)
                    {
                        Int64 Inner_ID = (Int64)Methods.IsNull(this.mObj_Base.pDr[Header_Key], 0);
                        Dr[Header_Key] = Inner_ID;
                    }
                    Da.SaveDataRow(Dr, this.mTableName);
                }
            }

            ArrDr = this.mDt.Select("", "", DataViewRowState.Deleted);
            foreach (DataRow Dr in ArrDr)
            {
                DataRow Nr = Dr.Table.NewRow();
                foreach (DataColumn Dc in Dr.Table.Columns)
                { 
                    Nr[Dc.ColumnName] = Dr[Dc.ColumnName, DataRowVersion.Original]; 
                }

                bool IsPKComplete = true;
                foreach (string Key in this.mList_Key)
                {
                    if (Information.IsDBNull(Dr[Key]))
                    {
                        IsPKComplete = false;
                        break;
                    }
                }

                if (IsPKComplete)
                {
                    //Nr["IsDeleted"] = true;
                    Da.SaveDataRow(Dr, this.mTableName, "", true);
                }
            }
        }

        #endregion

        #region _Properties

        public string pTableName
        {
            get
            { return this.mTableName; }
        }

        public DataTable pDt
        {
            get
            { return this.mDt; }
            set
            { this.mDt = value; }
        }

        #endregion
    }
}
