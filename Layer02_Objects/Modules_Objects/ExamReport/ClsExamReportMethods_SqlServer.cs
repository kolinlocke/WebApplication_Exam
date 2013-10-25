using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Layer01_Common;
using Layer01_Common.Common;
//using Layer01_Common.Connection;
using Layer01_Common.Objects;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.Base;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace Layer02_Objects.Modules_Objects.ExamReport 
{
    public class ClsExamReportMethods_SqlServer : Interface_ExamReportMethods
    {
        public DataTable GetReport(string Sort = "")
        {
            DataTable Dt = new ClsBase().pDa.GetQuery("uvw_RecruitmentTestExams_Scores_Desc", "", "", Sort);
            return Dt;
        }
    }
}
