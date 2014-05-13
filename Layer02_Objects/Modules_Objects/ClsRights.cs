using System;
using System.Collections.Generic;
using System.Data;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.Common;
using DataObjects_Framework.Objects;
using Layer02_Objects._System;
using DataObjects_Framework.Connection;

namespace Layer02_Objects.Modules_Objects
{
    public class ClsRights : Base
    {
        #region _Constructor

        public ClsRights(ClsSysCurrentUser pCurrentUser = null)
        {
            this.Setup("RecruitmentTestRights");
            this.Setup_AddTableDetail("RecruitmentTestRights_Details", "", "1 = 0");
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
            { Condition = new QueryCondition(); }
            Condition.Add("IsDeleted", "=", typeof(bool).ToString(), "0");
            return base.List_Count(Condition);
        }

        public override void Load(Keys Keys = null)
        {
            base.Load(Keys);

            Int64 ID = 0;
            if (Keys != null)
            {
                try { ID = Keys["RecruitmentTestRightsID"]; }
                catch { }
            }

            List<QueryParameter> Params = new List<QueryParameter>();
            Params.Add(new QueryParameter("ID", ID));
            DataTable Dt;
            Dt = Do_Methods_Query.ExecuteQuery("usp_RecruitmentTestRights_Details_Load", Params).Tables[0];

            this.AddRequired(Dt);
            this.pTableDetail_Set("RecruitmentTestRights_Details", Dt);
        }

        #endregion

        #region _Properties

        public DataTable pDt_Details
        {
            get { return this.pTableDetail_Get("RecruitmentTestRights_Details"); }
        }

        #endregion
    }
}
