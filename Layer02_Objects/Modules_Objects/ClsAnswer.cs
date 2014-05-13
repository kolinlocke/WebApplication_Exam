using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using Layer02_Objects;

namespace Layer02_Objects.Modules_Objects
{
    public class ClsAnswer: Base
    {
        #region _Constructor

        public ClsAnswer()
        { this.Setup("RecruitmentTestAnswers", ""); }

        #endregion
    }
}
