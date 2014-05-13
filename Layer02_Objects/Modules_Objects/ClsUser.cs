using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer02_Objects;
using Layer02_Objects._System;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace Layer02_Objects.Modules_Objects
{
    public class ClsUser: Base
    {
        #region _Variables

        ClsSysCurrentUser mCurrentUser;

        #endregion

        #region _Constructor

        public ClsUser(ClsSysCurrentUser pCurrentUser = null)
        { 
            this.Setup("RecruitmentTestUser", "uvw_RecruitmentTestUser");
            this.Setup_AddTableDetail("RecruitmentTestUser_Rights", "", "1 = 0");
            this.mCurrentUser = pCurrentUser;
        }

        #endregion

        #region _Methods

        public override DataTable List(string Condition = "", string Sort = "")
        { return this.List((QueryCondition)null, Sort); }

        public override DataTable List(QueryCondition Condition, string Sort, long Top, int Page)
        {
            if (Condition == null)
            { Condition = new QueryCondition(); }

            Condition.Add("IsDeleted", "=", typeof(bool).ToString(), "0");

            return base.List(Condition, Sort, Top, Page);
        }

        public override long List_Count(QueryCondition Condition = null)
        {
            
            if (Condition == null)
            { Condition = Do_Methods.CreateQueryCondition(); }
            Condition.Add("IsDeleted", "=", typeof(bool).ToString(), "0");
            return base.List_Count(Condition);
        }

        public override void Load(Keys Keys = null)
        {
            base.Load(Keys);

            Int64 ID = 0;
            if (Keys != null)
            {
                try { ID = Keys["RecruitmentTestUserID"]; }
                catch { }
            }

            List<QueryParameter> List_Sp = new List<QueryParameter>();
            List_Sp.Add(new QueryParameter("ID", ID));
            DataTable Dt;
            Dt = Do_Methods_Query.ExecuteQuery("usp_RecruitmentTestUser_Rights_Load", List_Sp).Tables[0];

            this.AddRequired(Dt);
            this.pTableDetail_Set("RecruitmentTestUser_Rights", Dt);
        }

        public string GeneratePassword(Int32 Length)
        {
            StringBuilder Sb = new StringBuilder();
            Random R_Type = new Random();
            Random R_Ch = new Random();
            Random R_Nm = new Random();
            for (Int32 Ct = 0; Ct < Length; Ct++)
            {
                if (R_Type.Next(2) == 0)
                { Sb.Append(Convert.ToChar(R_Ch.Next(26) + 65)); }
                else
                { Sb.Append(R_Nm.Next(10)); }
            }
            return Sb.ToString(); ;
        }

        #endregion

        #region _Properties

        public DataTable pDt_Rights
        {
            get { return this.pTableDetail_Get("RecruitmentTestUser_Rights"); }
        }

        #endregion
    }
}