using System;
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

namespace Layer02_Objects.Modules_Objects.ExamReport 
{
    public class ClsExamReportMethods_SqlServer : Interface_ExamReportMethods
    {
        public DataTable GetReport(string Sort = "")
        {
            DataTable Dt = Do_Methods_Query.GetQuery("uvw_RecruitmentTestExams_Scores_Desc", "", "", Sort);
            return Dt;
        }
    }
}
