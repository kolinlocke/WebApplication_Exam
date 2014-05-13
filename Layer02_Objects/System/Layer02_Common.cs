using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataObjects_Framework.Common;
using DataObjects_Framework.Connection;
using Layer01_Common.Common;
using DataObjects_Framework.Objects;
using DataObjects_Framework.PreparedQueryObjects;

namespace Layer02_Objects._System
{
    public class Layer02_Common
    {
        public static DateTime GetServerDate(Connection_SqlServer Da)
        {
            DataTable Dt = Da.ExecuteQuery("Select GetDate() As ServerDate").Tables[0];
            if (Dt.Rows.Count > 0) return (DateTime)Dt.Rows[0][0];
            else return DateTime.Now;
        }

        public static DateTime GetServerDate()
        {
            Connection_SqlServer Da = new Connection_SqlServer();
            try
            {
                Da.Connect();
                return GetServerDate(Da);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { Da.Close(); }
        }

        public static string GetSeriesNo(string Name)
        {
            string Rv = "";
            DataTable Dt;
            string TableName;
            string FieldName;
            string Prefix;
            Int32 Digits;

            Dt = Do_Methods_Query.GetQuery("System_DocumentSeries", "", "ModuleName = '" + Name + "'");
            if (Dt.Rows.Count > 0)
            {
                TableName = (string)Do_Methods.IsNull(Dt.Rows[0]["TableName"], "");
                FieldName = (string)Do_Methods.IsNull(Dt.Rows[0]["FieldName"], "");
                Prefix = (string)Do_Methods.IsNull(Dt.Rows[0]["Prefix"], "");
                Digits = (Int32)Do_Methods.IsNull(Dt.Rows[0]["Digits"], "");
            }
            else
            { return Rv; }

            List<QueryParameter> Sp = new List<QueryParameter>();
            Sp.Add(new QueryParameter("@TableName", TableName));
            Sp.Add(new QueryParameter("@FieldName", FieldName));
            Sp.Add(new QueryParameter("@Prefix", Prefix));
            Sp.Add(new QueryParameter("@Digits", Digits));

            Dt = Do_Methods_Query.ExecuteQuery("usp_GetSeriesNo", Sp).Tables[0];
            if (Dt.Rows.Count > 0)
            { Rv = (string)Dt.Rows[0][0]; }

            return Rv;
        }

        public static bool CheckSeriesDuplicate(
            string TableName
            , string SeriesField
            , Keys Keys
            , string SeriesNo)
        {
            bool Rv = false;
            DataTable Dt;

            System.Text.StringBuilder Sb_Query_Key = new StringBuilder();
            string Query_Key = "";
            string Query_And = "";

            foreach (string Inner_Key in Keys.pName)
            {
                Sb_Query_Key.Append(Query_And + " " + Inner_Key + " = " + Keys[Inner_Key]);
                Query_And = " And ";
            }

            Query_Key = " 1 = 1 ";
            if (Sb_Query_Key.ToString() != "")
            { Query_Key = "(Not (" + Sb_Query_Key.ToString() + "))"; }

            Dt = Do_Methods_Query.GetQuery(
                "[" + TableName + "]"
                , "Count(1) As [Ct]"
                , Query_Key + " And " + SeriesField + " = '" + SeriesNo + "'");
            if (Dt.Rows.Count > 0)
            {
                if ((Int32)Dt.Rows[0][0] > 0)
                { Rv = true; }
            }

            //True means duplicates have been found
            return Rv;
        }

        public static void AddSelected(
            DataTable Dt_Target
            , List<long> Selected_IDs
            , string Selected_DataSourceName
            , string Selected_KeyName
            , string Target_Key
            , bool HasTmpKey
            , List<Layer01_Constants.Str_AddSelectedFields> List_Selected_Fields = null
            , List<Layer01_Constants.Str_AddSelectedFieldsDefault> List_Selected_FieldsDefault = null)
        {
            if (Selected_IDs == null)
            { return; }

            if (Selected_IDs.Count == 0)
            { return; }

            PreparedQuery Pq = Do_Methods.CreatePreparedQuery();
            Pq.pQuery = @"Select * From " + Selected_DataSourceName + @" Where " + Selected_KeyName + @" = @ID";
            Pq.Add_Parameter("ID", Do_Constants.eParameterType.Long);
            Pq.Prepare();

            foreach (Int64 Selected_ID in Selected_IDs)
            {
                Pq.pParameters.GetParameter("ID").Value = Selected_ID;
                DataTable Dt_Selected = Pq.ExecuteQuery().Tables[0];
                if (Dt_Selected.Rows.Count > 0)
                {
                    DataRow Dr_Selected = Dt_Selected.Rows[0];
                    DataRow[] ArrDr;
                    DataRow Dr_Target = null;

                    ArrDr = Dt_Target.Select(Target_Key + @" = " + Do_Methods.Convert_Int64(Dr_Selected[Selected_KeyName]));
                    if (ArrDr.Length > 0)
                    { Dr_Target = ArrDr[0]; }
                    else
                    {
                        Dr_Target = Dt_Target.NewRow();
                        Dt_Target.Rows.Add(Dr_Target);

                        Dr_Target[Target_Key] = Dr_Selected[Selected_KeyName];

                        if (HasTmpKey)
                        {
                            Int64 Ct = 0;
                            ArrDr = Dt_Target.Select("", "TmpKey Desc", DataViewRowState.CurrentRows);
                            if (ArrDr.Length > 0)
                            { Ct = Do_Methods.Convert_Int64(ArrDr[0]["TmpKey"]); }
                            Ct++;

                            Dr_Target["TmpKey"] = Ct;
                            Dr_Target["Item_Style"] = "";
                        }
                    }

                    if (List_Selected_Fields != null)
                    {
                        foreach (Layer01_Constants.Str_AddSelectedFields Selected_Field in List_Selected_Fields)
                        { Dr_Target[Selected_Field.Field_Target] = Dr_Selected[Selected_Field.Field_Selected]; }
                    }

                    if (List_Selected_FieldsDefault != null)
                    {
                        foreach (Layer01_Constants.Str_AddSelectedFieldsDefault Selected_FieldDefault in List_Selected_FieldsDefault)
                        { Dr_Target[Selected_FieldDefault.Field_Target] = Selected_FieldDefault.Value; }
                    }

                }
            }

        }
    }
}
