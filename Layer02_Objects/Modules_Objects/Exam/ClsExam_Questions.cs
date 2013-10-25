using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Layer01_Common;
using Layer01_Common.Common;
using Layer02_Objects._System;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace Layer02_Objects.Modules_Objects.Exam
{
    public class ClsExam_Questions
    {
        DataRow mDr;
        List<ClsExam_Questions_Answers> mList_Answers = new List<ClsExam_Questions_Answers>();

        public ClsExam_Questions(DataRow Dr_Question, DataRow[] Arr_Dr_Answers)
        {
            this.mDr = Dr_Question;
            foreach (DataRow Dr in Arr_Dr_Answers)
            { this.mList_Answers.Add(new ClsExam_Questions_Answers(Dr)); }
        }

        public Int64 pCt
        {
            get { return (Int64)Do_Methods.IsNull(this.mDr["Ct"], 0); }
        }

        public string pQuestion
        {
            get { return (string)Do_Methods.IsNull(this.mDr["Question"], ""); }
        }

        public Layer02_Constants.eLookupQuestionType pQuestionType
        {
            get
            { return (Layer02_Constants.eLookupQuestionType)Do_Methods.Convert_Int32(this.mDr["LookupQuestionTypeID"], 0); }
        }

        public bool pIsCorrect
        {
            get { return (bool)Do_Methods.IsNull(this.mDr["IsCorrect"], false); }
        }

        public List<ClsExam_Questions_Answers> pList_Answers
        {
            get { return this.mList_Answers; }
        }
    }
}
