﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Layer01_Common;
using Layer01_Common.Common;
//using Layer01_Common.Connection;
using Layer01_Common.Objects;
using Layer02_Objects;
//using Layer02_Objects.Modules_Base;
//using Layer02_Objects.Modules_Base.Abstract;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace Layer02_Objects._System.CurrentUser
{
    public class ClsCurrentUserMethods_SqlServer : Interface_CurrentUserMethods
    {
        public string GetAdministratorPassword()
        {
            string Rv = "";
            ClsConnection_SqlServer Cn = new ClsConnection_SqlServer();
            try
            {
                Cn.Connect();

                List<Do_Constants.Str_Parameters> Sp = new List<Do_Constants.Str_Parameters>();
                Sp.Add(new Do_Constants.Str_Parameters("ParameterName", "Administrator_Password"));
                Sp.Add(new Do_Constants.Str_Parameters("ParameterValue", "Password"));
                Cn.ExecuteNonQuery( "usp_Require_System_Parameter", Sp);
                DataTable Dt = Cn.ExecuteQuery(@"Select dbo.udf_Get_System_Parameter('Administrator_Password')").Tables[0];

                if (Dt.Rows.Count > 0)
                { Rv = (string)Do_Methods.IsNull(Dt.Rows[0][0], ""); }
            }
            catch { }
            finally { Cn.Close(); }

            return Rv;
        }

        public bool CheckAccess(Layer02_Constants.eSystem_Modules System_ModulesID, Layer02_Constants.eAccessLib AccessType, Int64 UserID)
        {
            bool ReturnValue = false;

            DataTable Dt =
                new ClsDataAccess_SqlServer().GetQuery(
                "uvw_RecruitmentTestUser_Rights", "Count(1) As [Ct]"
                , "RecruitmentTestUserID = " + UserID + " And System_ModulesID = " + (long)System_ModulesID + " And System_Modules_AccessLibID = " + (long)AccessType + " And IsActive = 1 And IsAllowed = 1");

            if (Dt.Rows.Count > 0)
            {
                if (Do_Methods.Convert_Int64(Dt.Rows[0][0]) > 0)
                { ReturnValue = true; }
            }
            
            return ReturnValue;
        }
    }
}
