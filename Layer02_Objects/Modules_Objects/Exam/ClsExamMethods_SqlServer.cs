﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataObjects_Framework;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.Common;
using DataObjects_Framework.Connection;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Objects;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using DataObjects_Framework.PreparedQueryObjects;

namespace Layer02_Objects.Modules_Objects.Exam
{
    public class ClsExamMethods_SqlServer: Interface_ExamMethods
    {
        #region _ImplementedMethods

        public DataSet GenerateExam(Int64 QuestionLimit, Int64 CategoryID)
        {
            //DataSet Rv = null;
            //ClsConnection_SqlServer Cn = new ClsConnection_SqlServer();
            //try
            //{ 
            //    Cn.Connect();
            //    List<Do_Constants.Str_Parameters> List_Sp = new List<Do_Constants.Str_Parameters>();
            //    List_Sp.Add(new Do_Constants.Str_Parameters("Question_Limit", QuestionLimit));
            //    List_Sp.Add(new Do_Constants.Str_Parameters("CategoryID", CategoryID));
            //    Rv = Cn.ExecuteQuery("usp_GenerateExam", List_Sp);
            //}
            //catch { }
            //finally
            //{ Cn.Close(); }

            Interface_DataAccess Da = Do_Methods.CreateDataAccess();
            List<QueryParameter> List_Sp = new List<QueryParameter>();
            List_Sp.Add(new QueryParameter("Question_Limit", QuestionLimit));
            List_Sp.Add(new QueryParameter("CategoryID", CategoryID));

            DataSet Rv = Da.ExecuteQuery("usp_GenerateExam", List_Sp);
            return Rv;
        }

        public DataSet LoadExam(long ExamID)
        {
            //DataSet Rv = null;
            //ClsConnection_SqlServer Cn = new ClsConnection_SqlServer();
            //try
            //{
            //    Cn.Connect();
            //    List<Do_Constants.Str_Parameters> List_Sp = new List<Do_Constants.Str_Parameters>();
            //    List_Sp.Add(new Do_Constants.Str_Parameters("ExamID", ExamID));
            //    Rv = Cn.ExecuteQuery("usp_LoadExam", List_Sp);
            //}
            //catch { }
            //finally
            //{ Cn.Close(); }

            Interface_DataAccess Da = Do_Methods.CreateDataAccess();
            List<QueryParameter> List_Sp = new List<QueryParameter>();
            List_Sp.Add(new QueryParameter("ExamID", ExamID));

            DataSet Rv = Da.ExecuteQuery("usp_LoadExam", List_Sp);
            return Rv;
        }

        public long ComputeScore(DataSet Ds_Exam)
        {
            DataTable Dt_Question = Ds_Exam.Tables[0];
            DataTable Dt_Question_Answer = Ds_Exam.Tables[1];

            StringBuilder Sb_Query = new StringBuilder();
            Sb_Query.Append(@" Select * From RecruitmentTestQuestionAnswers ");
            Sb_Query.Append(@" Where ");
            Sb_Query.Append(@" Lkp_RecruitmentTestQuestionsID = @QuestionID ");
            Sb_Query.Append(@" And IsNull(IsAnswer,0) = 1 And IsNull(IsDeleted,0) = 0");

            PreparedQuery Pq = Do_Methods.CreatePreparedQuery();
            Pq.pQuery = Sb_Query.ToString();
            Pq.Add_Parameter("QuestionID", Do_Constants.eParameterType.Long);
            Pq.Prepare();

            Int64 Score = 0;

            foreach (DataRow Dr in Dt_Question.Rows)
            {
                Pq.pParameters.GetParameter("QuestionID").Value = Do_Methods.IsNull(Dr["RecruitmentTestQuestionsID"], 0);
                DataTable Inner_Dt = Pq.ExecuteQuery().Tables[0];
                Int64 Ct_Answer = Inner_Dt.Rows.Count;
                Int64 Ct_ExamAnswer = 0;
                DataRow[] Arr_Dr_Answers = Dt_Question_Answer.Select(@"Lkp_RecruitmentTestQuestionsID = " + ((Int64)Dr["RecruitmentTestQuestionsID"]).ToString());
                foreach (DataRow Inner_Dr in Arr_Dr_Answers)
                {
                    bool IsExamAnswer = (bool)Do_Methods.IsNull(Inner_Dr["IsAnswered"], false);
                    bool IsAnswer = false;

                    DataRow[] Inner_Arr_Dr = Inner_Dt.Select(@"Lkp_RecruitmentTestAnswersID = " + ((Int64)Inner_Dr["Lkp_RecruitmentTestAnswersID"]));
                    if (Inner_Arr_Dr.Length > 0)
                    { IsAnswer = true; }

                    if (IsAnswer && (IsExamAnswer == IsAnswer))
                    { Ct_ExamAnswer++; }
                }

                if (Ct_Answer == Ct_ExamAnswer)
                { Score++; }
            }

            return Score;
        }

        #endregion        
    }
}
