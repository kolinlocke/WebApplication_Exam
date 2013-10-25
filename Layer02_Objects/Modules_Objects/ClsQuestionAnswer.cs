using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.VisualBasic;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using Layer02_Objects;
using Layer02_Objects._System;

namespace Layer02_Objects.Modules_Objects
{
    public class ClsQuestionAnswer : ClsBase_List
    {
        #region _Variables

        //ClsBaseObjs mBO_Answer = new ClsBaseObjs();
        
        #endregion

        #region _Constructor

        public ClsQuestionAnswer()
        {
            ClsQueryCondition Qc = base.pDa.CreateQueryCondition();
            Qc.Add("IsDeleted", "= 0", typeof(bool).ToString(), "0");
            this.Setup("RecruitmentTestQuestionAnswers", "uvw_RecruitmentTestQuestionAnswers", Qc);

            //[-]

            List<Do_Constants.Str_ForeignKeyRelation> FetchKeys = new List<Do_Constants.Str_ForeignKeyRelation>();
            FetchKeys.Add(new Do_Constants.Str_ForeignKeyRelation("Lkp_RecruitmentTestQuestionsID", "RecruitmentTestQuestionsID"));

            List<Do_Constants.Str_ForeignKeyRelation> ForeignKeys = new List<Do_Constants.Str_ForeignKeyRelation>();
            ForeignKeys.Add(new Do_Constants.Str_ForeignKeyRelation("Lkp_RecruitmentTestAnswersID", "RecruitmentTestAnswersID"));

            this.Setup_AddListObject(
                "Answer"
                , new ClsAnswer()
                , null
                , "uvw_RecruitmentTestAnswers_QuestionAnswers"
                , FetchKeys
                , ForeignKeys
                , Qc);
        }

        #endregion
                
        #region _Methods

        public override void Load(ClsKeys Keys, ClsBase Obj_Parent = null)
        {
            base.Load(Keys, Obj_Parent);

            DataRow[] ArrDr = this.pDt_List.Select("", "OrderIndex");
            int Ct = 0;
            foreach (DataRow Dr in ArrDr)
            {
                Ct++;
                Dr["OrderIndex"] = Ct;
            }

            this.FixOrderIndex(true);
            this.pDt_List.DefaultView.Sort = "OrderIndex";
        }

        protected override void Save_Add()
        {
            DataRow[] ArrDr = this.pDt_List.Select("", "", DataViewRowState.CurrentRows);
            foreach (DataRow Inner_Dr in ArrDr)
            { Inner_Dr["Lkp_RecruitmentTestQuestionsID"] = this.mObj_Parent.pDr["RecruitmentTestQuestionsID"]; }
        }

        public void FixOrderIndex(bool IsSetup = false)
        {
            DataRow[] ArrDr = this.pDt_List.Select("", "OrderIndex", DataViewRowState.CurrentRows);
            int Ct = 0;
            foreach (DataRow Dr in ArrDr)
            {
                Ct++;
                Dr["OrderIndex"] = Ct;
            }
            if (IsSetup)
            { this.pDt_List.AcceptChanges(); }
        }

        #endregion

        #region _Properties

        public DataTable pDt_Answer
        {
            get { return this.pDt_ListObject_Get("Answer"); }
        }

        public ClsAnswer pObj_Answer_Get(Int64 TmpKey)
        {
            return (ClsAnswer)this.pObj_ListObject_Get("Answer", TmpKey);
        }
        
        #endregion
    }
}
