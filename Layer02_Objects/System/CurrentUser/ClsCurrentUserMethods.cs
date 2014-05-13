using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects_Framework.Common;
using DataObjects_Framework.Objects;
using System.Data;

namespace Layer02_Objects._System.CurrentUser
{
    public class ClsCurrentUserMethods : Interface_CurrentUserMethods
    {
        public String GetAdministratorPassword()
        {
            String ReturnValue = "";

            List<QueryParameter> Params = new List<QueryParameter>();
            Params.Add(new QueryParameter("ParameterName", "Administrator_Password"));
            Params.Add(new QueryParameter("ParameterValue", "Password"));
            Do_Methods_Query.ExecuteNonQuery("usp_Require_System_Parameter", Params);

            DataTable Dt = Do_Methods_Query.ExecuteQuery(@"Select dbo.udf_Get_System_Parameter('Administrator_Password')").Tables[0];
            if (Dt.Rows.Count > 0)
            { ReturnValue = Do_Methods.Convert_Value<String>(Dt.Rows[0][0], ""); }

            return ReturnValue;
        }

        public Boolean CheckAccess(Layer02_Constants.eSystem_Modules System_ModulesID, Layer02_Constants.eAccessLib AccessType, long UserID)
        {
            Boolean ReturnValue = false;
            DataTable Dt = Do_Methods_Query.GetQuery(
                "uvw_RecruitmentTestUser_Rights"
                , "Count(1) As [Ct]"
                , "RecruitmentTestUserID = " + UserID + " And System_ModulesID = " + (Int64)System_ModulesID + " And System_Modules_AccessLibID = " + (Int64)AccessType + " And IsActive = 1 And IsAllowed = 1");

            if (Dt.Rows.Count > 0)
            {
                if (Do_Methods.Convert_Value<Int64>(Dt.Rows[0][0]) > 0)
                { ReturnValue = true; }
            }

            return ReturnValue;
        }
    }
}
