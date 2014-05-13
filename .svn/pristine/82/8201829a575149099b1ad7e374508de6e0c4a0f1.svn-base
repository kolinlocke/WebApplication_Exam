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
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace Layer02_Objects.Modules_Objects
{
    public class ClsRights : ClsBase
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
        { return this.List((ClsQueryCondition)null, Sort); }

        public override DataTable List(ClsQueryCondition Condition, string Sort = "", int Top = 0, int Page = 0)
        {
            if (Condition == null)
            { Condition = new ClsQueryCondition(); }

            Condition.Add("IsDeleted", "=", typeof(bool).ToString(), "0");
            
            return base.List(Condition, Sort, Top, Page);
        }

        public override long List_Count(ClsQueryCondition Condition = null)
        {
            if (Condition == null)
            { Condition = new ClsQueryCondition(); }
            Condition.Add("IsDeleted", "=", typeof(bool).ToString(), "0");
            return base.List_Count(Condition);
        }

        public override void Load(ClsKeys Keys = null)
        {
            base.Load(Keys);

            Int64 ID = 0;
            if (Keys != null)
            {
                try { ID = Keys["RecruitmentTestRightsID"]; }
                catch { }
            }

            List<Do_Constants.Str_Parameters> List_Sp = new List<Do_Constants.Str_Parameters>();
            List_Sp.Add(new Do_Constants.Str_Parameters("@ID", ID));
            DataTable Dt;
            Dt = new ClsConnection_SqlServer().ExecuteQuery("usp_RecruitmentTestRights_Details_Load", List_Sp).Tables[0];

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
